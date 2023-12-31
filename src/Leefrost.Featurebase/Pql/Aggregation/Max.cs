﻿using System.Text;

namespace Leefrost.Featurebase.Pql.Aggregation;
public class Max : ReadQuery
{
    private readonly string _field;
    private readonly RowQuery? _query;

    public Max(string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Field is required for Max operation", nameof(field));

        _field = field;
    }

    public Max(RowQuery query, string field)
        : this(field)
    {
        _query = query;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Max(");

        if (_query is not null)
            builder.Append($"{_query.Build()}, ");

        builder.Append($"field={_field})");
        return builder.ToString();
    }
}
