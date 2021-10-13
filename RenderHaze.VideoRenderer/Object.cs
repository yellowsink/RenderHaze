using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	[DebuggerDisplay("{Image.Width}x{Image.Height} Image")]
	public class Object<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public Image<TPixel> Image;

		public Object(Image<TPixel> image) => Image = image;
	}
}