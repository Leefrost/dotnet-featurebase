using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;
public class Difference : RowQuery
{
    private readonly List<RowQuery> _rows = [];

    public Difference(RowQuery row1, RowQuery row2)
    {
        _rows.Add(row1);
        _rows.Add(row2);
    }

    public Difference(IEnumerable<RowQuery> rows)
    {
        var queries = rows.ToList();
        if (queries.Count < 2)
            throw new ArgumentException("Difference must have at least 2 rows to compare");

        _rows.AddRange(queries);
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Difference(");
        builder.Append(string.Join(',', _rows.Select(row => row.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
