using Leefrost.Featurebase.Clients;

namespace FeatureBaseSampleWebApp;

public class MySpecialService
{
    private readonly IFeaturebaseClient _fbClient;

    public MySpecialService(IFeaturebaseClient fbClient)
    {
        _fbClient = fbClient;
    }

    public string GetSomeFakeMessage()
    {
        return "Hello from data service";
    }
}
