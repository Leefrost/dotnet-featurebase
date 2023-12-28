namespace Leefrost.Featurebase.Pql;

public abstract class Query
{
    public abstract string Build();
}

public abstract class ReadQuery : Query
{ }

public abstract class WriteQuery : Query
{ }

public abstract class RowQuery : ReadQuery
{ }

public abstract class CountQuery : ReadQuery
{ }