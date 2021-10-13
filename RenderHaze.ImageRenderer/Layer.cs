using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.ImageRenderer
{
	[DebuggerDisplay("({OffsetX},{OffsetY}) @{Opacity * 100}% Scaled ({ScaleX * 100},{ScaleY * 100})%")]
	public class Layer<T> where T : unmanaged, IPixel<T>
	{
		public Image<T>    Image;
		public int         OffsetX;
		public int         OffsetY;
		public float       Opacity;
		public OriginPoint OffsetOrigin;
		public OriginPoint ScaleOrigin;
		public float       ScaleX;
		public float       ScaleY;

		public Layer(Image<T>    image, int offsetX, int offsetY, float opacity = 1, float scaleX = 1, float scaleY = 1,
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
	}
}