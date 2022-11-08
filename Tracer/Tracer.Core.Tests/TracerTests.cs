using Tracer.Core;
using Tracer.Core.Tests;

namespace Tracer.Core.Tests
{
    public class TracerServiceTests
    {
        [Fact]
        public void SingleThread()
        {
            var tracer = new TracerSevice();
            var foo = new FooSingleThread(tracer);

            foo.MyMethod();
            var result = tracer.GetTraceResult();

            Assert.NotNull(result);
            Assert.NotNull(result.Threads);
            Assert.Equal(1, result.Threads.Count);
            var thread = result.Threads.Select(t => t.Value).First();
            Assert.NotNull(thread.Methods);
            Assert.Equal(1, thread.Methods.Count);
            Assert.NotNull(thread.Methods[0].Methods);
            Assert.Equal(2, thread.Methods[0].Methods.Count);
            Assert.NotNull(thread.Methods[0].Methods[0].Methods);
            Assert.NotNull(thread.Methods[0].Methods[1].Methods);
            Assert.Equal(0, thread.Methods[0].Methods[0].Methods.Count);
            Assert.Equal(0, thread.Methods[0].Methods[1].Methods.Count);

            Assert.Equal("MyMethod", thread.Methods[0].MethodName);
            Assert.Equal("FooSingleThread", thread.Methods[0].MethodClassName);
            Assert.Equal("M1", thread.Methods[0].Methods[0].MethodName);
            Assert.Equal("BarSingleThread", thread.Methods[0].Methods[0].MethodClassName);
            Assert.Equal("M2", thread.Methods[0].Methods[1].MethodName);
            Assert.Equal("BarSingleThread", thread.Methods[0].Methods[1].MethodClassName);

            Assert.True(thread.ExecuteTime >= 270);
            Assert.True(thread.Methods[0].MethodExecTime >= 270);
            Assert.True(thread.Methods[0].Methods[0].MethodExecTime >= 100);
            Assert.True(thread.Methods[0].Methods[1].MethodExecTime >= 70);
        }

        [Fact]
        public void MultipleThreads()
        {
            var tracer = new TracerSevice();
            var foo = new FooMultipleThreads(tracer);

            foo.MyMethod();
            var result = tracer.GetTraceResult();

            Assert.NotNull(result);
            Assert.NotNull(result.Threads);
            Assert.Equal(2, result.Threads.Count);
            var threads = result.Threads.Select(t => t.Value).ToList();
            Assert.NotNull(threads[0].Methods);
            Assert.NotNull(threads[1].Methods);
            Assert.Equal(1, threads[0].Methods.Count);
            Assert.Equal(1, threads[1].Methods.Count);
            Assert.NotNull(threads[0].Methods[0].Methods);
            Assert.Equal(0, threads[0].Methods[0].Methods.Count);
            Assert.NotNull(threads[1].Methods[0].Methods);
            Assert.Equal(1, threads[1].Methods[0].Methods.Count);
            Assert.NotNull(threads[1].Methods[0].Methods[0].Methods);
            Assert.Equal(0, threads[1].Methods[0].Methods[0].Methods.Count);

            Assert.Equal("M1", threads[0].Methods[0].MethodName);
            Assert.Equal("BarMultipleThreads", threads[0].Methods[0].MethodClassName);
            Assert.Equal("MyMethod", threads[1].Methods[0].MethodName);
            Assert.Equal("FooMultipleThreads", threads[1].Methods[0].MethodClassName);
            Assert.Equal("M2", threads[1].Methods[0].Methods[0].MethodName);
            Assert.Equal("BarMultipleThreads", threads[1].Methods[0].Methods[0].MethodClassName);

            Assert.True(threads[1].ExecuteTime >= 170);
            Assert.True(threads[1].Methods[0].MethodExecTime >= 170);
            Assert.True(threads[1].Methods[0].Methods[0].MethodExecTime >= 70);
            Assert.True(threads[0].ExecuteTime >= 100);
            Assert.True(threads[0].Methods[0].MethodExecTime >= 100);
        }
    }
}