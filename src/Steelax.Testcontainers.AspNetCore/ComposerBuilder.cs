using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Defaults;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ComposerBuilder: IComposerBuilder
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    
    private static INetwork CreateNetwork() =>
        new NetworkBuilder().WithName(Guid.NewGuid().ToString("N")).WithCleanUp(true).Build();

    public IComposerBuilder AddContainer<TContainer>() where TContainer : class, IContainerService<IContainer>
    {
        _serviceCollection.TryAddKeyedSingleton<TContainer>(ContainerNames.Default);
        _serviceCollection.AddSingleton<IContainerService<IContainer>>(provider =>
            provider.GetRequiredKeyedService<TContainer>(ContainerNames.Default));
        return this;
    }

    public IComposerBuilder AddContainers<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum
    {
        var names = Enum.GetValues<TContainerNames>();

        foreach (var name in names)
        {
            _serviceCollection.TryAddKeyedSingleton<TContainer>(name);
            _serviceCollection.AddSingleton<IContainerService<IContainer>>(provider =>
                provider.GetRequiredKeyedService<TContainer>(name));
        }

        return this;
    }

    public IComposerBuilder AddNetworks<TNetworkNames>()
        where TNetworkNames : struct, Enum
    {
        var names = Enum.GetValues<TNetworkNames>();

        foreach (var name in names)
            _serviceCollection.TryAddKeyedSingleton<INetwork>(name, (_, key) => CreateNetwork());

        return this;
    }

    public IComposerProvider Build()
    {
        _serviceCollection.TryAddKeyedSingleton<INetwork>(NetworkNames.Shared, (_, key) => CreateNetwork());
        
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var composeProvider = new ComposerProvider(serviceProvider);

        return composeProvider;
    }
}