namespace Papillon.DI;

[AttributeUsage(AttributeTargets.Class)]
public abstract class InjectableAttribute : Attribute
{
    protected InjectableAttribute(InjectionMode mode)
    {
        Mode = mode;
    }

    public InjectionMode Mode { get; }
}