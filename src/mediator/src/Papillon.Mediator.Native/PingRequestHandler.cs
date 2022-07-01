namespace Papillon.Mediator.Native;

internal abstract class PingRequestHandler
{
    public abstract Task HandleAsync(object message,
                                     IServiceProvider serviceProvider,
                                     CancellationToken cancellationToken = default);
}

internal class PingRequestHandler<TMessage> : PingRequestHandler where TMessage : IMessage
{
    public override Task HandleAsync(object message,
                                     IServiceProvider serviceProvider,
                                     CancellationToken cancellationToken = default)
    {
        var handler = HandlerResolver.Resolve<IHandler<TMessage>>(serviceProvider);

        return handler.HandleAsync((TMessage)message, cancellationToken);
    }
}