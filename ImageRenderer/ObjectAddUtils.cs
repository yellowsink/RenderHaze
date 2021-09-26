using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageRenderer
{
	public static class ObjectAddUtils
	{
		public static void AddObject<TPixel>(this Renderer<TPixel> renderer, Image<TPixel> image, int x, int y, float opacity)
			where TPixel : unmanaged, IPixel<TPixel>
			=> renderer.Layers.Add(new Layer<TPixel>
			{
				OffsetX = x,
				OffsetY = y,
				Opacity = opacity,
				Image = image
			});

		public static void AddObject<TPixel>(this Renderer<TPixel> renderer, string imgPath, int x, int y, float opacity)
			where TPixel : unmanaged, IPixel<TPixel>
			=> renderer.AddObject(Image.Load<TPixel>(imgPath), x, y, opacity);
	}
}