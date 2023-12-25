using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public sealed class GroupByOptions
{
    public Row? Filter { get; set; }
    public string? Condition { get; set; }
    public string? Aggregate { get; set; }
    public string? Sort { get; set; }
    public uint? Limit { get; set; }
    public uint? Offset { get; set; }

    public string ExtendQuery()
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

public sealed class GroupBy : Query
{
    private readonly List<Rows> _queries = [];
    private readonly GroupByOptions? _options;

    public GroupBy(Rows query)
    {
        _queries.Add(query);
    }

    public GroupBy(Rows query, GroupByOptions? options)
        :this(query)
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
        builder.Append(string.Join(',', _queries.Select(row=>row.Build())));

        if (_options is not null)
            builder.Append(_options.ExtendQuery());

        builder.Append(')');
        return builder.ToString();
    }
}
