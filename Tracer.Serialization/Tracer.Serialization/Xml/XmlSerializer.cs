using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace Tracer.Serialization.Xml
{
    class XmlSerializer : ISerialization
    {
        public Stream Serialize(TraceResult TraceResult)
        {
            XmlDocument xmlDoc = new XmlDocument();
            System.IO.Stream stream = new System.IO.MemoryStream();
            XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlDec);
            XmlElement xmlRoot = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(xmlRoot);
            foreach (KeyValuePair<int, ThreadTraceResult> thread in TraceResult.Threads)
            {
                XmlElement xmlThreadElement = xmlDoc.CreateElement("thread");
                xmlThreadElement.SetAttribute("id", thread.Value.ThreadID.ToString());
                xmlThreadElement.SetAttribute("time", thread.Value.ExecuteTime.ToString() + "ms");
                GetInfo(thread.Value.Methods, xmlDoc, xmlThreadElement);
                xmlRoot.AppendChild(xmlThreadElement);
            }
            xmlDoc.Save(stream);
            return stream;
        }
    }
}
