using System.Collections.ObjectModel;

namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient: IDisposable
{
    string Provider { get; }
    Task CheckAvailabilityAsync(CancellationToken cancellationToken);
    Task<long> CountAsync(string query, CancellationToken cancellationToken);
    Task<ReadOnlyCollection<T>> SelectAsync<T>(string query, CancellationToken cancellationToken);
    Task<string> ExecuteRawPqlAsync(string query, CancellationToken cancellationToken);
}