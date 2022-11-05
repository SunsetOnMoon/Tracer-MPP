using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Tracer.Example
{
    class JsonOutput : IOutput
    {
        public void ConsoleOutput(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            Console.WriteLine(json);
        }

        public void FileOutput(Stream stream, string filename)
        {
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default))
                sw.WriteLine(json);
        }
    }
}
