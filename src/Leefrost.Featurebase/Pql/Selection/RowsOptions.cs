using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;

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