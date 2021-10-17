using System;

namespace RenderHaze.Mlt
{
    public class Producer
    {
        public const string ImageMltService = "qimage";
        public const string AudioMltService = "avformat";
        public const string VideoMltService = "avformat-novalidate";

        public string Id;
        public TimeSpan In;
        public TimeSpan Out;
        
        public int Length;
        public string Eof = "pause";
        public string Resource;
        public int? Ttl;
        public int? AspectRatio;
        public bool? Progressive;
        public bool Seekable = true;
        public int? MetaMediaWidth;
        public int? MetaMediaHeight;
        public int? MetaMediaNbStreams;
        public StreamMeta[] Streams;
        public int? AudioIndex;
        public int? VideoIndex;
        public bool? MuteOnPause;
        public string MltService;

        // TODO: oh god finish this why
    }

    public struct StreamMeta 
    {
        public string? StreamType;
        public string? CodecSampleFmt;
        public int? CodecSampleRate;
        public int? CodecChannels;
        public string? CodecName;
        public int? BitRate;
    }
}