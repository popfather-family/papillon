using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Papillon.CQRS;

public class CommandBusTests
{
    [Fact]
    public async Task Should_Find_CommandHandler()
    {
        var command = new CustomCommand();
        var handler = new CustomCommandHandler();
        var messageBus = CreateMessageBus(command, handler);
        var commandBus = new CommandBus(messageBus);
        await commandBus.ExecuteAsync(command);

        var capturedCommand = DataSource.Items[command.GetIdentity()];

        Assert.NotNull(capturedCommand);
    }

    private static IMessageBus CreateMessageBus(CustomCommand command, CustomCommandHandler handler)
    {
        var mock = new Mock<IMessageBus>();

        mock.Setup(c => c.SendAsync(command, default))
            .Returns(() => handler.HandleAsync(command));

        return mock.Object;
    }

    private record CustomCommand : Command;

    private class CustomCommandHandler : ICommandHandler<CustomCommand>
    {
        public Task HandleAsync(CustomCommand command, CancellationToken cancellationToken = default)
        {
            DataSource.Items.Add(command.GetIdentity(), "Papillon");

            return Task.CompletedTask;
        }
    }

    private static class DataSource
    {
        public static readonly Dictionary<Id, string> Items = new();
    }
}