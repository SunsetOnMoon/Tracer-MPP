using System.Threading;
using Tracer.Core;

namespace Tracer.Core.Tests
{
    internal class BarSingleThread
    {
        private readonly ITracer _tracer;

        public BarSingleThread(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        public void M2()
        {
            _tracer.StartTrace();
            Thread.Sleep(70);
            _tracer.StopTrace();
        }
    }
}