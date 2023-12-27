﻿using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class TopKOptions
{
    public uint? K { get; set; }
    public Row? Filter { get; set; }
    public TimeSpan? From { get; set; }
    public TimeSpan? To { get; set; }

    public string ExtendQuery()
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

public class TopK : Query
{
    private readonly Rows _field;
    private readonly TopKOptions? _options;

    public TopK(Rows field)
    {
        _field = field;
    }

    public TopK(Rows field, TopKOptions? options)
    : this(field)
    {
        _options = options;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append($"TopK({_field.Build()}");

        if (_options is not null)
            builder.Append(_options.ExtendQuery());

        builder.Append(')');
        return builder.ToString();
    }
}