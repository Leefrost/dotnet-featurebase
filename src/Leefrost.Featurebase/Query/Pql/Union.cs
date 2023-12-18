using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
public class Union : RowQuery
{
    private readonly List<RowQuery> _rows = [];

    public Union(RowQuery row1, RowQuery row2)
    {
        _rows.Add(row1);
        _rows.Add(row2);
    }

    public Union(IEnumerable<RowQuery> rows)
    {
        _rows = rows.ToList();
    }
    
    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Union(");
        builder.Append(string.Join(',', _rows.Select(row => row.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
