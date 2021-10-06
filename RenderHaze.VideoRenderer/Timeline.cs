using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	public struct Timeline<TPixel> where TPixel : unmanaged, IPixel<TPixel>
	{
		public Object<TPixel> Obj;
		public TimePoint[]    TimePoints;
		public ulong          LastFrameNum;

		public Timeline(Object<TPixel> obj, ulong lastFrameNum, TimePoint[] timePoints)
		{
			Obj          = obj;
			TimePoints   = timePoints;
			LastFrameNum = lastFrameNum;
		}
	}
}