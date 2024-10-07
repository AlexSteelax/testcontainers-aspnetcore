using Steelax.Testcontainers.AspNetCore;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using UnitTests.Testscontainers;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var provider = IComposerBuilder.Create().AddContainer<PostgresService>().Build();

        await provider.StartAsync();
        await provider.StopAsync();
    }
}