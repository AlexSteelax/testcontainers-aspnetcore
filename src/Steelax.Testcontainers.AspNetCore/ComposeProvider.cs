using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Common;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ComposeProvider(IServiceProvider serviceProvider): IComposeProvider
{
    public TContainer GetRequiredContainer<TContainer>(object key) where TContainer : class, IContainer =>
        serviceProvider.GetRequiredKeyedService<IContainer>(key) as TContainer ?? throw new KeyNotFoundException();

    public IContainer GetRequiredContainer(object key) =>
        serviceProvider.GetRequiredKeyedService<IContainer>(key);

    public IEnumerable<IContainer> GetContainers() =>
        serviceProvider.GetServices<IContainer>();

    public INetwork GetSharedNetwork() =>
        serviceProvider.GetRequiredService<SharedNetwork>().Instance;

    public INetwork GetRequiredNetwork(object key) =>
        serviceProvider.GetRequiredKeyedService<INetwork>(key);
}