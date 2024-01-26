using Leefrost.Featurebase.Pql;

namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient: IDisposable
{
    Task<long> CountAsync(CountQuery query, CancellationToken cancellationToken);

    Task<TResult> GetAsync<TResult>(Query query, CancellationToken  cancellationToken);

    Task<IReadOnlyCollection<TResult>> GetManyAsync<TResult>(Query query, CancellationToken cancellationToken);
}