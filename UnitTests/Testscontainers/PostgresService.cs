using DotNet.Testcontainers.Networks;
using Microsoft.Extensions.DependencyInjection;
using Steelax.Testcontainers.AspNetCore.Abstractions;
using Steelax.Testcontainers.AspNetCore.Defaults;
using Testcontainers.PostgreSql;

namespace UnitTests.Testscontainers;

// ReSharper disable once ClassNeverInstantiated.Global
public class PostgresService(
    [FromKeyedServices(NetworkNames.Shared)] INetwork network, ContainerNames containerNames)
    : IContainerService<PostgreSqlContainer>
{
    public PostgreSqlContainer Container { get; } = new PostgreSqlBuilder().WithNetwork(network).Build();
}