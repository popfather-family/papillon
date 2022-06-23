namespace Papillon.DI;

public class ScopedAttribute : InjectableAttribute
{
    public ScopedAttribute() : base(InjectionMode.Scoped)
    {
    }
}