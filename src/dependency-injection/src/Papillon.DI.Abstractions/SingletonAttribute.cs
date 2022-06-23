namespace Papillon.DI;

public class SingletonAttribute : InjectableAttribute
{
    public SingletonAttribute() : base(InjectionMode.Singleton)
    {
    }
}