namespace Leefrost.Featurebase.Clients.Community.Responses;

internal record CommunityCount(IEnumerable<long> Results)
{
    public long Result => Results.FirstOrDefault();
}