using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace MyApi.HealthChecks;
public class MemoryHealthCheck : IHealthCheck
{
    private readonly long _memoryThresholdInBytes;

    public MemoryHealthCheck(long memoryThresholdInBytes)
    {
        _memoryThresholdInBytes = memoryThresholdInBytes;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var availableMemory = GetAvailableMemory();

        if (availableMemory >= _memoryThresholdInBytes)
        {
            return Task.FromResult(HealthCheckResult.Healthy($"Available memory: {availableMemory} bytes."));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"Low memory: {availableMemory} bytes."));
        }
    }

    private long GetAvailableMemory()
    {
        using (var process = Process.GetCurrentProcess())
        {
            return process.WorkingSet64;
        }
    }
}