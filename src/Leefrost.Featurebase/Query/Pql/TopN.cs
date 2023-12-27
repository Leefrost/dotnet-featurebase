using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class TopNOptions
{
    public uint? N { get; set; }
    public Row? Filter { get; set; }
    
    public string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (Filter is not null)
            builder.Append($", {Filter.Build()}");

        if (N is not null)
            builder.Append($", n={N}");

        return builder.ToString();
    }
}

public class TopN : Query
{
    private readonly Rows _field;
    private readonly TopNOptions? _options;

    public TopN(Rows field)
    {
        _field = field;
    }

    public TopN(Rows field, TopNOptions? options)
    : this(field)
    {
        _options = options;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"TopN({_field.Build()}");

        if (_options is not null)
            builder.Append(_options.ExtendQuery());

        builder.Append(')');
        return builder.ToString();
    }
}
