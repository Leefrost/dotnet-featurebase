using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Leefrost.Featurebase.Clients.Community;

public sealed class CommunityFeaturebaseClient : IDisposable, IFeaturebaseClient
{
    private const string PqlEndpoint = "query";

    private readonly HttpClient _httpClient;
    private readonly FeaturebaseCommunityOptions _options;
    private readonly ILogger<CommunityFeaturebaseClient> _logger;

    public CommunityFeaturebaseClient(
        HttpClient httpClient,
        IOptions<FeaturebaseCommunityOptions> options,
        ILogger<CommunityFeaturebaseClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public string Provider => "Community";

    public async Task CheckAvailabilityAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed check");
    }

    public async Task<int> ExecuteAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        return 0;
    }

    public void Dispose() => _httpClient.Dispose();
}