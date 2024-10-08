using Steelax.Testcontainers.AspNetCore.Abstractions;
using UnitTests.Testscontainers;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var provider = IComposeBuilder.Create().AddContainer<PostgresService>().Build();

        await provider.StartAsync();
        Assert.NotEmpty(provider.GetContainerServices());
        await provider.StopAsync();
    }
}