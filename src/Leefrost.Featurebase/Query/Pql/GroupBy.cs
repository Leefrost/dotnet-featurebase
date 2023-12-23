using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class GroupBy : Query
{
    private readonly uint? _offset;
    private readonly uint? _limit;
    private readonly string? _sort;
    private readonly string? _aggregate;
    private readonly string? _condition;
    private readonly Row? _filter;
    private readonly List<Rows> _queries = [];

    public GroupBy(Rows query)
    {
        _queries.Add(query);
    }

    public GroupBy(IEnumerable<Rows> queries)
    {
        _queries = queries.ToList();
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter)
        : this(queries)
    {
        _filter = filter;
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter, string? condition)
        : this(queries, filter)
    {
        _condition = condition;
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter, string? condition, string? aggregate)
        : this(queries, filter, condition)
    {
        _aggregate = aggregate;
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter, string? condition, string? aggregate, string? sort)
        : this(queries, filter, condition, aggregate)
    {
        _sort = sort;
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter, string? condition, string? aggregate, string? sort, uint? limit)
        : this(queries, filter, condition, aggregate, sort)
    {
        _limit = limit;
    }

    public GroupBy(IEnumerable<Rows> queries, Row? filter, string? condition, string? aggregate, string? sort, uint? limit, uint? offset)
        : this(queries, filter, condition, aggregate, sort, limit)
    {
        _offset = offset;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("GroupBy(");
        builder.Append(string.Join(',', _queries));

        if (_filter is not null)
            builder.Append($", {_filter.Build()}");

        if (_condition is not null)
            builder.Append($", {_condition}");

        if (_aggregate is not null)
            builder.Append($", {_aggregate}");

        if (_sort is not null)
            builder.Append($", {_sort}");

        if (_limit is not null)
            builder.Append($", {_limit}");

        if (_offset is not null)
            builder.Append($", {_offset}");

        builder.Append(')');
        return builder.ToString();
    }
}
