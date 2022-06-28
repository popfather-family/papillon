namespace Papillon.CQRS;

public interface ICommandBus
{
    Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default);
}