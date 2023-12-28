using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public class TopKOptions
{
    public uint? K { get; set; }
    public Row? Filter { get; set; }
    public TimeSpan? From { get; set; }
    public TimeSpan? To { get; set; }

    internal string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (K is not null)
            builder.Append($", k={K}");

        if (Filter is not null)
            builder.Append($", filter={Filter.Build()}");

        if (From is not null)
            builder.Append($", from={From}");

        if (To is not null)
            builder.Append($", to={To}");

        return builder.ToString();
    }
}