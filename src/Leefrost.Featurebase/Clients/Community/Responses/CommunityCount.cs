namespace Leefrost.Featurebase.Clients.Community.Responses;

internal record CommunityCount(IEnumerable<long> Results)
{
    public long First => Results.FirstOrDefault();
}