namespace Papillon.CQRS;

public interface IQueryBus
{
    Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query,
                                          CancellationToken cancellationToken = default);
}