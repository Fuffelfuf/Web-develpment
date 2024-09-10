using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Data.Sqlite;
using System.Threading;
using System.Threading.Tasks;
namespace MyApi.HealthChecks;
public class SqliteHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public SqliteHealthCheck(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return HealthCheckResult.Healthy("Database is available.");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database is unavailable.", ex);
        }
    }
}