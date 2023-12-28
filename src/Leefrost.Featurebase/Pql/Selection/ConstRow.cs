using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;
public class ConstRow : RowQuery
{
    private readonly List<string> _columns;

    public ConstRow(IEnumerable<string> columns)
    {
        var columnList = columns.ToList();
        if (columnList.Count == 0)
            throw new ArgumentException("ConstRow must have at least one column key");

        _columns = columnList;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("ConstRow(columns=[");
        builder.Append(string.Join(",", _columns));

        builder.Append("])");
        return builder.ToString();
    }
}
