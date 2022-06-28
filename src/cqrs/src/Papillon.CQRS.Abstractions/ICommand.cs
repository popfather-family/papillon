using Papillon.Mediator;

namespace Papillon.CQRS;

public interface ICommand : IMessage
{
    Id Id { get; }

    DateTime CreatedOnUtc { get; }
}