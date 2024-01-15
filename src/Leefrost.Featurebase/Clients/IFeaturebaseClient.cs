using Leefrost.Featurebase.Pql;

namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient: IDisposable
{
    Task<TResult> ExecuteAsync<TResult>(Query query, CancellationToken  cancellationToken);
    Task<IReadOnlyList<TResult>> ExecuteManyAsync<TResult>(Query query, CancellationToken cancellationToken);
    Task<int> CountAsync(CountQuery query, CancellationToken cancellationToken);


    Task<long> CountAsync(string query, CancellationToken cancellationToken);
    Task<IReadOnlyList<string>> SelectAsync(string query, CancellationToken cancellationToken);
    Task<string> ExecuteRawPqlAsync(string query, CancellationToken cancellationToken);
}