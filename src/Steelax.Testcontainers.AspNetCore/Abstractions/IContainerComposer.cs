using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IContainerComposer
{
    Task StopAsync(CancellationToken cancelationToken = default);
    Task StartAsync(CancellationToken cancelationToken = default);

    void Stop();
    void Start();

    IContainer? GetContainer<TConfig>() where TConfig : class;
}
