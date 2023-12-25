using System.Data.Common;
using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class RowsOptions
{
    public string? Previous { get; set; }
    public string? Like { get; set; }
    public string? Column { get; set; }
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }

    internal string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (!string.IsNullOrEmpty(Like)) 
            builder.Append($", like={Like}");

        if (!string.IsNullOrEmpty(Column))
            builder.Append($", column={Column}");

        if (!string.IsNullOrEmpty(Previous))
            builder.Append($", previous={Previous}");

        if (From != TimeSpan.Zero && To != TimeSpan.Zero)
            builder.Append($"from={From.ToString()} to={To.ToString()}");

        return builder.ToString();
    }
}

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
