namespace Papillon.CQRS;

[Transient]
public class CommandBus : ICommandBus
{
    private readonly IMessageBus messageBus;

    public CommandBus(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        return messageBus.SendAsync(command, cancellationToken);
    }
}