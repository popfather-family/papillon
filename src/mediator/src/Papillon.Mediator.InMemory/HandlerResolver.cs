namespace Papillon.Mediator.InMemory;

internal static class HandlerResolver
{
    public static THandler Resolve<THandler>(IServiceProvider serviceProvider)
    {
        var handler = serviceProvider.GetService(typeof(THandler));
        if (handler is null)
        {
            throw new HandlerNotFoundException(typeof(THandler).Name);
        }

        return (THandler)handler;
    }
}