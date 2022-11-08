using System.Threading;
using System.Threading.Tasks;
using Tracer.Core;

namespace Tracer.Core.Tests
{
    internal class FooMultipleThreads
    {
        private readonly BarMultipleThreads _bar;
        private readonly ITracer _tracer;

        internal FooMultipleThreads(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new BarMultipleThreads(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            var task = Task.Run(_bar.M1);
            _bar.M2();
            task.Wait();
            _tracer.StopTrace();
        }
    }
}