namespace Leefrost.Featurebase.Pql;

public abstract class Query
{
    public abstract string Build();
}

public abstract class RowQuery : Query
{

}