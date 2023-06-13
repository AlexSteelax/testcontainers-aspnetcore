using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public class ContainerService<TSelf> : IContainerWrapper<TSelf>
    where TSelf: class
{
    private IContainer? container;

    protected ContainerBuilder Builder { get; set; } = new();
    public IContainer Container => container ??= Builder.WithNetwork(SharedNetwork.Instance).Build();
}