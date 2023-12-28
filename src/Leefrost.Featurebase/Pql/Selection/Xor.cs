using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;
public class Xor : RowQuery
{
    private readonly List<RowQuery> _rows = [];

    public Xor(RowQuery row1, RowQuery row2)
    {
        _rows.Add(row1);
        _rows.Add(row2);
    }

    public Xor(IEnumerable<RowQuery> rows)
    {
        var queries = rows.ToList();
        if (queries.Count < 2)
            throw new ArgumentException("Xor must have at least 2 row arguments");

        _rows.AddRange(queries);
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Xor(");
        builder.Append(string.Join(',', _rows.Select(row => row.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
