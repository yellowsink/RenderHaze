using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RenderHaze.ImageRenderer
{
	public static class LayerProcessing
	{
		public static (float x, float y) Scale<TPixel>(IImageProcessingContext pc, Layer<TPixel> layer)
			where TPixel : unmanaged, IPixel<TPixel>
		{
			var oX  = 0f;
			var oY  = 0f;

			if (Math.Abs(layer.ScaleX - 1) > 0.01)
			{
				var sourceX = layer.Image.Width;
				var newX    = sourceX * layer.ScaleX;
				oX = layer.ScaleOrigin switch
				{
					OriginPoint.TopLeft      => 0,
					OriginPoint.CenterLeft   => 0,
					OriginPoint.BottomLeft   => 0,
					OriginPoint.TopCenter    => (float) sourceX / 2 - newX / 2,
					OriginPoint.Center       => (float) sourceX / 2 - newX / 2,
					OriginPoint.BottomCenter => (float) sourceX / 2 - newX / 2,
					OriginPoint.TopRight     => sourceX             - newX,
					OriginPoint.CenterRight  => sourceX             - newX,
					OriginPoint.BottomRight  => sourceX             - newX,
					_                        => throw new ArgumentOutOfRangeException()
				};
			}

			if (Math.Abs(layer.ScaleY - 1) > 0.01)
			{
				var sourceY = layer.Image.Height;
				var newY    = sourceY * layer.ScaleY;
				oY = layer.ScaleOrigin switch
				{
					OriginPoint.TopLeft      => 0,
					OriginPoint.TopCenter    => 0,
					OriginPoint.TopRight     => 0,
					OriginPoint.CenterLeft   => (float) sourceY / 2 - newY / 2,
					OriginPoint.Center       => (float) sourceY / 2 - newY / 2,
					OriginPoint.CenterRight  => (float) sourceY / 2 - newY / 2,
					OriginPoint.BottomLeft   => sourceY             - newY,
					OriginPoint.BottomCenter => sourceY             - newY,
					OriginPoint.BottomRight  => sourceY             - newY,
					_                        => throw new ArgumentOutOfRangeException()
				};
			}

			pc.Transform(new AffineTransformBuilder()
										   .AppendScale(new SizeF(layer.ScaleX, layer.ScaleY)));

			return (oX, oY);
		}

		public static (int x, int y) CalculateOriginOffset<TPixel>(IImageProcessingContext pc, Layer<TPixel> layer)
			where TPixel : unmanaged, IPixel<TPixel>
		{
			var x = layer.OffsetOrigin switch
			{
				OriginPoint.TopLeft      => 0,
				OriginPoint.CenterLeft   => 0,
				OriginPoint.BottomLeft   => 0,
				OriginPoint.TopCenter    => -(layer.Image.Width / 2),
				OriginPoint.Center       => -(layer.Image.Width / 2),
				OriginPoint.BottomCenter => -(layer.Image.Width / 2),
				OriginPoint.TopRight     => -layer.Image.Width,
				OriginPoint.CenterRight  => -layer.Image.Width,
				OriginPoint.BottomRight  => -layer.Image.Width,
				_                        => throw new ArgumentOutOfRangeException()
			};

			var y = layer.OffsetOrigin switch
			{
				OriginPoint.TopLeft      => 0,
				OriginPoint.TopCenter    => 0,
				OriginPoint.TopRight     => 0,
				OriginPoint.CenterLeft   => -(layer.Image.Height / 2),
				OriginPoint.Center       => -(layer.Image.Height / 2),
				OriginPoint.CenterRight  => -(layer.Image.Height / 2),
				OriginPoint.BottomLeft   => -layer.Image.Height,
				OriginPoint.BottomCenter => -layer.Image.Height,
				OriginPoint.BottomRight  => -layer.Image.Height,
				_                        => throw new ArgumentOutOfRangeException()
			};

			return (x, y);
		}
	}
}