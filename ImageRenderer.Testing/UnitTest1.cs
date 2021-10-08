using System;
using System.IO;
using NUnit.Framework;
using RenderHaze.ImageRenderer;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageRenderer.Testing
{
	public class Tests
	{
		[SetUp]
		public void Setup() { }

		[Test]
		public void Test1()
		{
			var renderer   = new Renderer<Rgba64>();
			var currentDir = Environment.CurrentDirectory;
			var test1      = Path.Combine(currentDir, "test_1.png");
			var test2      = Path.Combine(currentDir, "test_2.png");
			var test3      = Path.Combine(currentDir, "test_3.PNG");

			var baseImg = Image.Load<Rgba64>(test2);

			// base layer
			renderer.AddObject(baseImg, 0, 0, 1, 3.5f, 2, OriginPoint.TopLeft, OriginPoint.TopCenter);
			// rectangular transparent layer
			renderer.AddObject(test1, 5, 10, 0.5f, .5f, 2);
			// layer with a transparent image, aligned vertically centered, and snapped to the right
			renderer.AddObject(test3, baseImg.Width, baseImg.Height / 2, 1, 1, 1, OriginPoint.CenterRight);

			var result = renderer.Render(baseImg.Width, baseImg.Height);
			result.SaveAsPng(Path.Combine(currentDir, "out.png"));
			
			Assert.Pass("check the output yourself lol");
		}
	}
}