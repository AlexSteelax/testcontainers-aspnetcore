using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;


namespace Steelax.Testcontainers.AspNetCore;

public static class TestContainersExtensions
{
    public static void AddTestContainers(this IServiceCollection services, Action<ContainerComposerOptions> configure)
    {
        var options = new ContainerComposerOptions();

        configure(options);

        services.AddSingleton(options);
        services.AddSingleton<IContainerComposer, ContainerComposer>();

        if (options.containersSpec is not null)
            foreach(var type in options.containersSpec)
            {
                services.AddSingleton(typeof(IContainerWrapper<>).MakeGenericType(type), type);
            }
    }

    public static IContainerComposer CreateContainerComposerService(Action<ContainerComposerOptions> configure)
    {
        var services = new ServiceCollection();
        services.AddTestContainers(configure);
        var provider = services.BuildServiceProvider();
        var composer = provider.GetRequiredService<IContainerComposer>();

        composer.Start();

        return composer;
    }
}

