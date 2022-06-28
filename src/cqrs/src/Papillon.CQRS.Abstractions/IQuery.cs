using Papillon.Mediator;

namespace Papillon.CQRS;

public interface IQuery<out TResponse> : IMessage<TResponse>
{
}