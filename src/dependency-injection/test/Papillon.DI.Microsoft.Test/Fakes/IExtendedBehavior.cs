namespace Papillon.DI.Microsoft.Fakes;

public interface IExtendedBehavior : IBehavior
{
}

[Extensible]
public interface IExtendedBehavior<T> : IBehavior<T>
{
}