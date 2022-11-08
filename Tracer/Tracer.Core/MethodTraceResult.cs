namespace Tracer.Core
{
    public class MethodTraceResult
    {
        public string MethodName;
        public string MethodClassName;
        public long MethodExecTime;
        public List<MethodTraceResult> Methods = new List<MethodTraceResult>();
    }
}
