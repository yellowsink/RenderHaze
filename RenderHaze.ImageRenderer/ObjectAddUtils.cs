using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.ImageRenderer
{
	public static class ObjectAddUtils
	{
		public static void AddObject<TPixel>(this Renderer<TPixel> renderer, Image<TPixel> image,      int   x, int y,
											 float                 opacity,  float         scaleX = 1, float scaleY = 1,
											 OriginPoint           offsetOrigin = OriginPoint.TopLeft,
											 OriginPoint           scaleOrigin  = OriginPoint.TopLeft)
			where TPixel : unmanaged, IPixel<TPixel>
			=> renderer.Layers.Add(new Layer<TPixel>(image, x, y, opacity, scaleX, scaleY, offsetOrigin, scaleOrigin));

		public static void AddObject<TPixel>(this Renderer<TPixel> renderer, string imgPath,    int   x, int y,
											 float                 opacity,  float  scaleX = 1, float scaleY = 1,
											 OriginPoint           offsetOrigin = OriginPoint.TopLeft,
											 OriginPoint           scaleOrigin  = OriginPoint.TopLeft)
			where TPixel : unmanaged, IPixel<TPixel>
			=> renderer.AddObject(Image.Load<TPixel>(imgPath),
								  x,
								  y,
								  opacity,
								  scaleX,
								  scaleY,
								  offsetOrigin,
								  scaleOrigin);
	}
}