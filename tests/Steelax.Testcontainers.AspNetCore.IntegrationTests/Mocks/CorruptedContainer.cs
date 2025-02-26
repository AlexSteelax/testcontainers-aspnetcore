namespace UnitTests.Mocks;

public sealed class CorruptedContainer
{
    private bool _corrupted;
    
    public Task StartAsync(CancellationToken _ = default) =>
        _corrupted ? Task.FromException(new Exception("Something went wrong")) : Task.CompletedTask;
    
    public void Corrupted(bool corrupted = true) =>
        _corrupted = corrupted;
}