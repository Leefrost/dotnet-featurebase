namespace Leefrost.Featurebase.Query.Pql;
public class All : PqlRowQuery
{
    public override string Build()
    {
        return "All()";
    }
}
