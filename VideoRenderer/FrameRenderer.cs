using System.Collections.Generic;
using System.Linq;
using ImageRenderer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace VideoRenderer
{
	public class FrameRenderer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public List<Timeline<TPixel>> ObjectTimelines;

		public Image<TPixel>[] RenderFrames(int width, int height)
		{
			var lastFrame = ObjectTimelines.Select(t => t.LastFrameNum).Max();

			var renderer = new BatchRenderer<TPixel>();
			
			for (ulong i = 0; i < lastFrame; i++)
			{
				var rend = new Renderer<TPixel>();
				GenerateLayers(ref rend, i);
				renderer.Renderers.Add(rend);
			}

			return renderer.RenderAll(width, height);
		}

		private void GenerateLayers(ref Renderer<TPixel> rend, ulong frameNum)
		{
			foreach (var timeline in ObjectTimelines)
			{
				var        obj       = timeline.Obj;
				TimePoint? timePoint = null;
				foreach (var point in timeline.TimePoints)
				{
					if (point.FrameNum > frameNum) break;
					timePoint = point;
				}
				
				if (!timePoint.HasValue) return;
				
				var tp = timePoint.Value;
				rend.AddObject(obj.Image, tp.X, tp.Y, tp.Opacity);
			}
		}
	}
}