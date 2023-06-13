using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IContainerComposer
{
    Task StopAsync(CancellationToken cancelationToken = default);
    Task StartAsync(CancellationToken cancelationToken = default);
    bool TryFindContainer<TConfig>(out IContainer? container) where TConfig : class;
}
