using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IComposeBuilder
{
    /// <summary>
    /// Configure named test container
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    /// <typeparam name="TBuilderEntity"></typeparam>
    /// <typeparam name="TContainerEntity"></typeparam>
    /// <returns></returns>
    IComposeBuilder ConfigureContainer<TBuilderEntity, TContainerEntity>(object key, ComposeBuilderHandler<TBuilderEntity, TContainerEntity> handler)
        where TContainerEntity : IContainer;

    /// <summary>
    /// Configure named network
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    IComposeBuilder ConfigureNetwork(object key, NetworkBuilderHandler handler);

    /// <summary>
    /// Add logger provider
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    IComposeBuilder AddLogger(Action<ILoggingBuilder> builder);
    
    /// <summary>
    /// Compose containers
    /// </summary>
    /// <returns></returns>
    IComposeProvider Build();
}