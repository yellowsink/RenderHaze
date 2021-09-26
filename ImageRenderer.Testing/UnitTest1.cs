using System;
using System.IO;
using NUnit.Framework;
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

			var baseImage = Image.Load<Rgba64>(test2);

			renderer.AddObject(baseImage, 0, 0, 1); // base layer
			renderer.AddObject(test1, 5, 10, 0.5f); // add a rectangular transparent layer
			renderer.AddObject(test3, 200, 300, 1); // add a layer with transparency in the image

			var result = renderer.Render(baseImage.Width, baseImage.Height);
			result.SaveAsPng(Path.Combine(currentDir, "out.png"));
			
			Assert.Pass("check the output yourself lol");
		}
	}
}