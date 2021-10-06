namespace RenderHaze.VideoRenderer
{
	public struct TimePoint
	{
		public int   X;
		public int   Y;
		public float Opacity;
		public ulong FrameNum;

		public TimePoint(ulong frameNum, int x, int y, float opacity)
		{
			X        = x;
			Y        = y;
			Opacity  = opacity;
			FrameNum = frameNum;
		}
	}
}