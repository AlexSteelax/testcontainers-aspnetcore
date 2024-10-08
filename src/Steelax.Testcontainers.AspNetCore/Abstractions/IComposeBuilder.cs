using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IComposeBuilder
{
    /// <summary>
    /// Create default composer builder
    /// </summary>
    static IComposeBuilder Create() => new ComposeBuilder();
    
    /// <summary>
    /// Add single container to cluster
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    IComposeBuilder AddContainer<TContainer>()
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Add several identical named containers to cluster
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TContainerNames"></typeparam>
    /// <returns></returns>
    IComposeBuilder AddContainers<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum;
    
    /// <summary>
    /// Add named networks to cluster
    /// </summary>
    /// <typeparam name="TNetworkNames"></typeparam>
    /// <returns></returns>
    IComposeBuilder AddNetworks<TNetworkNames>()
        where TNetworkNames : struct, Enum;
    
    /// <summary>
    /// Build cluster
    /// </summary>
    /// <returns></returns>
    IComposeProvider Build();
}