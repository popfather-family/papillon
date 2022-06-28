namespace Papillon.Mediator;

[Extensible]
public interface IHandler<in TMessage>
{
    Task HandleAsync(TMessage message, CancellationToken cancellationToken = default);
}

[Extensible]
public interface IHandler<in TMessage, TResponse>
{
    Task<TResponse> HandleAsync(TMessage message, CancellationToken cancellationToken = default);
}