namespace Papillon.CQRS;

[Transient]
public class ServiceBus : IServiceBus
{
    private readonly IMessageBus messageBus;

    public ServiceBus(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        return messageBus.SendAsync(command, cancellationToken);
    }

    public Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return messageBus.SendAsync(query, cancellationToken);
    }
}