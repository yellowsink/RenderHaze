using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageRenderer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace VideoRenderer
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

		public Image<TPixel>[] RenderFrames(int width, int height) => GenerateRenderer(out _).RenderAll(width, height);

		public void RenderFramesToDisk(int width, int height, string dir)
		{
			var directory  = Directory.CreateDirectory(dir);
			var renderer   = GenerateRenderer(out var frameCount);
			var frameNames = Enumerable.Range(1, (int) frameCount)
									   .Select(i => Path.Combine(directory.FullName, $"{i}.png"))
									   .ToArray();
			
			renderer.RenderAllToFiles(width, height, frameNames);
		}

		private void GenerateLayers(ref Renderer<TPixel> rend, ulong frameNum)
		{
			foreach (var timeline in ObjectTimelines)
			{
				var        obj       = timeline.Obj;
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
				
				if (!timePoint.HasValue) return;
				if (!nextTp.HasValue)
				{
					var ntp = timePoint.Value;
					ntp.FrameNum++;
					nextTp = ntp;
				}
				
				var tp            = timePoint.Value;
				var nTp           = nextTp.Value;
				var pointProgress = (frameNum - tp.FrameNum + 1) / (float) (nTp.FrameNum - tp.FrameNum + 1);

				var x = Convert.ToInt32(Interpolate(tp.X, nTp.X, pointProgress));
				var y = Convert.ToInt32(Interpolate(tp.Y, nTp.Y, pointProgress));
				var o = Interpolate(tp.Opacity, nTp.Opacity, pointProgress);
				
				rend.AddObject(obj.Image, x, y, o);
			}
		}

		private float Interpolate(float val1, float val2, float blend) => (blend * (val2 - val1)) + val1;
	}
}