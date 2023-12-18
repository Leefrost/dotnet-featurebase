﻿using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class UnionRows : RowQuery
{
    private readonly List<Rows> _rows;

    public UnionRows(IEnumerable<Rows> rows)
    {
        var queries = rows.ToList();
        if (queries.Count == 0)
            throw new ArgumentException("UnionRows must have at least one row argument");

        _rows = queries;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("UnionRows(");
        builder.Append(string.Join(',', _rows.Select(row => row.Build())));
        builder.Append(')');

        return builder.ToString();
    }
}
