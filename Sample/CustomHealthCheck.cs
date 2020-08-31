using Microsoft.Extensions.Diagnostics.HealthChecks;
using Sample.Core.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.API
{
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly IResourceLocalizer _resource;

        public CustomHealthCheck(IResourceLocalizer resource)
        {
            _resource = resource;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResult = true;

            if (healthCheckResult)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy(_resource.GetString("HEALTHY_STATUS")));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy(_resource.GetString("UNHEALTHY_STATUS")));
        }
    }
}
