using System.Reflection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Defaults;

namespace Steelax.Testcontainers.AspNetCore;

internal sealed class ComposeBuilder: IComposeBuilder
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    
    private static INetwork CreateNetwork() =>
        new NetworkBuilder().WithName(Guid.NewGuid().ToString("N")).WithCleanUp(true).Build();

    public IComposeBuilder AddContainer<TContainer>() where TContainer : class, IContainerService<IContainer>
    {
        AddContainers<TContainer, ContainerNames>();
        return this;
    }

    public IComposeBuilder AddContainers<TContainer, TContainerNames>()
        where TContainer : class, IContainerService<IContainer>
        where TContainerNames : struct, Enum
    {
        var names = Enum.GetValues<TContainerNames>();

        foreach (var name in names)
        {
            _serviceCollection.TryAddKeyedSingleton<TContainer>(name, (provider, key) => HasKeyedServiceArg(typeof(TContainer))
                ? ActivatorUtilities.CreateInstance<TContainer>(provider, key!)
                : ActivatorUtilities.CreateInstance<TContainer>(provider));
        }

        return this;
    }

    public IComposeBuilder AddNetworks<TNetworkNames>()
        where TNetworkNames : struct, Enum
    {
        var names = Enum.GetValues<TNetworkNames>();

        foreach (var name in names)
            _serviceCollection.TryAddKeyedSingleton<INetwork>(name, (_, key) =>
                CreateNetwork());

        return this;
    }

    public IComposeProvider Build()
    {
        //Add shared network
        AddNetworks<NetworkNames>();
        
        //Add container services
        _serviceCollection
            .Where(s => s.IsKeyedService && IsContainerService(s.ServiceType))
            .Select(s => new { s.ServiceType, s.ServiceKey })
            .ToList()
            .ForEach(s =>
            {
                _serviceCollection.AddSingleton(typeof(IContainerService<IContainer>), provider =>
                    provider.GetRequiredKeyedService(s.ServiceType, s.ServiceKey));
            });

        var serviceProvider = _serviceCollection.BuildServiceProvider();
        var composeProvider = new ComposeProvider(serviceProvider);

        return composeProvider;
    }

    private static bool IsContainerService(Type type)
    {
        return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IContainerService<>));
    }

    private static bool HasKeyedServiceArg(Type type)
    {
        return type.GetConstructors().First().GetParameters().Any(s => s.ParameterType.IsEnum);
    }
}