using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
public class Intersect : PqlRowQuery
{
    private readonly List<PqlRowQuery> _rows = [];

    public Intersect(PqlRowQuery row1, PqlRowQuery row2)
    {
        _rows.Add(row1);
        _rows.Add(row2);
    }

    public Intersect(IEnumerable<PqlRowQuery> rows)
    {
        _rows = rows.ToList();
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
