using Newtonsoft.Json;

namespace Leefrost.Featurebase.Clients.Community;
internal static class ResponseExtensions
{
    internal static async Task ThrowIfNotSuccessfulAsync(this HttpResponseMessage message,
        CancellationToken cancellationToken)
    {
        if (message.IsSuccessStatusCode)
            return;

        var errorMessage = await message.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrEmpty(errorMessage))
            throw new FeaturebaseException("Featurebase response is empty");

        try
        {
            var dbMessage = JsonConvert.DeserializeAnonymousType(errorMessage, new { Error = "" });
            if (dbMessage is null || string.IsNullOrEmpty(dbMessage.Error))
                throw new FeaturebaseException(
                    $"Failed to de-serialize DB error. Original response is: {errorMessage}");

            throw new FeaturebaseException($"Featurebase throws exception: {dbMessage.Error}");
        }
        catch (Exception e)
        {
            throw new FeaturebaseException(
                "Featurebase response is not successful. See more information in inner exception.", e);
        }
    }
}
