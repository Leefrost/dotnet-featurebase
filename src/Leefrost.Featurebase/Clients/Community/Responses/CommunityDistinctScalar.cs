namespace Leefrost.Featurebase.Clients.Community.Responses;

internal sealed record CommunityDistinctScalar(IEnumerable<DistinctScalarResult> Results)
{
    public IEnumerable<string> Result => Results.Select(x => x.Pos).SelectMany(x => x.Columns);
}

internal sealed record DistinctScalarResult(DistinctScalarPosResult Pos);

internal sealed record DistinctScalarPosResult(IEnumerable<string> Columns);