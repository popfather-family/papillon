using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Papillon.CQRS;

public class QueryBusTests
{
    [Fact]
    public async Task Should_Find_QueryHandler()
    {
        var query = new Query { ExpectedResponse = "Hello World!" };
        var handler = new QueryHandler();
        var messageBus = CreateMessageBus(query, handler);
        var queryBus = new QueryBus(messageBus);
        var response = await queryBus.QueryAsync(query);

        Assert.Equal(query.ExpectedResponse, response);
    }

    private static IMessageBus CreateMessageBus(Query query, QueryHandler handler)
    {
        var mock = new Mock<IMessageBus>();

        mock.Setup(c => c.SendAsync(query, default))
            .Returns(() => handler.HandleAsync(query));

        return mock.Object;
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