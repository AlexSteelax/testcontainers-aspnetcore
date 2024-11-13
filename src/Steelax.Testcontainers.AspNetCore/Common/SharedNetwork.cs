using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;

namespace Steelax.Testcontainers.AspNetCore.Common;

internal sealed class SharedNetwork
{
    public readonly INetwork Instance = new NetworkBuilder().WithName(Guid.NewGuid().ToString("N")).WithCleanUp(true).Build();
}