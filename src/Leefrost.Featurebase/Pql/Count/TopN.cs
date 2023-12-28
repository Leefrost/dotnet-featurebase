using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public class TopN : CountQuery
{
    private readonly Rows _field;
    private readonly TopNOptions? _options;

    public TopN(Rows field)
    {
        _field = field;
    }

    public TopN(Rows field, TopNOptions options)
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
