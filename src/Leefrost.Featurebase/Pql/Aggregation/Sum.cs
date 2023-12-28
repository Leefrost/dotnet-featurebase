using System.Text;

namespace Leefrost.Featurebase.Pql.Aggregation;

public class Sum : ReadQuery
{
    private readonly string _field;
    private readonly RowQuery? _query;

    public Sum(string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Filed is required for Sum query", nameof(field));

        _field = field;
    }

    public Sum(RowQuery query, string field)
        : this(field)
    {
        _query = query;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Sum(");

        if (_query is not null)
            builder.Append($"{_query.Build()}, ");

        builder.Append($"field={_field})");
        return builder.ToString();
    }
}
