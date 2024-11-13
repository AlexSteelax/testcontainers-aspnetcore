using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public delegate IContainerBuilder<TBuilderEntity, TContainerEntity> ComposeBuilderHandler<out TBuilderEntity, out TContainerEntity>(IComposeProvider composeProvider);