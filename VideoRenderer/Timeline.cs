using SixLabors.ImageSharp.PixelFormats;

namespace VideoRenderer
{
	public struct Timeline<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public Object<TPixel> Obj;
		public TimePoint[]    TimePoints;
		public ulong          LastFrameNum;
	}
}