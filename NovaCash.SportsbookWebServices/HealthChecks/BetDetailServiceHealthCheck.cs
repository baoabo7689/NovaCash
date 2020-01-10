using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NovaCash.SportsbookWebServices.HealthChecks
{
    public class BetDetailServiceHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var job = Hangfire.JobStorage.Current
                .GetConnection()
                .GetRecurringJobs()
                .FirstOrDefault(j => string.Equals(j.Id, "HangfireBetDetailWorker", StringComparison.OrdinalIgnoreCase));

            var lastExecution = job.LastExecution.HasValue
                ? TimeZoneInfo.ConvertTimeFromUtc(job.LastExecution.Value, TimeZoneInfo.Local)
                : DateTime.MinValue;

            var nextExecution = job.NextExecution.HasValue
                ? TimeZoneInfo.ConvertTimeFromUtc(job.NextExecution.Value, TimeZoneInfo.Local)
                : DateTime.MinValue;

            var message = $"ID: {job.Id}. Last Job State: {job.LastJobState}. Last Execution : {lastExecution}. Next Execution : {nextExecution}.";
            var isHealthy = nextExecution >= DateTime.Now.AddMinutes(-2);
            var result = isHealthy
                ? HealthCheckResult.Healthy(message)
                : HealthCheckResult.Unhealthy(message);
            return Task.FromResult(result);
        }

        public static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var results = new JObject(result.Entries.Select(pair =>
            new JProperty(pair.Key, new JObject(
                new JProperty("status", pair.Value.Status.ToString()),
                new JProperty("description", pair.Value.Description ?? "Error"),
                new JProperty("data", new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value))))))));

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", results));

            return context.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}