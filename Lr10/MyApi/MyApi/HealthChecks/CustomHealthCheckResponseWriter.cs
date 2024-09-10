using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

public static class CustomHealthCheckResponseWriter
{
    public static Task WriteResponse(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(new
        {
            status = result.Status.ToString(),
            checks = result.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                exception = entry.Value.Exception?.Message ?? "none",
                duration = entry.Value.Duration.ToString()
            }),
            totalDuration = result.TotalDuration.ToString()
        });

        return httpContext.Response.WriteAsync(json);
    }
}