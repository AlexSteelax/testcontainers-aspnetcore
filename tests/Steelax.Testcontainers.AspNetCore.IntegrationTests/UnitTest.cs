using Steelax.Testcontainers.AspNetCore;
using UnitTests.Mocks;
using UnitTests.Testscontainers;

namespace UnitTests;

public class UnitTest
{
    [Fact]
    public async Task ContainerRun_Success()
    {
        var provider = T.Create().ConfigureContainer(0, PostgresContainerConfigurator.Configure).Build();

        await provider.StartContainersAsync();
        _ = provider.GetRequiredContainer(0);
        await provider.StopContainersAsync();
    }

    [Fact]
    public async Task Retry_Success()
    {
        var container = new CorruptedContainer();

        await container.StartAsync();
        
        container.Corrupted();
        
        await Assert.ThrowsAsync<Exception>(() => container.StartAsync());

        var retryAttempt = 0;
        
        await Helper.RetryAsync(ct =>
        {
            if (++retryAttempt > 2)
                container.Corrupted(false);
            
            return container.StartAsync(ct);
        }, new ContainerRetryPolicy());
    }
}