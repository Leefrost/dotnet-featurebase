using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
public class Extract : Query
{
    private readonly RowQuery _filter;
    private readonly List<Rows> _rows = [];

    public Extract(RowQuery filter, Rows rows)
    {
        _filter = filter;
        _rows.Add(rows);
    }

    public Extract(RowQuery filter, IEnumerable<Rows> rows)
    {
        _filter = filter;
        _rows.AddRange(rows);
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"Extract({_filter.Build()}, ");
        builder.Append(string.Join(",", _rows.Select(rows => rows.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
