using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YamlDotNet.Serialization;
using System.IO;
using Tracer.Tracer.Serialization.Abstractions;
using Tracer.Tracer.Core;
using System.Reflection.Metadata.Ecma335;

namespace Tracer.Serialization.Yaml
{
    class YamlSerializer : ITraceResultSerializer
    {
        public Stream Serialize(TraceResult TraceResult)
        {
            Stream stream = new MemoryStream();
            var serializer = new SerializerBuilder().DisableAliases().Build();
            var result = serializer.Serialize(TraceResult);
            stream.Write(Encoding.UTF8.GetBytes(result));
            return stream;

        }

        public string Extension { get; } = "Yaml";

    }
}
