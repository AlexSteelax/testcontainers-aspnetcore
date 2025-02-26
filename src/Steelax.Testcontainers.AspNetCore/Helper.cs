namespace Steelax.Testcontainers.AspNetCore;

internal static class Helper
{
    public static async Task RetryAsync(Func<CancellationToken, Task> handler, ContainerRetryPolicy retryPolicy, CancellationToken cancellationToken = default)
    {
        var retryAttempt = 0;

        while (true)
        {
            try
            {
                await handler.Invoke(cancellationToken).ConfigureAwait(false);
                return;
            }
            catch(Exception ex)
            {
                if (!retryPolicy.ThrowRetryProvider.Invoke(ex))
                    throw;
                
                if (++retryAttempt > retryPolicy.RetryCount)
                    throw;
            }

            await Task.Delay(retryPolicy.SleepDurationProvider.Invoke(retryAttempt), cancellationToken).ConfigureAwait(false);
        }
    }
}