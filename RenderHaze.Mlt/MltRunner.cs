using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RenderHaze.Mlt
{
    public static class MltRunner
    {
        public static void Serialize(XmlTypes.Mlt xmlRoot, Stream destination)
        {
            var serializer = new XmlSerializer(typeof(XmlTypes.Mlt));
            var writer = new StreamWriter(destination);
            serializer.Serialize(writer, xmlRoot);
            writer.Close();
        }
    }
}