namespace Papillon.Mediator.InMemory;

[Transient]
public class MessageBus : IMessageBus
{
    private readonly IServiceProvider serviceProvider;

    public MessageBus(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task SendAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        var messageType = message.GetType();
        var handlerType = HandlerTypeFactory.Create(messageType);
        var handler = HandlerFactory.Create(handlerType);

        await handler.HandleAsync(message, serviceProvider, cancellationToken)
                     .ConfigureAwait(false);
    }

    public async Task<TResponse> SendAsync<TResponse>(IMessage<TResponse> message,
                                                      CancellationToken cancellationToken = default)
    {
        var messageType = message.GetType();
        var handlerType = HandlerTypeFactory.Create<TResponse>(messageType);
        var handler = HandlerFactory.Create<TResponse>(handlerType);

        return await handler.HandleAsync(message, serviceProvider, cancellationToken)
                            .ConfigureAwait(false);
    }
}