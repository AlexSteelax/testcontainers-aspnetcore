using Steelax.Testcontainers.AspNetCore.Abstractions;
using Testcontainers.PostgreSql;

namespace UnitTests.Testscontainers;

// ReSharper disable once ClassNeverInstantiated.Global
public class PostgresContainerConfigurator
{
    public static PostgreSqlBuilder Configure(IComposeProvider composeProvider)
    {
        var sharedNetwork = composeProvider.GetSharedNetwork();

        return new PostgreSqlBuilder().WithNetwork(sharedNetwork);
    }
}