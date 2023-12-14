using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
internal class Distinct : PqlQuery
{
    private readonly PqlRowQuery _rowQuery;
    private readonly string _field;

    public Distinct(PqlRowQuery rowQuery, string field)
    {
        _rowQuery = rowQuery;
        _field = field;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Distinct(");

        builder.Append(_rowQuery.Build());
        builder.Append($", field={_field})");

        return builder.ToString();
    }
}
