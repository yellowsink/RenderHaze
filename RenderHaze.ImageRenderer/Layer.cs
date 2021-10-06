using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.ImageRenderer
{
	public class Layer<T> where T : unmanaged, IPixel<T>
	{
		public Image<T> Image;
		public int      OffsetX;
		public int      OffsetY;
		public float    Opacity;

		public Layer(Image<T> image, int offsetX, int offsetY, float opacity)
		{
			Image   = image;
			OffsetX = offsetX;
			OffsetY = offsetY;
			Opacity = opacity;
		}
	}
}