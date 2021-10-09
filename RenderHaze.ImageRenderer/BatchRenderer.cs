using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RenderHaze.ImageRenderer
{
	public class BatchRenderer<TPixel> : IDisposable where TPixel : unmanaged, IPixel<TPixel>
	{
		public List<Renderer<TPixel>> Renderers = new();
		
		public BatchRenderer(List<Renderer<TPixel>> renderers) => Renderers = renderers;
		public BatchRenderer() {}

		public Image<TPixel>[] RenderAll(int width, int height)
			=> Renderers
			  .Select(r => r.Render(width, height))
			  .ToArray();

		public void RenderAllToFiles(int width, int height, string[] filenames, EventHandler<(int, int)>? progress)
		{
			if (filenames.Length != Renderers.Count)
				throw new ArgumentException("Please provide one filename per renderer", nameof(filenames));
			
			var rendererPairs = Renderers.Zip(filenames, (f,s) => (f, s)).ToArray();

			BatchTaskUnordered(rendererPairs, args =>
			{
				args.Item.f.Render(width, height).Save(args.Item.s);
				progress?.Invoke(this, (args.Index, rendererPairs.Length));
			}, 8);
		}
		
		// taken from https://github.com/yellowsink/sinkbox/blob/2817fd99793f5bfb7bd9ddd8811cfc0159796c61/Sinkbox/Threading.cs#L43 and modified
		private static void BatchTaskUnordered<T>(T[] items, Action<(int Index, T Item)> processFunc, int threads)
		{
			if (items.Length < threads) threads = items.Length;

			var threadBatches = new List<(int, T)>?[threads];
			for (var i = 0; i < items.Length; i++)
			{
				threadBatches[i % threads] ??= new List<(int, T)>();

				threadBatches[i % threads]!.Add((i, items[i]));
			}

			var threadTasks = new Task[threads];
			for (var i = 0; i < threadBatches.Length; i++)
			{
				// c# scoping rules fun
				var i1 = i;
				threadTasks[i] = Task.Run(() => QueueProcess(threadBatches[i1]!, processFunc));
			}

			Task.WaitAll(threadTasks.ToArray());

			static void QueueProcess(IEnumerable<(int, T)> items, Action<(int, T)> func)
			{
				foreach (var item in items) func(item);
			}
		}

		public void Dispose()
		{
			foreach (var r in Renderers) r.Dispose();
		}
	}
}