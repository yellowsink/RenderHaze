using System;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	public class VideoRenderSupervisor<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public readonly Timeline<TPixel>[] Timelines;
		public readonly uint               Width;
		public readonly uint               Height;
		public readonly double             Framerate;
		
		public VideoRenderSupervisor(Timeline<TPixel>[] timelines, uint width, uint height, double framerate)
		{
			Timelines = timelines;
			Width     = width;
			Height    = height;
			Framerate = framerate;
		}

		public void RenderVideoTo(string outputPath, string? audioPath, EventHandler<RenderProgressReport>? progress)
		{
			var tmpPath = Path.Combine(TmpPath.DefaultTempLocation, "renderhaze_frames");

			EventHandler<(int, int)>? progressFunc = progress != null
														 ? (s, p) => progress.Invoke(s,
															 new RenderProgressReport
															 {
																 Total = p.Item1, Completed = p.Item2,
																 Type  = RenderProgressType.Rendering
															 })
														 : null;
			
			var frameRenderer = new FrameRenderer<TPixel>
			{
				ObjectTimelines = Timelines.ToList()
			};
			frameRenderer.RenderFramesToDisk((int) Width, (int) Height, tmpPath, progressFunc);

			var files = new DirectoryInfo(tmpPath).EnumerateFiles()
												  .Select(f => f.FullName)
												  .OrderBy(fn => fn)
												  .ToArray();
			
			progress?.Invoke(this, new RenderProgressReport{Type = RenderProgressType.Encoding});
			Encoder.Encode(files, outputPath, Framerate, audioPath).Wait();
			Directory.Delete(tmpPath, true);
		}
	}

	public struct RenderProgressReport
	{
		public int                Total;
		public int                Completed;
		public RenderProgressType Type;
	}

	public enum RenderProgressType
	{
		Rendering, Encoding
	}
}