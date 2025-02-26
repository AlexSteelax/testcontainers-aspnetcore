using DotNet.Testcontainers.Containers;
using Steelax.Testcontainers.AspNetCore.Abstractions;

namespace Steelax.Testcontainers.AspNetCore;

public static class ComposeProviderExtensions
{
    public static Task StartAsync(
        this IContainer container,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default) =>
        Helper.RetryAsync(container.StartAsync, retryPolicy, cancellationToken);
    
    public static Task StartAsync(
        this IEnumerable<IContainer> containers,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default) =>
        Task.WhenAll(containers.Select(c => c.StartAsync(retryPolicy, cancellationToken)));
    
    public static Task StopAsync(
        this IEnumerable<IContainer> containers,
        CancellationToken cancellationToken = default) =>
        Task.WhenAll(containers.Select(c => c.StopAsync(cancellationToken)));
    
    /// <summary>
    /// Start named container
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="key"></param>
    /// <param name="retryPolicy"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainerAsync(
        this IComposeProvider composeProvider,
        object key,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default) =>
        composeProvider.GetRequiredContainer(key).StartAsync(retryPolicy, cancellationToken);

    /// <summary>
    /// Stop named container
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainerAsync(
        this IComposeProvider composeProvider,
        object key,
        CancellationToken cancellationToken = default) =>
        composeProvider.GetRequiredContainer(key).StopAsync(cancellationToken);

    /// <summary>
    /// Start all containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="retryPolicy"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainersAsync(
        this IComposeProvider composeProvider,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default) =>
        composeProvider.GetContainers().StartAsync(retryPolicy, cancellationToken);
    
    /// <summary>
    /// Stop all containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainersAsync(
        this IComposeProvider composeProvider,
        CancellationToken cancellationToken = default) =>
        composeProvider.GetContainers().StopAsync(cancellationToken);
    
    /// <summary>
    /// Start many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="keys"></param>
    /// <param name="retryPolicy"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainerGroupAsync(
        this IComposeProvider composeProvider,
        object[] keys,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default) =>
        keys.Select(composeProvider.GetRequiredContainer).StartAsync(retryPolicy, cancellationToken);
    
    /// <summary>
    /// Stop many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="keys"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainerGroupAsync(
        this IComposeProvider composeProvider,
        object[] keys,
        CancellationToken cancellationToken = default) =>
        keys.Select(composeProvider.GetRequiredContainer).StopAsync(cancellationToken);

    /// <summary>
    /// Start many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="retryPolicy"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TKeys"></typeparam>
    /// <returns></returns>
    public static Task StartContainerGroupAsync<TKeys>(
        this IComposeProvider composeProvider,
        ContainerRetryPolicy retryPolicy = default,
        CancellationToken cancellationToken = default)
        where TKeys : struct, Enum =>
        composeProvider.StartContainerGroupAsync(Enum.GetValues<TKeys>().Cast<object>().ToArray(), retryPolicy, cancellationToken);
    
    /// <summary>
    /// Stop many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TKeys"></typeparam>
    /// <returns></returns>
    public static Task StopContainerGroupAsync<TKeys>(
        this IComposeProvider composeProvider,
        CancellationToken cancellationToken = default)
        where TKeys : struct, Enum =>
        composeProvider.StopContainerGroupAsync(Enum.GetValues<TKeys>().Cast<object>().ToArray(), cancellationToken);
}