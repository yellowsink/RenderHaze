using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace VideoRenderer
{
	public static class Encoder
	{
		public static async Task Encode(string[] files, string outfile, double framerate, string? audioPath)
		{
			var conv = FFmpeg.Conversions.New()
							 .SetInputFrameRate(framerate)
							 .BuildVideoFromImages(files)
							 .SetFrameRate(framerate)
							 .SetOutput(outfile)
							 .SetPixelFormat(PixelFormat.yuv420p);
			if (!string.IsNullOrWhiteSpace(audioPath))
			{
				var info        = await FFmpeg.GetMediaInfo(audioPath);
				var audioStream = info.AudioStreams.FirstOrDefault()?.SetChannels(2);
				conv = conv.AddStream(audioStream);
			}

			await conv.Start();
		}
	}
}