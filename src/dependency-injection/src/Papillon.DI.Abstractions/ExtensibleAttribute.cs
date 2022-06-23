namespace Papillon.DI;

[AttributeUsage(AttributeTargets.Interface)]
public class ExtensibleAttribute : InjectableAttribute
{
    public ExtensibleAttribute() : base(InjectionMode.Transient)
    {
    }
}