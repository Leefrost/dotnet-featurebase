using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Leefrost.Featurebase.Clients.Community;

public sealed class CommunityFeaturebaseClient : IFeaturebaseClient
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

    public async Task CheckAvailabilityAsync(CancellationToken cancellationToken)
    {
        var content = new StringContent("Limit(All(), limit=1))");

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed check");
    }

    public Task<long> CountAsync(string query, CancellationToken cancellationToken)
    {
        return Task.FromResult(0L);
    }

    public Task<ReadOnlyCollection<T>> SelectAsync<T>(string query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ReadOnlyCollection<T>(new List<T>()));
    }

    public async Task<string> ExecuteRawPqlAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    public void Dispose() => _httpClient.Dispose();
}