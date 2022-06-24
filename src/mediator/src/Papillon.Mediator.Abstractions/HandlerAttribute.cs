using Papillon.DI;

namespace Papillon.Mediator;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerAttribute : TransientAttribute
{
}