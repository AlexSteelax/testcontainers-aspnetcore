using DotNet.Testcontainers.Containers;

namespace Steelax.Testcontainers.AspNetCore.Abstractions;

public interface IContainerWrapper<TConfig>: IContainerWrapper
    where TConfig : class
{
    //Nothing
    //A bit of smelling code
}

public interface IContainerWrapper
{
    IContainer Container { get; }
}