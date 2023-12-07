namespace Leefrost.Featurebase.Clients;

public interface IFeaturebaseClient
{
    string Provider { get; }
    Task CheckAvailabilityAsync(string query, CancellationToken cancellationToken);
    Task<int> ExecuteAsync(string query, CancellationToken cancellationToken);
}