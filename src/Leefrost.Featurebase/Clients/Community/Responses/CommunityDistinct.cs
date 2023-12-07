namespace Leefrost.Featurebase.Clients.Community.Responses;

internal sealed record CommunityDistinct(IEnumerable<DistinctResult> Results)
{
    public IEnumerable<string> Result => Results.SelectMany(x => x.Keys);
}

internal sealed record DistinctResult(IEnumerable<string> Keys);