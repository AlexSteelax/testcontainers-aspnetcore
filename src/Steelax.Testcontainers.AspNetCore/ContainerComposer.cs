using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ContainerComposer: IContainerComposer
{
    private readonly IReadOnlyList<IContainerWrapper> containers;
    private readonly IServiceProvider provider;

    public ContainerComposer(ContainerComposerOptions options, IServiceProvider provider)
    {
        containers = options?.containersSpec.Select(type => (IContainerWrapper)provider.GetRequiredService(typeof(IContainerWrapper<>).MakeGenericType(type))).ToList() ?? new();

        this.provider = provider;
    }

    public async Task StopAsync(CancellationToken cancelationToken = default)
    {
        await Task.WhenAll(containers.Select(c => c.Container.StopAsync(cancelationToken))).ConfigureAwait(false);
        await SharedNetwork.Instance.DeleteAsync(cancelationToken).ConfigureAwait(false);
    }

    public async Task StartAsync(CancellationToken cancelationToken = default)
    {
        await SharedNetwork.Instance.CreateAsync(cancelationToken).ConfigureAwait(false);
        await Task.WhenAll(containers.Select(c => c.Container.StartAsync(cancelationToken))).ConfigureAwait(false);
    }

    public bool TryFindContainer<TConfig>(out IContainer? container)
        where TConfig : class
    {
        container = provider.GetService<IContainerWrapper<TConfig>>()?.Container;

        return container != null;
    }
}
