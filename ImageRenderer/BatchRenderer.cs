using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageRenderer
{
	public class BatchRenderer<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public List<Renderer<TPixel>> Renderers = new();
		
		public BatchRenderer(List<Renderer<TPixel>> renderers) => Renderers = renderers;
		public BatchRenderer() {}

		public Image<TPixel>[] RenderAll(int width, int height)
			=> Renderers
			  .Select(r => r.Render(width, height))
			  .ToArray();
	}
}