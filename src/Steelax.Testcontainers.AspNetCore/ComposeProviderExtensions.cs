using Steelax.Testcontainers.AspNetCore.Abstractions;

namespace Steelax.Testcontainers.AspNetCore;

public static class ComposeProviderExtensions
{
    /// <summary>
    /// Start named container
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainerAsync(this IComposeProvider composeProvider, object key, CancellationToken cancellationToken = default) =>
        composeProvider.GetRequiredContainer(key).StartAsync(cancellationToken);
    
    /// <summary>
    /// Stop named container
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainerAsync(this IComposeProvider composeProvider, object key, CancellationToken cancellationToken = default) =>
        composeProvider.GetRequiredContainer(key).StopAsync(cancellationToken);
    
    /// <summary>
    /// Start all containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainersAsync(this IComposeProvider composeProvider, CancellationToken cancellationToken = default) =>
        Task.WhenAll(composeProvider.GetContainers().Select(s => s.StartAsync(cancellationToken)));
    
    /// <summary>
    /// Stop all containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainersAsync(this IComposeProvider composeProvider, CancellationToken cancellationToken = default) =>
        Task.WhenAll(composeProvider.GetContainers().Select(s => s.StopAsync(cancellationToken)));
    
    /// <summary>
    /// Start many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="keys"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StartContainerGroupAsync(this IComposeProvider composeProvider, object[] keys, CancellationToken cancellationToken = default) =>
        Task.WhenAll(keys.Select(k => composeProvider.GetRequiredContainer(k).StartAsync(cancellationToken)));
    
    /// <summary>
    /// Stop many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="keys"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task StopContainerGroupAsync(this IComposeProvider composeProvider, object[] keys, CancellationToken cancellationToken = default) =>
        Task.WhenAll(keys.Select(k => composeProvider.GetRequiredContainer(k).StopAsync(cancellationToken)));
    
    /// <summary>
    /// Start many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TKeys"></typeparam>
    /// <returns></returns>
    public static Task StartContainerGroupAsync<TKeys>(this IComposeProvider composeProvider, CancellationToken cancellationToken = default)
        where TKeys : struct, Enum =>
        Task.WhenAll(Enum.GetValues<TKeys>().Select(k => composeProvider.GetRequiredContainer(k).StartAsync(cancellationToken)));
    
    /// <summary>
    /// Stop many named containers
    /// </summary>
    /// <param name="composeProvider"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TKeys"></typeparam>
    /// <returns></returns>
    public static Task StopContainerGroupAsync<TKeys>(this IComposeProvider composeProvider, CancellationToken cancellationToken = default)
        where TKeys : struct, Enum =>
        Task.WhenAll(Enum.GetValues<TKeys>().Select(k => composeProvider.GetRequiredContainer(k).StopAsync(cancellationToken)));
}