namespace Papillon.DI.Microsoft.Fakes;

public class ExtendedBehavior : IExtendedBehavior
{
}

public class ExtendedGenericBehavior : IExtendedBehavior<Behavior>
{
}

[Scoped]
public class ScopedBehavior : IScopedBehavior
{
}