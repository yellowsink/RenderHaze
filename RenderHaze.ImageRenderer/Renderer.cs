using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RenderHaze.ImageRenderer
{
	public class Renderer<TPixel> : IDisposable where TPixel : unmanaged, IPixel<TPixel>
	{
		public List<Layer<TPixel>> Layers = new();
		
		public Renderer(List<Layer<TPixel>> layers) => Layers = layers;
		public Renderer() {}

		public Image<TPixel> Render(int width, int height)
		{
			var baseLayer = new Image<TPixel>(width, height);
			
			foreach (var layer in Layers) LayerImage(ref baseLayer, PreProcessLayer(layer));

			return baseLayer;
		}

		private static void LayerImage(ref Image<TPixel> baseI, Layer<TPixel> layer)
			=> baseI.Mutate(x =>
			{
				x.DrawImage(layer.Image, new Point(layer.OffsetX, layer.OffsetY), layer.Opacity);
			});

		private static Layer<TPixel> PreProcessLayer(Layer<TPixel> layer)
		{
			var processedLayer = layer;
			var img            = layer.Image;

			img.Mutate(x =>
			{
				var (scaleOx, scaleOy)   =  LayerProcessing.Scale(x, layer);
				var (originOx, originOy) =  LayerProcessing.CalculateOriginOffset(x, layer);
				processedLayer.OffsetX   += (int) scaleOx + originOx;
				processedLayer.OffsetY   += (int) scaleOy + originOy;
			});
			
			processedLayer.Image = img;
			return processedLayer;
		}

		public void Dispose()
		{
			foreach (var t in Layers) t.Image.Dispose();
		}
	}
}