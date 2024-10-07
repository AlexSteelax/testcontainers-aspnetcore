using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IComposerProvider
{
    /// <summary>
    /// Get single container instance
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    TContainer GetContainerService<TContainer>()
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Get named container service
    /// </summary>
    /// <param name="name"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    TContainer GetContainerService<TContainer>(Enum name)
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Get all named container services
    /// </summary>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TContainerNames"></typeparam>
    /// <returns></returns>
    IEnumerable<TContainer> GetContainerServices<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum;
    
    /// <summary>
    /// Start single container instance
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    Task StartContainerAsync<TContainer>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Start named container service
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    Task StartContainerAsync<TContainer>(Enum name, CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Start all named container services
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TContainerNames"></typeparam>
    /// <returns></returns>
    Task StartContainersAsync<TContainer, TContainerNames>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames: struct, Enum;
    
    /// <summary>
    /// Stop single container instance
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    Task StopContainerAsync<TContainer>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Stop named container service
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    Task StopContainerAsync<TContainer>(Enum name, CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>;
    
    /// <summary>
    /// Stop all named container services
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <typeparam name="TContainerNames"></typeparam>
    /// <returns></returns>
    Task StopContainersAsync<TContainer, TContainerNames>(CancellationToken cancellationToken = default)
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames: struct, Enum;

    /// <summary>
    /// Start all container services
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task StartAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Stop all container services
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task StopAsync(CancellationToken cancellationToken = default);
}