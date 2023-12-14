namespace Leefrost.Featurebase.Query.Pql;

public abstract class PqlQuery
{
    public abstract string Build();
}

public abstract class PqlRowQuery : PqlQuery
{

}