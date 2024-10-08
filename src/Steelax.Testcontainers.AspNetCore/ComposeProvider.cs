using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Defaults;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ComposeProvider(ServiceProvider serviceProvider): IComposeProvider
{
    public IEnumerable<IContainerService<IContainer>> GetContainerServices()
    {
        return serviceProvider.GetServices<IContainerService<IContainer>>();
    }
    
    public TContainer GetContainerService<TContainer>()
        where TContainer : class, IContainerService<IContainer>
    {
        return serviceProvider.GetRequiredKeyedService<TContainer>(ContainerNames.Default);
    }

    public TContainer GetContainerService<TContainer>(Enum name)
        where TContainer : class, IContainerService<IContainer>
    {
        return serviceProvider.GetRequiredKeyedService<TContainer>(name);
    }

    public IEnumerable<TContainer> GetContainerServices<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum
    {
        var names = Enum.GetValues<TContainerNames>();
        
        foreach (var name in names)
            yield return serviceProvider.GetRequiredKeyedService<TContainer>(name);
    }

    public Task StartContainerAsync<TContainer>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
    {
        return GetContainerService<TContainer>().Container.StartAsync(cancellationToken);
    }

    public Task StartContainerAsync<TContainer>(Enum name, CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
    {
        return GetContainerService<TContainer>(name).Container.StartAsync(cancellationToken);
    }

    public Task StartContainersAsync<TContainer, TContainerNames>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum
    {
        return Task.WhenAll(GetContainerServices<TContainer, TContainerNames>()
            .Select(s => s.Container.StartAsync(cancellationToken)));
    }

    public Task StopContainerAsync<TContainer>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
    {
        return GetContainerService<TContainer>().Container.StopAsync(cancellationToken);
    }

    public Task StopContainerAsync<TContainer>(Enum name, CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
    {
        return GetContainerService<TContainer>(name).Container.StopAsync(cancellationToken);
    }

    public Task StopContainersAsync<TContainer, TContainerNames>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum
    {
        return Task.WhenAll(GetContainerServices<TContainer, TContainerNames>()
            .Select(s => s.Container.StopAsync(cancellationToken)));
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(GetContainerServices()
            .Select(s => s.Container.StartAsync(cancellationToken)));
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(GetContainerServices()
            .Select(s => s.Container.StopAsync(cancellationToken)));
    }
}