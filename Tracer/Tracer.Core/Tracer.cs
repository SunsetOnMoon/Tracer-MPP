using System.Collections.Concurrent;
using System.Reflection;
using System.Diagnostics;

namespace Tracer.Core
{
    public class TracerSevice : ITracer
    {
        private ConcurrentDictionary<int, List<Stopwatch>> _exStack = new ConcurrentDictionary<int,List<Stopwatch>>();
        private TraceResult _traceInfo = new TraceResult();
        private ConcurrentDictionary<int, int> _methodStack = new ConcurrentDictionary<int, int>();
        
        private void Method(int ThreadID, string MethodName, string ClassName)
        {
            List<MethodTraceResult> _listMethod = new List<MethodTraceResult>();
            _listMethod = _traceInfo.Threads[ThreadID].Methods;
            for (int i = 1; i < _methodStack[ThreadID]; i++)
                _listMethod = _listMethod[_listMethod.Count - 1].Methods;
            MethodTraceResult _methodResult = new MethodTraceResult();
            _methodResult.MethodName = MethodName;
            _methodResult.MethodClassName = ClassName;
            _listMethod.Add(_methodResult); 
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];
            MethodInfo method = (MethodInfo)callingFrame.GetMethod();
            string methodName = method.Name;
            string classMethodName = method.DeclaringType.Name;
            ThreadTraceResult currentThread = new ThreadTraceResult();
            int threadID = Thread.CurrentThread.ManagedThreadId;

            if (_traceInfo.Threads.TryAdd(threadID, currentThread))
            {
                _traceInfo.Threads[threadID].ThreadID = threadID;
                _methodStack.TryAdd(threadID, 0);
                _exStack.TryAdd(threadID, new List<Stopwatch>());
            }

            _methodStack[threadID]++;
            _exStack[threadID].Add(new Stopwatch());
            Method(threadID, methodName, classMethodName);
            _exStack[threadID][_exStack[threadID].Count - 1].Start();

        }
        public void StopTrace()
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            _exStack[threadID][_exStack[threadID].Count - 1].Stop();
            List<MethodTraceResult> ListMethod = new List<MethodTraceResult>();
            ListMethod = _traceInfo.Threads[threadID].Methods;
            for (int i = 1; i < _methodStack[threadID]; i++)
                ListMethod = ListMethod[ListMethod.Count - 1].Methods;
            ListMethod[ListMethod.Count - 1].MethodExecTime = _exStack[threadID][_exStack[threadID].Count - 1].ElapsedMilliseconds;
            _exStack[threadID].Remove(_exStack[threadID][_exStack[threadID].Count - 1]);
            _methodStack[threadID]--;
        }

        public TraceResult GetTraceResult()
        {
            foreach (KeyValuePair<int, ThreadTraceResult> thread in _traceInfo.Threads)
            {
                long time = 0;
                foreach (MethodTraceResult Method in thread.Value.Methods)
                    time += Method.MethodExecTime;
                thread.Value.ExecuteTime = time;
            }
            return _traceInfo;
        }

    }
}
