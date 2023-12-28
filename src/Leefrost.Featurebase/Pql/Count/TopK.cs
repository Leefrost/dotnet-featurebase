using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public class TopK : CountQuery
{
    private readonly Rows _field;
    private readonly TopKOptions? _options;

    public TopK(Rows field)
    {
        _field = field;
    }

    public TopK(Rows field, TopKOptions options)
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
