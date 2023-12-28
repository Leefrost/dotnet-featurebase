using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;
public class Intersect : RowQuery
{
    private readonly List<RowQuery> _rows = [];

    public Intersect(RowQuery row1, RowQuery row2)
    {
        _rows.Add(row1);
        _rows.Add(row2);
    }

    public Intersect(IEnumerable<RowQuery> rows)
    {
        var queries = rows.ToList();
        if (queries.Count == 0)
            throw new ArgumentException("Intersect must have at least one row argument");

        _rows.AddRange(queries);
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Intersect(");
        builder.Append(string.Join(',', _rows.Select(row => row.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
