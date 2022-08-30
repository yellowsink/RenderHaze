using System;
using System.Collections.Generic;
using System.Diagnostics;
using SkiaSharp;

namespace RenderHaze.ImageRenderer;

[DebuggerDisplay("Renderer with {Layers.Count} layers")]
public class Renderer : IDisposable
{
	public IList<Layer> Layers = new List<Layer>();

	public Renderer(IList<Layer> layers) { Layers = layers; }

	public Renderer() { }

	public void Dispose()
	{
		foreach (var t in Layers) t.Dispose();
	}

	public SKImage Render(int width, int height)
	{
		var surface = SKSurface.Create(new SKImageInfo(width, height));

		foreach (var layer in Layers) LayerImage(surface.Canvas, PreProcessLayer(layer));

		return surface.Snapshot();
	}

	private static void LayerImage(SKCanvas canvas, Layer layer)
		=> SKSurface.Create(layer.Image.PeekPixels())
					.Draw(canvas,
						  layer.OffsetX,
						  layer.OffsetY,
						  new SKPaint
						  {
							  Color = SKColor.Empty.WithAlpha((byte) (layer.Opacity * byte.MaxValue))
						  });

	private static Layer PreProcessLayer(Layer layer)
	{
		var scaleOffset = ImageProcessing.Scale(layer);
		var posOffset   = ImageProcessing.CalculateOriginOffset(layer);
		layer.OffsetX += posOffset.x + scaleOffset.x;
		layer.OffsetY += posOffset.y + scaleOffset.y;

		return layer;
	}
}