using System;
using SkiaSharp;

namespace RenderHaze.ImageRenderer;

public static class ImageProcessing
{
	public static (int x, int y) Scale(Layer layer)
	{
		var resized = new SKBitmap(new SKImageInfo((int) (layer.Image.Width  * layer.ScaleX),
												   (int) (layer.Image.Height * layer.ScaleY)));

		layer.Image.ScalePixels(resized, SKFilterQuality.High);

		layer.Image = resized;

		return CalculateScaleOffset(layer.Image.Width,
									layer.Image.Height,
									layer.ScaleX,
									layer.ScaleY,
									layer.ScaleOrigin);
	}

	public static (int x, int y) CalculateScaleOffset(int         width, int height, float scaleX, float scaleY,
													  OriginPoint scaleOrigin)
	{
		var oX = 0f;
		var oY = 0f;

		if (Math.Abs(scaleX - 1) > 0.01)
		{
			var newX = width * scaleX;
			oX = scaleOrigin switch
			{
				OriginPoint.TopLeft      => 0,
				OriginPoint.CenterLeft   => 0,
				OriginPoint.BottomLeft   => 0,
				OriginPoint.TopCenter    => (width - newX) / (2 * scaleX),
				OriginPoint.Center       => (width - newX) / (2 * scaleX),
				OriginPoint.BottomCenter => (width - newX) / (2 * scaleX),
				OriginPoint.TopRight     => (width - newX) / 2,
				OriginPoint.CenterRight  => (width - newX) / 2,
				OriginPoint.BottomRight  => (width - newX) / 2,
				_                        => throw new ArgumentOutOfRangeException()
			};
		}

		if (!(Math.Abs(scaleY - 1) > 0.01)) return ((int) oX, (int) oY);

		var newY = height * scaleY;
		oY = scaleOrigin switch
		{
			OriginPoint.TopLeft      => 0,
			OriginPoint.TopCenter    => 0,
			OriginPoint.TopRight     => 0,
			OriginPoint.CenterLeft   => (height - newY) / (2 * scaleY),
			OriginPoint.Center       => (height - newY) / (2 * scaleY),
			OriginPoint.CenterRight  => (height - newY) / (2 * scaleY),
			OriginPoint.BottomLeft   => (height - newY) / 2,
			OriginPoint.BottomCenter => (height - newY) / 2,
			OriginPoint.BottomRight  => (height - newY) / 2,
			_                        => throw new ArgumentOutOfRangeException()
		};

		return ((int) oX, (int) oY);
	}

	public static (int x, int y) CalculateOriginOffset(Layer layer)
		=> CalculateOriginOffset(layer.Image.Width, layer.Image.Height, layer.OffsetOrigin);

	public static (int x, int y) CalculateOriginOffset(int width, int height, OriginPoint offsetOrigin)
	{
		var x = offsetOrigin switch
		{
			OriginPoint.TopLeft      => 0,
			OriginPoint.CenterLeft   => 0,
			OriginPoint.BottomLeft   => 0,
			OriginPoint.TopCenter    => -(width / 2),
			OriginPoint.Center       => -(width / 2),
			OriginPoint.BottomCenter => -(width / 2),
			OriginPoint.TopRight     => -width,
			OriginPoint.CenterRight  => -width,
			OriginPoint.BottomRight  => -width,
			_                        => throw new ArgumentOutOfRangeException()
		};

		var y = offsetOrigin switch
		{
			OriginPoint.TopLeft      => 0,
			OriginPoint.TopCenter    => 0,
			OriginPoint.TopRight     => 0,
			OriginPoint.CenterLeft   => -(height / 2),
			OriginPoint.Center       => -(height / 2),
			OriginPoint.CenterRight  => -(height / 2),
			OriginPoint.BottomLeft   => -height,
			OriginPoint.BottomCenter => -height,
			OriginPoint.BottomRight  => -height,
			_                        => throw new ArgumentOutOfRangeException()
		};

		return (x, y);
	}
}