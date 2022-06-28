using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Papillon.Mediator;
using Xunit;

namespace Papillon.CQRS;

public class ServiceBusTests
{
    [Fact]
    public async Task Should_Find_Routes()
    {
        var command = new CustomCommand();
        var commandHandler = new CustomCommandHandler();

        var query = new Query();
        var queryHandler = new QueryHandler();

        var messageBus = CreateMessageBus(command, commandHandler, query, queryHandler);
        var serviceBus = new ServiceBus(messageBus);

        await serviceBus.ExecuteAsync(command);
        var queryResponse = await serviceBus.QueryAsync(query);

        var capturedCommand = DataSource.Items[command.GetIdentity()];

        Assert.NotNull(capturedCommand);
        Assert.Equal(query.ExpectedResponse, queryResponse);
    }

    private static IMessageBus CreateMessageBus(CustomCommand command,
                                                CustomCommandHandler commandHandler,
                                                Query query,
                                                QueryHandler queryHandler)
    {
        var mock = new Mock<IMessageBus>();

        mock.Setup(c => c.SendAsync(command, default))
            .Returns(() => commandHandler.HandleAsync(command));

        mock.Setup(c => c.SendAsync(query, default))
            .Returns(() => queryHandler.HandleAsync(query));

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

    private class Query : IQuery<string>
    {
        public string ExpectedResponse { get; init; } = string.Empty;
    }

    private class QueryHandler : IQueryHandler<Query, string>
    {
        public Task<string> HandleAsync(Query query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(query.ExpectedResponse);
        }
    }
}