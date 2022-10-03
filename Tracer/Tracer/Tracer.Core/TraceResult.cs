using System.Collections.Concurrent;

namespace Tracer.Tracer.Core
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadTraceResult> Threads = new ConcurrentDictionary<int, ThreadTraceResult>();
    }
}
