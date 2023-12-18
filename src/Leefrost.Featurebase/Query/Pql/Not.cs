using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
public class Not : RowQuery
{
    private readonly RowQuery _row;

    public Not(RowQuery row)
    {
        _row = row;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"Not({_row.Build()})");

        return builder.ToString();
    }
}
