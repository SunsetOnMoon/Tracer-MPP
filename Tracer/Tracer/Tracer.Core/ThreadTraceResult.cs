namespace Tracer.Tracer.Core
{
    public class ThreadTraceResult
    {
        public List<MethodTraceResult> Methods = new List<MethodTraceResult>();
        public int ThreadID;
        public long ExecuteTime;
    }
}
