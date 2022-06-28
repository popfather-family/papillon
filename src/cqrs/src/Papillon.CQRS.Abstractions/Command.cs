namespace Papillon.CQRS;

public abstract record Command : ICommand
{
    Id ICommand.Id { get; } = Id.Generate();

    DateTime ICommand.CreatedOnUtc { get; } = DateTime.UtcNow;
}