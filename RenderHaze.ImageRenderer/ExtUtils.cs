using System.IO;
using SkiaSharp;

namespace RenderHaze.ImageRenderer;

public static class ExtUtils
{
	public static void AddObject(this Renderer renderer, SKBitmap image, int x, int y, float opacity, float scaleX = 1,
								 float         scaleY      = 1, OriginPoint offsetOrigin = OriginPoint.TopLeft,
								 OriginPoint   scaleOrigin = OriginPoint.TopLeft)
		=> renderer.Layers.Add(new Layer(image, x, y, opacity, scaleX, scaleY, offsetOrigin, scaleOrigin));

	public static void AddObject(this Renderer renderer, string imgPath, int x, int y, float opacity, float scaleX = 1,
								 float         scaleY      = 1, OriginPoint offsetOrigin = OriginPoint.TopLeft,
								 OriginPoint   scaleOrigin = OriginPoint.TopLeft)
		=> renderer.AddObject(SKBitmap.Decode(imgPath), x, y, opacity, scaleX, scaleY, offsetOrigin, scaleOrigin);

	public static void SavePng(this SKImage img, string path)
	{
		using var stream = File.OpenWrite(path);
		img.Encode().SaveTo(stream);
	}
}