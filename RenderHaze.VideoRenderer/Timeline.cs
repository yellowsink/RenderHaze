using System.Diagnostics;

namespace RenderHaze.VideoRenderer;

[DebuggerDisplay("Timeline with {Obj.Image.Width}x{Obj.Image.Height} image and {TimePoints.Length} timing points")]
public struct Timeline
{
	public RhObject    Obj;
	public TimePoint[] TimePoints;
	public ulong       LastFrameNum;

	public Timeline(RhObject obj, ulong lastFrameNum, TimePoint[] timePoints)
	{
		Obj          = obj;
		TimePoints   = timePoints;
		LastFrameNum = lastFrameNum;
	}
}