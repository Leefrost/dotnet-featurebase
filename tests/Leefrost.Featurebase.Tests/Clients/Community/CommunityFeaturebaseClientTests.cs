using System.Net;
using FluentAssertions;
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


    [Fact]
    public async Task CountAsync_CheckCall_CallIsSuccessful()
    {
        var options = new FeaturebaseCommunityOptions { Index = "index" };
        var client = CreateClient(options, "{ results: [100]}");

        var count = await client.CountAsync("Count(All())", default);

        count.Should().Be(100);
    }

    [Fact]
    public async Task SelectAsync_CheckCall_CallIsSuccessful()
    {
        var options = new FeaturebaseCommunityOptions { Index = "index" };
        var client = CreateClient(options, "{ results: [{ keys: ['123'] }] }");

        var count = await client.SelectAsync("Distinct(All())", default);

        count.Should().NotBeNullOrEmpty();
    }
}
