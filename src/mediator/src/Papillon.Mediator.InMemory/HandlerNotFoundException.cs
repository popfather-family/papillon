namespace Papillon.Mediator.InMemory;

public class HandlerNotFoundException : ApplicationException
{
    public HandlerNotFoundException(string handlerName) : base($"No handler found for type '{handlerName}'.")
    {
    }
}