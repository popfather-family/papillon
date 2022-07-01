using System;
using Xunit;

namespace Papillon;

public class IdTests
{
    [Fact]
    public void Should_Generate_Guids()
    {
        var firstId = Id.Generate();
        var secondId = Id.Generate();

        var firstGuid = Guid.Parse(firstId);
        var secondGuid = Guid.Parse(secondId);

        Assert.NotEqual(firstGuid, secondGuid);
    }

    [Fact]
    public void Should_Parse_Custom_Id()
    {
        var id = new Id("Microsoft");

        Assert.Equal("Microsoft", id);
    }
}