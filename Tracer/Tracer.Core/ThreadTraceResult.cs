namespace Tracer.Core
{
    public class ThreadTraceResult
    {
        internal List<MethodTraceResult> internalMethods = new List<MethodTraceResult>();
        public IReadOnlyList<MethodTraceResult> Methods => internalMethods.AsReadOnly();
        public int ThreadID;
        public long ExecuteTime;
    }
}
