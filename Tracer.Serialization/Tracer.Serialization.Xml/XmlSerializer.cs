using System.Xml;
using System.IO;
using Tracer.Serialization.Abstractions;
using Tracer.Core;

namespace Tracer.Serialization.Xml
{
    class XmlSerializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult TraceResult, Stream stream)
        {
            XmlDocument xmlDoc = new XmlDocument();
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
