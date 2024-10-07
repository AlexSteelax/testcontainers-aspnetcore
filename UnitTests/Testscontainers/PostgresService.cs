using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Defaults;
using Testcontainers.PostgreSql;

namespace UnitTests.Testscontainers;

public class PostgresService(
    [FromKeyedServices(NetworkNames.Shared)] INetwork network)
    : IContainerService<PostgreSqlContainer>
{
    public PostgreSqlContainer Container { get; } = new PostgreSqlBuilder().WithNetwork(network).Build();
}