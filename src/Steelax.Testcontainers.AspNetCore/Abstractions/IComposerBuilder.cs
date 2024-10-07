using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IComposerBuilder
{
    /// <summary>
    /// Create default composer builder
    /// </summary>
    static IComposerBuilder Create() => new ComposerBuilder();
    
    /// <summary>
    /// Add single container to cluster
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    IComposerBuilder AddContainer<TContainer>()
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Add several identical named containers to cluster
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TContainerNames"></typeparam>
    /// <returns></returns>
    IComposerBuilder AddContainers<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum;
    
    /// <summary>
    /// Add named networks to cluster
    /// </summary>
    /// <typeparam name="TNetworkNames"></typeparam>
    /// <returns></returns>
    IComposerBuilder AddNetworks<TNetworkNames>()
        where TNetworkNames : struct, Enum;
    
    /// <summary>
    /// Build cluster
    /// </summary>
    /// <returns></returns>
    IComposerProvider Build();
}