using System.Net;
using Leefrost.Featurebase.Clients.Community;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Contrib.HttpClient;

namespace Leefrost.Featurebase.Tests.Clients.Community;

public class CommunityFeaturebaseClientTests
{
    private static CommunityFeaturebaseClient CreateClient(FeaturebaseCommunityOptions options, string responseContent)
    {
        var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var httpClient = handler.CreateClient();
        httpClient.BaseAddress = new Uri("https://featurebase-db.com");

        handler.SetupAnyRequest()
            .ReturnsResponse(HttpStatusCode.NotFound);

        handler.SetupRequest(HttpMethod.Post, "https://featurebase-db.com/query")
            .ReturnsResponse(HttpStatusCode.OK, responseContent);

        var opt = Options.Create(options);

        return new CommunityFeaturebaseClient(httpClient, opt, Mock.Of<ILogger<CommunityFeaturebaseClient>>());
    }
}
