using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RenderHaze.ImageRenderer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	public class FrameRenderer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public List<Timeline<TPixel>> ObjectTimelines = new();

		private BatchRenderer<TPixel> GenerateRenderer(out ulong frameCount)
		{
			frameCount = ObjectTimelines.Select(t => t.LastFrameNum).Max();

			var renderer = new BatchRenderer<TPixel>();
			
			for (ulong i = 0; i < frameCount; i++)
			{
				var rend = new Renderer<TPixel>();
				GenerateLayers(ref rend, i);
				renderer.Renderers.Add(rend);
			}

			return renderer;
		}

		public Image<TPixel>[] RenderFrames(int width, int height)
		{
			using var rend = GenerateRenderer(out _);
			return rend.RenderAll(width, height);
		}

		public void RenderFramesToDisk(int width, int height, string dir, EventHandler<(int, int)>? progress)
		{
			var       directory = Directory.CreateDirectory(dir);
			using var renderer  = GenerateRenderer(out var frameCount);
			var frameNames = Enumerable.Range(1, (int) frameCount)
									   .Select(i => Path.Combine(directory.FullName,
																 i.ToString().PadLeft(6, '0') + ".png"))
									   .ToArray();
			
			renderer.RenderAllToFiles(width, height, frameNames, progress);
		}

		private void GenerateLayers(ref Renderer<TPixel> rend, ulong frame)
		{
			foreach (var timeline in ObjectTimelines)
			{
				var obj = timeline.Obj;
				var (rawLastTp, rawNextTp) = FindRelevantTimingPoints(frame, timeline);

				if (!rawLastTp.HasValue) return;
				if (!rawNextTp.HasValue)
				{
					var ntp = rawLastTp.Value;
					ntp.FrameNum++;
					rawNextTp = ntp;
				}

				var lastPoint = rawLastTp.Value;
				var nextPoint = rawNextTp.Value;
				var progress = (float) (frame - lastPoint.FrameNum + 1) / (nextPoint.FrameNum - lastPoint.FrameNum + 1);

				var o = Interpolate(lastPoint.Opacity, nextPoint.Opacity, progress);

				if (o < 0.001) continue; // acceptable range for opacity to be effectively 0

				var (lxo, lyo) = ImageProcessing.CalculateScaleOffset(obj.Image.Width,
																	  obj.Image.Height,
																	  lastPoint.Sx,
																	  lastPoint.Sy,
																	  lastPoint.ScaleOrigin);
				var (nxo, nyo) = ImageProcessing.CalculateScaleOffset(obj.Image.Width,
																	  obj.Image.Height,
																	  nextPoint.Sx,
																	  nextPoint.Sy,
																	  nextPoint.ScaleOrigin);
				var (lxo1, lyo1)
					= ImageProcessing.CalculateOriginOffset(obj.Image.Width, obj.Image.Height, lastPoint.OffsetOrigin);
				var (nxo1, nyo1)
					= ImageProcessing.CalculateOriginOffset(obj.Image.Width, obj.Image.Height, nextPoint.OffsetOrigin);

				lxo += lxo1;
				lyo += lyo1;
				nxo += nxo1;
				nyo += nyo1;
				
				var x  = Convert.ToInt32(Interpolate(lastPoint.X + lxo, nextPoint.X + lyo, progress));
				var y  = Convert.ToInt32(Interpolate(lastPoint.Y + nxo, nextPoint.Y + nyo, progress));
				var sx = Interpolate(lastPoint.Sx, nextPoint.Sx, progress);
				var sy = Interpolate(lastPoint.Sy, nextPoint.Sy, progress);

				rend.AddObject(obj.Image, x, y, o, sx, sy);
			}
		}

		private static (TimePoint?, TimePoint?) FindRelevantTimingPoints(ulong frameNum, Timeline<TPixel> timeline)
		{
			TimePoint? timePoint = null;
			TimePoint? nextTp    = null;
			foreach (var point in timeline.TimePoints)
			{
				if (point.FrameNum > frameNum && timePoint.HasValue)
				{
					nextTp = point;
					break;
				}

				if (point.FrameNum <= frameNum)
					timePoint = point;
			}

			return (timePoint, nextTp);
		}

		private static float Interpolate(float val1, float val2, float blend) => (blend * (val2 - val1)) + val1;
	}
}