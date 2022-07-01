using System.Runtime.ExceptionServices;

namespace Papillon.Mediator.Native;

internal abstract class PingPongRequestHandler<TResponse>
{
    public abstract Task<TResponse> HandleAsync(object message,
                                                IServiceProvider serviceProvider,
                                                CancellationToken cancellationToken = default);
}

internal class PingPongRequestHandler<TMessage, TResponse> : PingPongRequestHandler<TResponse>
    where TMessage : IMessage<TResponse>
{
    public override Task<TResponse> HandleAsync(object message,
                                                IServiceProvider serviceProvider,
                                                CancellationToken cancellationToken = default)
    {
        var handler = HandlerResolver.Resolve<IHandler<TMessage, TResponse>>(serviceProvider);
        var handleTask = handler.HandleAsync((TMessage)message, cancellationToken);

        return handleTask.ContinueWith(c =>
                                       {
                                           if (c.IsFaulted && c.Exception?.InnerException is not null)
                                           {
                                               ExceptionDispatchInfo.Capture(c.Exception.InnerException)
                                                                    .Throw();
                                           }

                                           return c.Result;
                                       },
                                       cancellationToken);
    }
}