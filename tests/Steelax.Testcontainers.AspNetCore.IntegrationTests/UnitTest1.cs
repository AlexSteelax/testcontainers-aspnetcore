using Steelax.Testcontainers.AspNetCore;
using UnitTests.Testscontainers;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var provider = T.Create().ConfigureContainer(0, PostgresContainerConfigurator.Configure).Build();

        await provider.StartContainersAsync();
        _ = provider.GetRequiredContainer(0);
        await provider.StopContainersAsync();
    }
}