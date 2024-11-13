using Steelax.Testcontainers.AspNetCore.Abstractions;

namespace Steelax.Testcontainers.AspNetCore;

public static class T
{
    public static IComposeBuilder Create() => new ComposeBuilder();
}