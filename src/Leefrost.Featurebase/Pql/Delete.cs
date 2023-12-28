namespace Leefrost.Featurebase.Pql;

public class Delete : WriteQuery
{
    private readonly RowQuery _query;

    public Delete(RowQuery query)
    {
        _query = query;
    }
    
    public override string Build()
    {
        return $"Delete({_query.Build()})";
    }
}
