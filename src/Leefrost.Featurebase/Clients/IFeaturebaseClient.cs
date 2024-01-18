using Leefrost.Featurebase.Pql;

namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient: IDisposable
{
    Task<int> CountAsync(CountQuery query, CancellationToken cancellationToken);
    Task<TResult> ExecuteAsync<TResult>(Query query, CancellationToken  cancellationToken);
}