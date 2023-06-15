using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public class ContainerService<TSelf> : IContainerWrapper<TSelf>
    where TSelf: class
{
    private IContainer? container;

    protected ContainerBuilder<ContainerBuilder, IContainer, IContainerConfiguration>? Builder { get; set; }
    public IContainer Container => container ??= Builder!.WithNetwork(SharedNetwork.Instance).Build();
}