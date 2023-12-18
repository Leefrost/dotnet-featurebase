namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient: IDisposable
{
    string Provider { get; }
    Task CheckAvailabilityAsync(CancellationToken cancellationToken);
    Task<long> CountAsync(string query, CancellationToken cancellationToken);
    Task<IReadOnlyList<string>> SelectAsync(string query, CancellationToken cancellationToken);
    Task<string> ExecuteRawPqlAsync(string query, CancellationToken cancellationToken);
}