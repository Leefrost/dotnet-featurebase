using System.Text;

namespace Leefrost.Featurebase.Pql.Aggregation;
public class Min : Query
{
    private readonly string _field;
    private readonly RowQuery? _query;

    public Min(string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Field is required for Min operation", nameof(field));

        _field = field;
    }

    public Min(RowQuery query, string field)
        : this(field)
    {
        _query = query;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Min(");

        if (_query is not null)
            builder.Append($"{_query.Build()}, ");

        builder.Append($"field={_field})");
        return builder.ToString();
    }
}
