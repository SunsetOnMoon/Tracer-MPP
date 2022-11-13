using System.Collections.Concurrent;
using System.Collections;
using System.Reflection;

namespace Tracer.Core
{
    public class TraceResult  
    {

        internal ConcurrentDictionary<int, ThreadTraceResult> internalThreads = new ConcurrentDictionary<int, ThreadTraceResult>();
        public IReadOnlyDictionary<int, ThreadTraceResult> Threads => internalThreads;
        //internal ConcurrentDictionary<int, ThreadTraceResult> internalThreads = new ConcurrentDictionary<int, ThreadTraceResult>();

    }
}
