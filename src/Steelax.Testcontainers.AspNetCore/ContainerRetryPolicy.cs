namespace Steelax.Testcontainers.AspNetCore;

public readonly struct ContainerRetryPolicy(
    ContainerRetryPolicy.ThrowRetryHandler throwRetryProvider,
    ContainerRetryPolicy.SleepDurationHandler sleepDurationProvider,
    int retryCount)
{
    public readonly SleepDurationHandler SleepDurationProvider = sleepDurationProvider;
    public readonly int RetryCount = retryCount;
    public readonly ThrowRetryHandler ThrowRetryProvider = throwRetryProvider;

    public ContainerRetryPolicy() : this(_ => true, _ => DefaultSleepDuration, DefaultRetryCount) { }
    
    public delegate TimeSpan SleepDurationHandler(int retryAttempt);

    public delegate bool ThrowRetryHandler(Exception exception);
    
    public static readonly ContainerRetryPolicy None = new ContainerRetryPolicy(_ => false, _ => TimeSpan.Zero, 0);
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static readonly TimeSpan DefaultSleepDuration = TimeSpan.FromMilliseconds(500);
    // ReSharper disable once MemberCanBePrivate.Global
    public const int DefaultRetryCount = 3;
}