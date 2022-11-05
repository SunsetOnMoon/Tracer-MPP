
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Tracer.Serialization.Abstractions;
using Tracer.Serialization;

namespace Tracer.Serialization.Json
{
    class JsonSerializer : ISerialization
    {
        public Stream Serialize(TraceResult TraceResult)
        {
            string buffer = JsonConvert.SerializeObject(TraceResult, Newtonsoft.Json.Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(buffer);
            System.IO.Stream stream = new System.IO.MemoryStream(byteArray);
            return stream;
        }
    }
}
