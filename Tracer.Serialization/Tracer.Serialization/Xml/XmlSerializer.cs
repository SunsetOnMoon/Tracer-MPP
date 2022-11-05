using System.Xml;
using System.IO;
using Tracer.Tracer.Serialization.Abstractions;
using Tracer.Tracer.Core;

namespace Tracer.Serialization.Xml
{
    class XmlSerializer : ITraceResultSerializer
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

        static void GetInfo(List<MethodTraceResult> Methods, XmlDocument XmlDoc, XmlElement XmlMethod)
        {
            foreach (MethodTraceResult Method in Methods)
            {
                XmlElement xmlMethodElement = XmlDoc.CreateElement("method");
                xmlMethodElement.SetAttribute("name", Method.MethodName);
                xmlMethodElement.SetAttribute("time", Method.MethodExecTime.ToString() + "ms");
                xmlMethodElement.SetAttribute("class", Method.MethodClassName);
                if (Method.Methods.Count > 0)
                    GetInfo(Method.Methods, XmlDoc, xmlMethodElement);
                XmlMethod.AppendChild(xmlMethodElement);
            }
        }

        public string Extension { get; } = "Xml";
    }
}
