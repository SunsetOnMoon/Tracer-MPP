using System;
using System.Linq;
using System.Text;

using YamlDotNet.Serialization;
using System.IO;
using Tracer.Serialization.Abstractions;
using Tracer.Core;
using System.Reflection.Metadata.Ecma335;

namespace Tracer.Serialization.Yaml
{
    class YamlSerializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult TraceResult, Stream stream)
        {
            var serializer = new SerializerBuilder().DisableAliases().Build();
            var result = serializer.Serialize(TraceResult);
            stream.Write(Encoding.UTF8.GetBytes(result));
        }

        public string Extension { get; } = "Yaml";

    }
}
