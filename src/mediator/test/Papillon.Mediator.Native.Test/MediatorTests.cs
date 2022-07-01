using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Papillon.Mediator.Native;

public class MediatorTests
{
    private static IMessageBus MessageBus
    {
        get
        {
            var serviceProvider = BuildServiceProviderMock();

            return new MessageBus(serviceProvider.Object);
        }
    }

    [Fact]
    public async Task Should_Invoke_Request_Handler()
    {
        var request = new PingRequest();
        await MessageBus.SendAsync(request);
    }

    [Fact]
    public async Task Should_Invoke_Request_Handlers()
    {
        var pingRequest = new PingRequest();
        await MessageBus.SendAsync(pingRequest);

        var pongRequest = new PongRequest();
        await MessageBus.SendAsync(pongRequest);
    }

    [Fact]
    public async Task Should_Invoke_Request_Response_Handler()
    {
        var request = new PingPongRequest();
        _ = await MessageBus.SendAsync(request);
    }

    private static Mock<IServiceProvider> BuildServiceProviderMock()
    {
        var serviceProvider = new Mock<IServiceProvider>();

        serviceProvider.Setup(c => c.GetService(typeof(IHandler<PingRequest>)))
                       .Returns(() => new PingRequestHandler());

        serviceProvider.Setup(c => c.GetService(typeof(IHandler<PongRequest>)))
                       .Returns(() => new PongRequestHandler());

        serviceProvider.Setup(c => c.GetService(typeof(IHandler<PingPongRequest, PingPongResponse>)))
                       .Returns(() => new PingPongRequestHandler());

        return serviceProvider;
    }

    private class PingRequest : IMessage
    {
    }

    private class PingRequestHandler : IHandler<PingRequest>
    {
        public Task HandleAsync(PingRequest request, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    private class PongRequest : IMessage
    {
    }

    private class PongRequestHandler : IHandler<PongRequest>
    {
        public Task HandleAsync(PongRequest request, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    private class PingPongRequest : IMessage<PingPongResponse>
    {
    }

    private class PingPongResponse
    {
    }

    private class PingPongRequestHandler : IHandler<PingPongRequest, PingPongResponse>
    {
        public Task<PingPongResponse> HandleAsync(PingPongRequest request,
                                                  CancellationToken cancellationToken = default)
        {
            var response = new PingPongResponse();

            return Task.FromResult(response);
        }
    }
}