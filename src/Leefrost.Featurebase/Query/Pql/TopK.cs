using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class TopKOptions
{
    public Row? Filter { get; set; }
    public uint? Limit { get; set; }
    public string? Sort { get; set; }

    public string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (Filter is not null)
            builder.Append($", filter={Filter.Build()}");

        if (Limit is not null)
            builder.Append($", limit={Limit}");

        if (!string.IsNullOrEmpty(Sort))
            builder.Append($", sort={Sort}");

        return builder.ToString();
    }
}

public class TopK : Query
{
    private readonly Rows _field;
    private readonly TopKOptions? _options;

    public TopK(Rows field)
    {
        _field = field;
    }

    public TopK(Rows field, TopKOptions? options)
    : this(field)
    {
        _options = options;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"TopK({_field.Build()}");

        if (_options is not null)
            builder.Append(_options.ExtendQuery());

        builder.Append(')');
        return builder.ToString();
    }
}
