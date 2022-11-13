namespace Tracer.Core
{
    public class MethodTraceResult
    {

        internal List<MethodTraceResult> internalMethods = new List<MethodTraceResult>();     //MethodTraceResult method = new MethodTraceResult();
        public string MethodName;
        public string MethodClassName;
        public long MethodExecTime;
        public IReadOnlyList<MethodTraceResult> Methods => internalMethods.AsReadOnly();
    }
}
