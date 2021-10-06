using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	public class Object<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public Image<TPixel> Image;

		public Object(Image<TPixel> image) => Image = image;
	}
}