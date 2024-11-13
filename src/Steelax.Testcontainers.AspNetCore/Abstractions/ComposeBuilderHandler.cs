using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public delegate ContainerBuilder<TBuilderEntity, TContainerEntity, TConfigurationEntity> ComposeBuilderHandler<TBuilderEntity, TContainerEntity, TConfigurationEntity>(IComposeProvider composeProvider)
    where TConfigurationEntity : IContainerConfiguration
    where TContainerEntity : IContainer
    where TBuilderEntity : ContainerBuilder<TBuilderEntity, TContainerEntity, TConfigurationEntity>;
