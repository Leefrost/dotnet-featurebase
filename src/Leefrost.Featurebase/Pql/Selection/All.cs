namespace Leefrost.Featurebase.Pql.Selection;
public class All : RowQuery
{
    public override string Build()
    {
        return "All()";
    }
}
