using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public class ContainerService<TSelf, TBuilderEntity, TContainerEntity, TConfigurationEntity> : IContainerWrapper<TSelf>
    where TSelf: class
    where TBuilderEntity : ContainerBuilder<TBuilderEntity, TContainerEntity, TConfigurationEntity>
    where TContainerEntity : IContainer
    where TConfigurationEntity : IContainerConfiguration
{
    private IContainer? container;

    protected ContainerBuilder<TBuilderEntity, TContainerEntity, TConfigurationEntity>? Builder { get; set; }
    public IContainer Container => container ??= Builder!.WithNetwork(SharedNetwork.Instance).Build();
}