namespace Papillon.CQRS;

public static class CommandExtensions
{
    public static Id GetIdentity(this Command command) => ((ICommand)command).Id;
}