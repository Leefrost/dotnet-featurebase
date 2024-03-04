using Leefrost.Featurebase.Clients.Community.Responses;
using Newtonsoft.Json;

namespace Leefrost.Featurebase.Clients.Community;

internal static class FetchExtensions
{
    internal static async Task<long> FetchCountAsync(this HttpResponseMessage message,
        CancellationToken cancellationToken)
    {
        var content = await message.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var result = JsonConvert.DeserializeObject<CommunityCount>(content)
                         ?? throw new FeaturebaseException($"Failed to de-serialize response for counting query");

            return result.First;
        }
        catch (Exception ex)
        {
            throw new FeaturebaseException(
                "Failed to fetch content Featurebase response. See more info in inner exception.", ex);
        }
    }

    internal static async Task<TResponse> FetchAsync<TResponse>(this HttpResponseMessage message,
        CancellationToken cancellationToken)
    {
        var content = await message.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var result = JsonConvert.DeserializeObject<TResponse>(content)
                         ?? throw new FeaturebaseException($"Failed to de-serialize response for {typeof(TResponse)}");

            return result;
        }
        catch (Exception ex)
        {
            throw new FeaturebaseException(
                "Failed to fetch content Featurebase response. See more info in inner exception.", ex);
        }
    }

    internal static async Task<IReadOnlyList<TResponse>> FetchManyAsync<TResponse>(this HttpResponseMessage message,
        CancellationToken cancellationToken)
    {
        var result = new List<TResponse>();
        await using var content = await message.Content.ReadAsStreamAsync(cancellationToken);
        try
        {
            var serializer = new JsonSerializer();
            using var reader = new StreamReader(content);
            await using var jsonReader = new JsonTextReader(reader);
            jsonReader.SupportMultipleContent = true;

            while (await jsonReader.ReadAsync(cancellationToken))
            {
                var row = serializer.Deserialize<TResponse>(jsonReader);
                if (row is not null)
                    result.Add(row);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new FeaturebaseException(
                "Failed to fetch content Featurebase response. See more info in inner exception.", ex);
        }
    }

}
