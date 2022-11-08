using Tracer.Core;
using Tracer.Core.Tests;
using Xunit;

namespace Tracer.Core.Tests
{
    public class TracerTests
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
            Assert.NotNull(result.Threads[0].Methods);
            Assert.Equal(1, result.Threads[0].Methods.Count);
            Assert.NotNull(result.Threads[0].Methods[0].Methods);
            Assert.Equal(2, result.Threads[0].Methods[0].Methods.Count);
            Assert.NotNull(result.Threads[0].Methods[0].Methods[0].Methods);
            Assert.NotNull(result.Threads[0].Methods[0].Methods[1].Methods);
            Assert.Equal(0, result.Threads[0].Methods[0].Methods[0].Methods.Count);
            Assert.Equal(0, result.Threads[0].Methods[0].Methods[1].Methods.Count);

            Assert.Equal("MyMethod", result.Threads[0].Methods[0].MethodName);
            Assert.Equal("FooSingleThread", result.Threads[0].Methods[0].MethodClassName);
            Assert.Equal("M1", result.Threads[0].Methods[0].Methods[0].MethodName);
            Assert.Equal("BarSingleThread", result.Threads[0].Methods[0].Methods[0].MethodClassName);
            Assert.Equal("M2", result.Threads[0].Methods[0].Methods[1].MethodName);
            Assert.Equal("BarSingleThread", result.Threads[0].Methods[0].Methods[1].MethodClassName);

            Assert.True(result.Threads[0].ExecuteTime >= 270);
            Assert.True(result.Threads[0].Methods[0].MethodExecTime >= 270);
            Assert.True(result.Threads[0].Methods[0].Methods[0].MethodExecTime >= 100);
            Assert.True(result.Threads[0].Methods[0].Methods[1].MethodExecTime >= 70);
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
            Assert.NotNull(result.Threads[0].Methods);
            Assert.NotNull(result.Threads[1].Methods);
            Assert.Equal(1, result.Threads[0].Methods.Count);
            Assert.Equal(1, result.Threads[1].Methods.Count);
            Assert.NotNull(result.Threads[0].Methods[0].Methods);
            Assert.Equal(0, result.Threads[0].Methods[0].Methods.Count);
            Assert.NotNull(result.Threads[1].Methods[0].Methods);
            Assert.Equal(1, result.Threads[1].Methods[0].Methods.Count);
            Assert.NotNull(result.Threads[1].Methods[0].Methods[0].Methods);
            Assert.Equal(0, result.Threads[1].Methods[0].Methods[0].Methods.Count);

            Assert.Equal("M1", result.Threads[0].Methods[0].MethodName);
            Assert.Equal("BarMultipleThreads", result.Threads[0].Methods[0].MethodClassName);
            Assert.Equal("MyMethod", result.Threads[1].Methods[0].MethodName);
            Assert.Equal("FooMultipleThreads", result.Threads[1].Methods[0].MethodClassName);
            Assert.Equal("M2", result.Threads[1].Methods[0].Methods[0].MethodName);
            Assert.Equal("BarMultipleThreads", result.Threads[1].Methods[0].Methods[0].MethodClassName);

            Assert.True(result.Threads[1].ExecuteTime >= 170);
            Assert.True(result.Threads[1].Methods[0].MethodExecTime >= 170);
            Assert.True(result.Threads[1].Methods[0].Methods[0].MethodExecTime >= 70);
            Assert.True(result.Threads[0].ExecuteTime >= 100);
            Assert.True(result.Threads[0].Methods[0].MethodExecTime >= 100);
        }
    }
}
