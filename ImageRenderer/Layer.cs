using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageRenderer
{
	public class Layer<T> where T : unmanaged, IPixel<T>
	{
		public Image<T> Image;
		public int      OffsetX;
		public int      OffsetY;
		public float    Opacity;
	}
}