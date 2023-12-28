using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public class Count : Query
{
    private readonly RowQuery? _query;
    private readonly Distinct? _distinctQuery;

    public Count(RowQuery query)
    {
        _query = query;
    }

    public Count(Distinct query)
    {
        _distinctQuery = query;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Count(");

        if (_query is not null)
            builder.Append(_query.Build());

        if (_distinctQuery is not null)
            builder.Append(_distinctQuery.Build());

        builder.Append(')');
        return builder.ToString();
    }
}
