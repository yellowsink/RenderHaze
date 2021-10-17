using System;
using System.Xml.Serialization;
using System.Collections.Generic;

#pragma warning disable CS8618

// https://json2csharp.com/xml-to-csharp

namespace RenderHaze.Mlt
{
    [XmlRoot(ElementName = "profile")]
    public class Profile
    {

        [XmlAttribute(AttributeName = "frame_rate_num")]
        public int FrameRateNum;

        [XmlAttribute(AttributeName = "sample_aspect_num")]
        public int SampleAspectNum;

        [XmlAttribute(AttributeName = "display_aspect_den")]
        public int DisplayAspectDen;

        [XmlAttribute(AttributeName = "colorspace")]
        public int Colorspace;

        [XmlAttribute(AttributeName = "progressive")]
        public int Progressive;

        [XmlAttribute(AttributeName = "description")]
        public string Description;

        [XmlAttribute(AttributeName = "display_aspect_num")]
        public int DisplayAspectNum;

        [XmlAttribute(AttributeName = "frame_rate_den")]
        public int FrameRateDen;

        [XmlAttribute(AttributeName = "width")]
        public int Width;

        [XmlAttribute(AttributeName = "height")]
        public int Height;

        [XmlAttribute(AttributeName = "sample_aspect_den")]
        public int SampleAspectDen;
    }

    [XmlRoot(ElementName = "property")]
    public class Property
    {

        [XmlAttribute(AttributeName = "name")]
        public string Name;

        [XmlText]
        public int Text;
    }

    [XmlRoot(ElementName = "producer")]
    public class Producer
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlAttribute(AttributeName = "in")]
        public DateTime In;

        [XmlAttribute(AttributeName = "out")]
        public DateTime Out;

        [XmlText]
        public string Text;
    }

    [XmlRoot(ElementName = "entry")]
    public class Entry
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlElement(ElementName = "filter")]
        public List<Filter> Filter;

        [XmlAttribute(AttributeName = "producer")]
        public string Producer;

        [XmlAttribute(AttributeName = "in")]
        public DateTime In;

        [XmlAttribute(AttributeName = "out")]
        public DateTime Out;

        [XmlText]
        public string Text;
    }

    [XmlRoot(ElementName = "playlist")]
    public class Playlist
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlElement(ElementName = "entry")]
        public List<Entry> Entry;

        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlText]
        public string Text;

        [XmlElement(ElementName = "blank")]
        public List<Blank> Blank;
    }

    [XmlRoot(ElementName = "filter")]
    public class Filter
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlText]
        public string Text;

        [XmlAttribute(AttributeName = "out")]
        public DateTime Out;
    }

    [XmlRoot(ElementName = "blank")]
    public class Blank
    {

        [XmlAttribute(AttributeName = "length")]
        public DateTime Length;
    }

    [XmlRoot(ElementName = "track")]
    public class Track
    {

        [XmlAttribute(AttributeName = "hide")]
        public string Hide;

        [XmlAttribute(AttributeName = "producer")]
        public string Producer;
    }

    [XmlRoot(ElementName = "tractor")]
    public class Tractor
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlElement(ElementName = "track")]
        public List<Track> Track;

        [XmlElement(ElementName = "filter")]
        public List<Filter> Filter;

        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlAttribute(AttributeName = "in")]
        public DateTime In;

        [XmlAttribute(AttributeName = "out")]
        public DateTime Out;

        [XmlText]
        public string Text;

        [XmlElement(ElementName = "transition")]
        public List<Transition> Transition;
    }

    [XmlRoot(ElementName = "transition")]
    public class Transition
    {

        [XmlElement(ElementName = "property")]
        public List<Property> Property;

        [XmlAttribute(AttributeName = "id")]
        public string Id;

        [XmlText]
        public string Text;
    }

    [XmlRoot(ElementName = "mlt")]
    public class Mlt
    {

        [XmlElement(ElementName = "profile")]
        public Profile Profile;

        [XmlElement(ElementName = "producer")]
        public List<Producer> Producer;

        [XmlElement(ElementName = "playlist")]
        public List<Playlist> Playlist;

        [XmlElement(ElementName = "tractor")]
        public List<Tractor> Tractor;

        [XmlAttribute(AttributeName = "LC_NUMERIC")]
        public string LCNUMERIC;

        [XmlAttribute(AttributeName = "version")]
        public string Version;

        [XmlAttribute(AttributeName = "root")]
        public string Root;

        [XmlText]
        public string Text;
    }
}
