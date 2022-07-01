using System.Collections.Concurrent;

namespace Papillon.Mediator.Native;

internal static class HandlerFactory
{
    private static readonly ConcurrentDictionary<Type, object> Cache = new();

    public static PingRequestHandler Create(Type handlerType)
    {
        return (PingRequestHandler)Cache.GetOrAdd(handlerType, CreateInstance(handlerType));
    }

    public static PingPongRequestHandler<TResponse> Create<TResponse>(Type handlerType)
    {
        return (PingPongRequestHandler<TResponse>)Cache.GetOrAdd(handlerType, CreateInstance(handlerType));
    }

    private static object CreateInstance(Type handlerType) => Activator.CreateInstance(handlerType)!;
}