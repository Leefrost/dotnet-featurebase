namespace Leefrost.Featurebase.Query.Pql;
public class All : RowQuery
{
    public override string Build()
    {
        return "All()";
    }
}
