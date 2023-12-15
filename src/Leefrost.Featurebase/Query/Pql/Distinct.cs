using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
internal class Distinct : PqlQuery
{
    private readonly PqlRowQuery _rowQuery;
    private readonly string _field;
    private readonly string? _index;

    public Distinct(PqlRowQuery rowQuery, string field)
    {
        _rowQuery = rowQuery;
        _field = field;
    }

    public Distinct(PqlRowQuery rowQuery, string field, string index)
        : this(rowQuery, field)
    {
        if (string.IsNullOrEmpty(index))
            throw new ArgumentException("Index must have a value");

        _index = index;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Distinct(");

        builder.Append(_rowQuery.Build());
        builder.Append($", field={_field}");

        if (!string.IsNullOrEmpty(_index))
            builder.Append($", index={_index}");

        builder.Append(')');
        return builder.ToString();
    }
}
