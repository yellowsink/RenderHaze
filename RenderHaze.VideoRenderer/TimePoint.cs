using RenderHaze.ImageRenderer;

namespace RenderHaze.VideoRenderer
{
	public struct TimePoint
	{
		public int         X;
		public int         Y;
		public float       Opacity;
		public ulong       FrameNum;
		public OriginPoint OffsetOrigin;
		public OriginPoint ScaleOrigin;
		public float       Sx;
		public float       Sy;

		public TimePoint(ulong       frameNum, int x, int y, float opacity, float sx = 1, float sy = 1,
						 OriginPoint offsetOrigin = OriginPoint.TopLeft, OriginPoint scaleOrigin = OriginPoint.TopLeft)
		{
			X            = x;
			Y            = y;
			Opacity      = opacity;
			Sx           = sx;
			Sy           = sy;
			OffsetOrigin = offsetOrigin;
			ScaleOrigin  = scaleOrigin;
			FrameNum     = frameNum;
		}
	}
}