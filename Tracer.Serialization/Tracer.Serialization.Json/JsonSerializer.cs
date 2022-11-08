
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Tracer.Serialization.Abstractions;
using Tracer.Serialization;
using Tracer.Core;
using System.Text.Json;

namespace Tracer.Serialization.Json
{
    class JsonSerializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult TraceResult, Stream stream)
        {
            string buffer = JsonConvert.SerializeObject(TraceResult, Newtonsoft.Json.Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(buffer);
            stream.Write(byteArray);
        }

        public string Extension { get; } = "Json";
    }
}
