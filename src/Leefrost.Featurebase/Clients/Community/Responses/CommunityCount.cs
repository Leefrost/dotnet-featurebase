namespace Leefrost.Featurebase.Clients.Community.Responses;

internal record CommunityCount(IEnumerable<int> Results)
{
    public int Result => Results.FirstOrDefault();
}