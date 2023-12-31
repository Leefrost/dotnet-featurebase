﻿using System.Text;

namespace Leefrost.Featurebase.Pql.Selection;

public class Distinct : ReadQuery
{
    private readonly RowQuery _rowQuery;
    private readonly string _field;
    private readonly string? _index;

    public Distinct(RowQuery rowQuery, string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Filed must have a value", nameof(field));

        _rowQuery = rowQuery;
        _field = field;
    }

    public Distinct(RowQuery rowQuery, string field, string index)
        : this(rowQuery, field)
    {
        if (string.IsNullOrEmpty(index))
            throw new ArgumentException("Index must have a value", nameof(index));

        _index = index;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Distinct(");

        builder.Append(_rowQuery.Build());
        builder.Append($", field={_field}");

        if (!string.IsNullOrEmpty(_index))
            builder.Append($", index={_index}");

        builder.Append(')');
        return builder.ToString();
    }
}
