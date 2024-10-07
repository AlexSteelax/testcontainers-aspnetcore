using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IContainerService<out TContainer>
    where TContainer : class, IContainer
{
    TContainer Container { get; }
}