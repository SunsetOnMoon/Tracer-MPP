using Tracer.Core;

namespace Tracer.Serialization.Abstractions
{
    public interface ITraceResultSerializer
    {
        void Serialize(TraceResult TraceResult, Stream stream);

        public string Extension { get; }

    }
}
