using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Example
{
    interface IOutput
    {
        void ConsoleOutput(Stream stream);
        void FileOutput(Stream stream, string Filename);
    }
}
