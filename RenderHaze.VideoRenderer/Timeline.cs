using System.Diagnostics;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.VideoRenderer
{
	[DebuggerDisplay("Timeline with {Obj.Image.Width}x{Obj.Image.Height} image and {TimePoints.Length} timing points")]
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