namespace Papillon.DI;

public class TransientAttribute : InjectableAttribute
{
    public TransientAttribute() : base(InjectionMode.Transient)
    {
    }
}