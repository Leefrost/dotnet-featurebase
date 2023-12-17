namespace Leefrost.Featurebase.Query.Pql;

public abstract class Query
{
    public abstract string Build();
}

public abstract class RowQuery : Query
{

}