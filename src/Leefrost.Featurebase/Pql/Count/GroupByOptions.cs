using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public sealed class GroupByOptions
{
    public Row? Filter { get; set; }
    public string? Condition { get; set; }
    public string? Aggregate { get; set; }
    public string? Sort { get; set; }
    public uint? Limit { get; set; }
    public uint? Offset { get; set; }

    internal string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (Filter is not null)
            builder.Append($", {Filter.Build()}");

        if (!string.IsNullOrEmpty(Condition))
            builder.Append($", condition={Condition}");

        if (!string.IsNullOrEmpty(Aggregate))
            builder.Append($", aggregate={Aggregate}");

        if (!string.IsNullOrEmpty(Sort))
            builder.Append($", sort={Sort}");

        if (Limit is not null)
            builder.Append($", limit={Limit}");

        if (Offset is not null)
            builder.Append($", offset={Offset}");

        return builder.ToString();
    }
}