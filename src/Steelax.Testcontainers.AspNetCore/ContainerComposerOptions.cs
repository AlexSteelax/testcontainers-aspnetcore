using Steelax.Testcontainers.AspNetCore.Abstractions;
using System.Reflection;

namespace Steelax.Testcontainers.AspNetCore;

public class ContainerComposerOptions
{
    internal Type[]? containersSpec;

    public ContainerComposerOptions ScanContainersIn(params Assembly[] assemblies)
    {
        containersSpec = assemblies.SelectMany(assembly => assembly.GetExportedTypes()).Where(type => type.IsClass && !type.IsAbstract && type.BaseType is not null && typeof(IContainerWrapper<>).MakeGenericType(type).IsAssignableFrom(type)).ToArray();

        return this;
    }
}
