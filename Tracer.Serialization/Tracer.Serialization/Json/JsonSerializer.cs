
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Tracer.Tracer.Serialization.Abstractions;
using Tracer.Serialization;
using Tracer.Tracer.Core;

namespace Tracer.Serialization.Json
{
    class JsonSerializer : ITraceResultSerializer
    {
        public Stream Serialize(TraceResult TraceResult)
        {
            string buffer = JsonConvert.SerializeObject(TraceResult, Newtonsoft.Json.Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(buffer);
            System.IO.Stream stream = new System.IO.MemoryStream(byteArray);
            return stream;
        }

        public string Extension { get; } = "Json";
    }
}
