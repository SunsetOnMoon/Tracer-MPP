namespace Tracer.Serialization
{
    interface ISerialization
    {
        Stream Serialize(TraceResult TraceResult);
    }
}