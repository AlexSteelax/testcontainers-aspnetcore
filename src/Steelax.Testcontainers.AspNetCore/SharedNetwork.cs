using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;

namespace Steelax.Testcontainers.AspNetCore;

internal static class SharedNetwork
{
    public static INetwork Instance = new NetworkBuilder().WithName(Guid.NewGuid().ToString("N")).WithCleanUp(true).Build();
}
