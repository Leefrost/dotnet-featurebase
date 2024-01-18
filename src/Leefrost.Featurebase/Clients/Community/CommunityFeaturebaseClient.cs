using Leefrost.Featurebase.Pql;
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

    public async Task<TResult> ExecuteAsync<TResult>(Query query, CancellationToken cancellationToken)
    {
        var content = new StringContent(query.Build());

        using var response = await _httpClient.PostAsync(PqlEndpoint, content, cancellationToken);
        await response.ThrowIfNotSuccessfulAsync(cancellationToken);

        var result = await response.FetchAsync<TResult>(cancellationToken);
        return result;
    }

    public Task<int> CountAsync(CountQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose() => _httpClient.Dispose();
}