using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Exploratory;

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
        var includedRows = rows.ToList();
        if (includedRows.Count == 0)
            throw new ArgumentException("Columns must be selected for Extract query", nameof(rows));
        
        _filter = filter;
        _rows.AddRange(includedRows);
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
