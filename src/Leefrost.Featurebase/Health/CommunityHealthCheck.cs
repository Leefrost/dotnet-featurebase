using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Leefrost.Featurebase.Health;

public class CommunityHealthCheck : IHealthCheck
{
    private readonly string _featurebaseUrl;

    public CommunityHealthCheck(string featurebaseUrl)
    {
        if (string.IsNullOrEmpty(featurebaseUrl))
            throw new ArgumentException("Featurebase URL is required", nameof(featurebaseUrl));

        _featurebaseUrl = featurebaseUrl;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_featurebaseUrl);

            var response = await httpClient.GetAsync("/status", cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new HealthCheckResult(context.Registration.FailureStatus,
                    description: "Featurebase cluster response is not successful");

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, null, ex);
        }
    }
}
