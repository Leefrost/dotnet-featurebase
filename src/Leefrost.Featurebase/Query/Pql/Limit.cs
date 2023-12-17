﻿using System.Text;

namespace Leefrost.Featurebase.Query.Pql;
public class Limit : RowQuery
{
    private readonly RowQuery _query;

    private readonly uint _limit;
    private readonly uint _offset;

    public Limit(RowQuery query)
    {
        _query = query;
    }

    public Limit(RowQuery query, uint limit, uint offset)
        : this(query)
    {
        _limit = limit;
        _offset = offset;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Limit(");
        builder.Append(_query.Build());

        if (_limit > 0)
            builder.Append($", limit={_limit}");

        if (_offset > 0)
            builder.Append($", offset={_offset}");

        builder.Append(')');
        return builder.ToString();
    }
}
