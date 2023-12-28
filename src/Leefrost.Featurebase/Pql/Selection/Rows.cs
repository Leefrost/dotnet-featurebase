using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;

public class Rows : RowQuery
{
    private readonly string _field;
    private readonly RowsOptions? _options;

    public Rows(string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Field is required for Rows query", nameof(field));

        _field = field;
    }

    public Rows(string field, RowsOptions? options)
        : this(field)
    {
        _options = options;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Rows(");
        builder.Append($"{_field}");

        if (_options is not null)
            builder.Append($"{_options.ExtendQuery()}");

        builder.Append(')');
        return builder.ToString();
    }
}
