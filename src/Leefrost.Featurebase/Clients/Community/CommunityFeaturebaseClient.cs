using Leefrost.Featurebase.Clients.Community.Responses;
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
        Uri connectionUrl,
        IOptions<FeaturebaseCommunityOptions> options,
        ILogger<CommunityFeaturebaseClient> logger)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = connectionUrl
        };

        _options = options.Value;
        _logger = logger;
    }

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
        await response.ThrowIfNotSuccessfulAsync(cancellationToken);
    }

    public async Task<long> CountAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        await response.ThrowIfNotSuccessfulAsync(cancellationToken);

        var result = await response.FetchAsync<CommunityCount>(cancellationToken);
        return result.Result;
    }

    public async Task<IReadOnlyList<string>> SelectAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        await response.ThrowIfNotSuccessfulAsync(cancellationToken);

        var result = await response.FetchAsync<CommunityDistinct>(cancellationToken);
        return result.Result.ToList();
    }

    public async Task<string> ExecuteRawPqlAsync(string query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query);

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        await response.ThrowIfNotSuccessfulAsync(cancellationToken);

        var result = await response.Content.ReadAsStringAsync(cancellationToken);
        return result;
    }

    public void Dispose() => _httpClient.Dispose();
}