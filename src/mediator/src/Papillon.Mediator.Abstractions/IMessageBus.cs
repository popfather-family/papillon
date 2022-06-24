namespace Papillon.Mediator;

public interface IMessageBus
{
    Task SendAsync(IMessage message, CancellationToken cancellationToken = default);

    Task<TResponse> SendAsync<TResponse>(IMessage<TResponse> message, CancellationToken cancellationToken = default);
}