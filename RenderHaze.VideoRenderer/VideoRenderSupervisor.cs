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

		public void RenderVideoTo(string outputPath, string? audioPath)
		{
			var tmpPath = Path.Combine(TmpPath.DefaultTempLocation, "renderhaze_frames");
			
			var frameRenderer = new FrameRenderer<TPixel>();
			frameRenderer.ObjectTimelines = Timelines.ToList();
			frameRenderer.RenderFramesToDisk((int) Width, (int) Height, tmpPath);

			var files = new DirectoryInfo(tmpPath).EnumerateFiles()
												  .Select(f => f.FullName)
												  .OrderBy(fn => fn)
												  .ToArray();
			
			Encoder.Encode(files, outputPath, Framerate, audioPath).Wait();
			
			Directory.Delete(tmpPath, true);
		}
	}
}