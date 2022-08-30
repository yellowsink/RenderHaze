using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SkiaSharp;

namespace RenderHaze.ImageRenderer;

[DebuggerDisplay("Batch Renderer with {Renderers.Count} individual renderers")]
public class BatchRenderer : IDisposable
{
	public IList<Renderer> Renderers = new List<Renderer>();

	public BatchRenderer(IList<Renderer> renderers) { Renderers = renderers; }

	public BatchRenderer() { }

	public void Dispose()
	{
		foreach (var r in Renderers) r.Dispose();
	}

	public SKImage[] RenderAll(int width, int height)
		=> Renderers.Select(r => r.Render(width, height)).ToArray();

	public void RenderAllToFiles(int width, int height, string[] filenames, EventHandler<(int, int)>? progress)
	{
		if (filenames.Length != Renderers.Count)
			throw new ArgumentException("Please provide one filename per renderer", nameof(filenames));

		var rendererPairs = Renderers.Zip(filenames, (f, s) => (f, s)).ToArray();

		var finished = 0;

		BatchTaskUnordered(rendererPairs,
						   arg =>
						   {
							   arg.f.Render(width, height).SavePng(arg.s);
							   progress?.Invoke(this, (++finished, rendererPairs.Length));
						   })
		   .Wait();
	}

	private static Task BatchTaskUnordered<T>(IEnumerable<T> items, Action<T> processFunc)
		=> Task.WhenAll(items.Select(e => Task.Run(() => processFunc(e))));
}