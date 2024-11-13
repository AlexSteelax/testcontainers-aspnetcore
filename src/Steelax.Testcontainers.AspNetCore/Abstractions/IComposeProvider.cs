using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IComposeProvider
{
    /// <summary>
    /// Get named typed container instance
    /// </summary>
    /// <param name="key"></param>
    /// <typeparam name="TContainer"></typeparam>
    /// <returns></returns>
    TContainer GetRequiredContainer<TContainer>(object key) where TContainer : class, IContainer;

    /// <summary>
    /// Get named abstracted container instance
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    IContainer GetRequiredContainer(object key);
    
    /// <summary>
    /// Get all abstracted container instances
    /// </summary>
    /// <returns></returns>
    IEnumerable<IContainer> GetContainers();

    /// <summary>
    /// Get shared network
    /// </summary>
    /// <returns></returns>
    INetwork GetSharedNetwork();

    /// <summary>
    /// Get named network
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    INetwork GetRequiredNetwork(object key);
}