using System.Diagnostics;
using SkiaSharp;

namespace RenderHaze.VideoRenderer;

[DebuggerDisplay("{Image.Width}x{Image.Height} Image")]
public class RhObject
{
	public SKBitmap Image;

	public RhObject(SKBitmap image) { Image = image; }
}