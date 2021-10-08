using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RenderHaze.ImageRenderer
{
	public static class ImageProcessing
	{
		public static (int x, int y) Scale<TPixel>(IImageProcessingContext pc, Layer<TPixel> layer)
			where TPixel : unmanaged, IPixel<TPixel>
		{
			pc.Transform(new ProjectiveTransformBuilder().AppendScale(new SizeF(layer.ScaleX, layer.ScaleY)));
			return CalculateScaleOffset(layer.Image.Width,
										layer.Image.Height,
										layer.ScaleX,
										layer.ScaleY,
										layer.ScaleOrigin);
		}
		
		public static (int x, int y) CalculateScaleOffset(int width,
														  int height,
														  float scaleX,
														  float scaleY,
														  OriginPoint scaleOrigin)
		{
			var oX  = 0f;
			var oY  = 0f;

			if (Math.Abs(scaleX - 1) > 0.01)
			{
				var newX    = width * scaleX;
				oX = scaleOrigin switch
				{
					OriginPoint.TopLeft      => 0,
					OriginPoint.CenterLeft   => 0,
					OriginPoint.BottomLeft   => 0,
					OriginPoint.TopCenter    => (float) width / 2 - newX / 2,
					OriginPoint.Center       => (float) width / 2 - newX / 2,
					OriginPoint.BottomCenter => (float) width / 2 - newX / 2,
					OriginPoint.TopRight     => width             - newX,
					OriginPoint.CenterRight  => width             - newX,
					OriginPoint.BottomRight  => width             - newX,
					_                        => throw new ArgumentOutOfRangeException()
				};
			}

			if (Math.Abs(scaleY - 1) > 0.01)
			{
				var newY    = height * scaleY;
				oY = scaleOrigin switch
				{
					OriginPoint.TopLeft      => 0,
					OriginPoint.TopCenter    => 0,
					OriginPoint.TopRight     => 0,
					OriginPoint.CenterLeft   => (float) height / 2 - newY / 2,
					OriginPoint.Center       => (float) height / 2 - newY / 2,
					OriginPoint.CenterRight  => (float) height / 2 - newY / 2,
					OriginPoint.BottomLeft   => height             - newY,
					OriginPoint.BottomCenter => height             - newY,
					OriginPoint.BottomRight  => height             - newY,
					_                        => throw new ArgumentOutOfRangeException()
				};
			}

			return ((int) oX, (int) oY);
		}

		public static (int x, int y) CalculateOriginOffset<TPixel>(Layer<TPixel> layer)
			where TPixel : unmanaged, IPixel<TPixel>
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
}