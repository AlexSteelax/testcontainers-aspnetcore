using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Common;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ComposeBuilder: IComposeBuilder
{
    private readonly ServiceCollection _serviceCollection = new();
    
    public IComposeBuilder ConfigureContainer<TBuilderEntity, TContainerEntity, TConfigurationEntity>(object key, ComposeBuilderHandler<TBuilderEntity, TContainerEntity, TConfigurationEntity> handler)
        where TContainerEntity : IContainer
        where TBuilderEntity : ContainerBuilder<TBuilderEntity, TContainerEntity, TConfigurationEntity>
        where TConfigurationEntity : IContainerConfiguration
    {
        _serviceCollection.TryAddKeyedSingleton<IContainer>(key, (provider, _) =>
        {
            var composeProvider = new ComposeProvider(provider);
            var logger = provider.GetRequiredService<ILogger<TContainerEntity>>();

            return handler(composeProvider).WithLogger(logger).Build();
        });
        
        return this;
    }

    public IComposeBuilder ConfigureNetwork(object key, NetworkBuilderHandler handler)
    {
        _serviceCollection.TryAddKeyedSingleton<INetwork>(key, (_, _) =>
        {
            var networkBuilder = handler();

            return networkBuilder.Build();
        });

        return this;
    }

    public IComposeBuilder AddLogger(Action<ILoggingBuilder> builder)
    {
        _serviceCollection.AddLogging(builder);

        return this;
    }

    public IComposeProvider Build()
    {
        TryAddNonKeyedContainers();
        TryAddLogger();

        _serviceCollection.TryAddSingleton<SharedNetwork>();
        
        return new ComposeProvider(_serviceCollection.BuildServiceProvider());
    }

    private void TryAddLogger()
    {
        var loggerDescriptor = ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>();
        
        if (!_serviceCollection.Contains(loggerDescriptor))
            _serviceCollection.AddLogging();
    }

    private void TryAddNonKeyedContainers()
    {
        _serviceCollection
            .Where(descriptor => descriptor.ServiceType == typeof(IContainer))
            .Select(s => new { s.ServiceType, s.ServiceKey })
            .ToList()
            .ForEach(s => _serviceCollection
                .AddSingleton(typeof(IContainer), provider => provider.GetRequiredKeyedService(s.ServiceType, s.ServiceKey)));
    }
}