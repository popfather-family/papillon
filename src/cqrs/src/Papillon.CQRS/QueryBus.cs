using Papillon.DI;
using Papillon.Mediator;

namespace Papillon.CQRS;

[Transient]
public class QueryBus : IQueryBus
{
    private readonly IMessageBus messageBus;

    public QueryBus(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return messageBus.SendAsync(query, cancellationToken);
    }
}