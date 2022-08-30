using System;
using System.Diagnostics;
using SkiaSharp;

namespace RenderHaze.ImageRenderer;

[DebuggerDisplay("({OffsetX},{OffsetY}) @{Opacity * 100}% Scaled ({ScaleX * 100},{ScaleY * 100})%")]
public class Layer : IDisposable
{
	public SKBitmap    Image;
	public OriginPoint OffsetOrigin;
	public int         OffsetX;
	public int         OffsetY;
	public float       Opacity;
	public OriginPoint ScaleOrigin;
	public float       ScaleX;
	public float       ScaleY;

	public Layer(SKBitmap    image, int offsetX, int offsetY, float opacity = 1, float scaleX = 1, float scaleY = 1,
				 OriginPoint offsetOrigin = OriginPoint.TopLeft, OriginPoint scaleOrigin = OriginPoint.TopLeft)
	{
		Image        = image;
		OffsetX      = offsetX;
		OffsetY      = offsetY;
		Opacity      = opacity;
		ScaleX       = scaleX;
		ScaleY       = scaleY;
		OffsetOrigin = offsetOrigin;
		ScaleOrigin  = scaleOrigin;
	}

	public void Dispose() => Image.Dispose();
}