using Tracer.Core;
using System.Text.Json;
using System.Reflection;
using Tracer.Serialization.Abstractions;

public class Program
{
    public static void Main(string[] args)
    {
        var tracer = new TracerSevice();
        var foo = new FooMultipleThreads(tracer);
        foo.MyMethod();
        var result = tracer.GetTraceResult();

        foreach (var filePath in Directory.EnumerateFiles("Plugins", "*.dll"))
        {
            var assembly = Assembly.LoadFrom(filePath);

            var serializerNamespace = filePath.Split('\\')[1];
            serializerNamespace = serializerNamespace[..serializerNamespace.LastIndexOf('.')];
            var serializerTypeName = serializerNamespace.Split('.')[2];
            var fullName = $"{serializerNamespace}.{serializerTypeName}Serializer";

            var serializerType = assembly.GetType(fullName) ?? throw new TypeLoadException(fullName);
            var serializer = (ITraceResultSerializer)(Activator.CreateInstance(serializerType) ?? throw new TypeLoadException(fullName));

            using var stream = new FileStream($"serialize_result.{serializerTypeName.ToLower()}", FileMode.Create);
            serializer.Serialize(result, stream);
        }
    }
}

internal class BarMultipleThreads
{
    private readonly ITracer _tracer;

    public BarMultipleThreads(ITracer tracer)
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
