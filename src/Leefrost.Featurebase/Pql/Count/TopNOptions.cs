using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public class TopNOptions
{
    public uint? N { get; set; }
    public Row? Filter { get; set; }

    internal string ExtendQuery()
    {
        var builder = new StringBuilder();

        if (Filter is not null)
            builder.Append($", {Filter.Build()}");

        if (N is not null)
            builder.Append($", n={N}");

        return builder.ToString();
    }
}