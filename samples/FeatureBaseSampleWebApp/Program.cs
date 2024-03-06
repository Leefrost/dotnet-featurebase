using FeatureBaseSampleWebApp;
using Leefrost.Featurebase.Clients;
using Leefrost.Featurebase.Clients.Community;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();


builder.Services.AddOptions<FeaturebaseCommunityOptions>();

builder.Services.AddHttpClient<IFeaturebaseClient, CommunityFeaturebaseClient>((_, services) =>
{
    var options = Microsoft.Extensions.Options.Options.Create(new FeaturebaseCommunityOptions());
    var logFactory = services.GetRequiredService<ILoggerFactory>();
    var logger = logFactory.CreateLogger<CommunityFeaturebaseClient>();

    return new CommunityFeaturebaseClient(new Uri("http://test.com"), options, logger);
});
builder.Services.AddTransient<MySpecialService>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", (MySpecialService service) => service.GetSomeFakeMessage());

app.Run();
