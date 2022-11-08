using System.Threading;
using Tracer.Core;

namespace Tracer.Core.Tests
{
    internal class FooSingleThread
    {
        private readonly BarSingleThread _bar;
        private readonly ITracer _tracer;

        internal FooSingleThread(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new BarSingleThread(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _bar.M1();
            _bar.M2();
            _tracer.StopTrace();
        }
    }
}
