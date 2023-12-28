using System.Text;

namespace Leefrost.Featurebase.Pql.Aggregation;
public class Percentile : Query
{
    private readonly string _field;
    private readonly float _nth;
    private readonly RowQuery? _query;

    public Percentile(string field, float nth)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Field name is required for Percentile query", nameof(field));

        _field = field;
        _nth = nth;
    }

    public Percentile(string field, float nth, RowQuery query)
        : this(field, nth)
    {
        _query = query;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"Percentile(field={_field}, nth={_nth}");

        if (_query is not null)
            builder.Append($", filter={_query.Build()}");

        builder.Append(')');
        return builder.ToString();
    }
}
