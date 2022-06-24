namespace Papillon.Mediator;

public interface IMessage
{
}

public interface IMessage<out TResponse> : IMessage
{
}