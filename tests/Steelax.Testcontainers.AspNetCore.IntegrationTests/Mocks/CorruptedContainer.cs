using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Microsoft.Extensions.Logging;

namespace UnitTests.Mocks;

public sealed class CorruptedContainer : IContainer
{
    private bool _corrupted;
    
    public Task StartAsync(CancellationToken _ = default) =>
        _corrupted ? Task.FromException(new Exception("Something went wrong")) : Task.CompletedTask;
    
    public void Corrupted(bool corrupted = true) =>
        _corrupted = corrupted;

    public ValueTask DisposeAsync() => default;

    #region NotImplemented
    public ushort GetMappedPublicPort(int containerPort)
    {
        throw new NotImplementedException();
    }

    public ushort GetMappedPublicPort(string containerPort)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetExitCodeAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<(string Stdout, string Stderr)> GetLogsAsync(DateTime since = default, DateTime until = default, bool timestampsEnabled = true, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CopyAsync(byte[] fileContent, string filePath, UnixFileModes fileMode = UnixFileModes.None | UnixFileModes.OtherRead | UnixFileModes.GroupRead | UnixFileModes.UserWrite | UnixFileModes.UserRead, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CopyAsync(string source, string target, UnixFileModes fileMode = UnixFileModes.None | UnixFileModes.OtherRead | UnixFileModes.GroupRead | UnixFileModes.UserWrite | UnixFileModes.UserRead, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CopyAsync(DirectoryInfo source, string target, UnixFileModes fileMode = UnixFileModes.None | UnixFileModes.OtherRead | UnixFileModes.GroupRead | UnixFileModes.UserWrite | UnixFileModes.UserRead, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CopyAsync(FileInfo source, string target, UnixFileModes fileMode = UnixFileModes.None | UnixFileModes.OtherRead | UnixFileModes.GroupRead | UnixFileModes.UserWrite | UnixFileModes.UserRead, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReadFileAsync(string filePath, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ExecResult> ExecAsync(IList<string> command, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public DateTime CreatedTime => throw new NotImplementedException();
    public DateTime StartedTime => throw new NotImplementedException();
    public DateTime StoppedTime => throw new NotImplementedException();
    public ILogger Logger => throw new NotImplementedException();
    public string Id => throw new NotImplementedException();
    public string Name => throw new NotImplementedException();
    public string IpAddress => throw new NotImplementedException();
    public string MacAddress => throw new NotImplementedException();
    public string Hostname => throw new NotImplementedException();
    public IImage Image => throw new NotImplementedException();
    public TestcontainersStates State => throw new NotImplementedException();
    public TestcontainersHealthStatus Health => throw new NotImplementedException();
    public long HealthCheckFailingStreak => throw new NotImplementedException();
    public event EventHandler? Creating;
    public event EventHandler? Starting;
    public event EventHandler? Stopping;
    public event EventHandler? Created;
    public event EventHandler? Started;
    public event EventHandler? Stopped;
    
    #endregion
}