using System.Text;
using Leefrost.Featurebase.Pql.Selection;

namespace Leefrost.Featurebase.Pql.Count;

public sealed class GroupBy : CountQuery
{
    private readonly List<Rows> _queries = [];
    private readonly GroupByOptions? _options;

    public GroupBy(Rows query)
    {
        _queries.Add(query);
    }

    public GroupBy(Rows query, GroupByOptions? options)
        : this(query)
    {
        _options = options;
    }

    public GroupBy(IEnumerable<Rows> queries)
    {
        _queries = queries.ToList();
    }

    public GroupBy(IEnumerable<Rows> queries, GroupByOptions? options)
        : this(queries)
    {
        _options = options;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("GroupBy(");
        builder.Append(string.Join(',', _queries.Select(row => row.Build())));

        if (_options is not null)
            builder.Append(_options.ExtendQuery());

        builder.Append(')');
        return builder.ToString();
    }
}
