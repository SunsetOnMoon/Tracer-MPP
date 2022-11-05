using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tracer.Tracer.Example
{
    class XmlOutput : IOutput
    {
        public void ConsoleOutput(Stream stream)
        {
            XmlDocument xmlDoc = new XmlDocument();
            stream.Position = 0;
            xmlDoc.Load(stream);
            xmlDoc.Save(Console.Out);
        }

        public void FileOutput(Stream stream, string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            stream.Position = 0;
            xmlDoc.Load(stream);
            xmlDoc.Save(filename);
        }
    }
}
