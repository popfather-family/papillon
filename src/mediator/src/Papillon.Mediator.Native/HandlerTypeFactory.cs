namespace Papillon.Mediator.Native;

internal static class HandlerTypeFactory
{
    public static Type Create(Type requestType)
    {
        return typeof(PingRequestHandler<>).MakeGenericType(requestType);
    }

    public static Type Create<TResponse>(Type requestType)
    {
        return typeof(PingPongRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
    }
}