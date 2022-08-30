using System;
using System.IO;
using NUnit.Framework;
using RenderHaze.VideoRenderer;
using SkiaSharp;

namespace VideoRenderer.Testing;

public class Tests
{
	[Test]
	public void Test1()
	{
		File.Delete(Path.Combine(Environment.CurrentDirectory, "test_out.mp4"));

		var test1 = SKBitmap.Decode(Path.Combine(Environment.CurrentDirectory, "test_1.png"));
		var test2 = SKBitmap.Decode(Path.Combine(Environment.CurrentDirectory, "test_2.png"));

		var bgTimeline = new Timeline(new RhObject(test2),
									  100,
									  new[]
									  {
										  new TimePoint(0, 0, 0, 1)
									  });

		var timeline1 = new Timeline(new RhObject(test1),
									 5,
									 new[]
									 {
										 new TimePoint(0,  60,   50,  1),
										 new TimePoint(20, 500,  600, .75f),
										 new TimePoint(49, 200,  100, .9f),
										 new TimePoint(75, 1500, 500, .25f),
										 new TimePoint(99, 0,    0,   0)
									 });

		var timelines = new[] { bgTimeline, timeline1 };

		var supervisor = new VideoRenderSupervisor(timelines, 1920, 1080, 30);

		supervisor.RenderVideoTo(Path.Combine(Environment.CurrentDirectory, "test_out.mp4"), null, null);
		
		Assert.Pass("check the output yourself lol");
	}
}