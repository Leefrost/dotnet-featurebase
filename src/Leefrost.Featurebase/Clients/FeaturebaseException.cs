namespace Leefrost.Featurebase.Clients;

internal class FeaturebaseException : Exception
{
    public FeaturebaseException(string message) : base(message)
    { }

    public FeaturebaseException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
