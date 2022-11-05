using Tracer.Tracer.Core;

namespace Tracer.Tracer.Serialization.Abstractions
{
    public interface ITraceResultSerializer
    {
        Stream Serialize(TraceResult TraceResult);

        public string Extension { get; }

    }
}
