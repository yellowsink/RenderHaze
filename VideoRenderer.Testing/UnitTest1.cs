using System;
using System.IO;
using NUnit.Framework;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace VideoRenderer.Testing
{
	public class Tests
	{
		[SetUp]
		public void Setup() { }

		[Test]
		public void Test1()
		{
			var test1 = Image.Load<Rgba64>(Path.Combine(Environment.CurrentDirectory, "test_1.png"));
			var test2 = Image.Load<Rgba64>(Path.Combine(Environment.CurrentDirectory, "test_2.png"));
			
			var renderer = new FrameRenderer<Rgba64>();

			renderer.ObjectTimelines.Add(new Timeline<Rgba64>(new Object<Rgba64>(test2),
															  100,
															  new[]
															  {
																  new TimePoint(0, 0, 0, 1)
															  }));

			var timeline1 = new Timeline<Rgba64>(new Object<Rgba64>(test1),
												 100,
												 new[]
												 {
													 new TimePoint(0,  60,   50,  1),
													 new TimePoint(20, 500,  600, .75f),
													 new TimePoint(49, 200,  100, .9f),
													 new TimePoint(75, 1500, 500, .25f),
													 new TimePoint(99, 0,    0,   0)
												 });
			renderer.ObjectTimelines.Add(timeline1);

			renderer.RenderFramesToDisk(1920, 1080, Path.Combine(Environment.CurrentDirectory, "test_out"));
		}
	}
}