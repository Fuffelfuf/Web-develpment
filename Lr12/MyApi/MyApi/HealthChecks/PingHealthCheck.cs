using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
namespace MyApi.HealthChecks;
public class PingHealthCheck : IHealthCheck
{
    private readonly string _host;

    public PingHealthCheck(string host)
    {
        _host = host;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_host);

            if (reply.Status == IPStatus.Success)
            {
                return HealthCheckResult.Healthy("Ping successful.");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"Ping failed: {reply.Status}");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Ping failed: {ex.Message}");
        }
    }
}