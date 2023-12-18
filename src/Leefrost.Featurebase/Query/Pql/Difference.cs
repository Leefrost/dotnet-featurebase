using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
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
        _rows = rows.ToList();
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
