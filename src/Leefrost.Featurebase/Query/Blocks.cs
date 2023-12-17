namespace Leefrost.Featurebase.Query;

public static class Blocks
{
    public static string All()
    {
        return "All()";
    }

    public static string Row(string field, int value)
    {
        return $"Row({field}={value})";
    }

    public static string Row(string field, string value)
    {
        return $"Row({field}=\"{value}\")";
    }

    public static string Row(string field, bool value)
    {
        return $"Row({field}={value.ToString().ToLower()})";
    }

    public static string RowLess(string field, int less)
    {
        return $"Row({field}<{less})";
    }

    public static string RowGreater(string field, int great)
    {
        return $"Row({great}<{field})";
    }

    public static string RowInRange(string field, int less, int greater)
    {
        return $"Row({greater}<{field}<{less})";
    }

    public static string Distinct(string expression, string targetField)
    {
        return $"Distinct({expression}, field={targetField})";
    }

    public static string Count(string expression)
    {
        return $"Count({expression})";
    }

    public static string Extract(string expression, string rowField)
    {
        return $"Extract({expression}, Rows({rowField}))";
    }

    public static string Extract(string expression, IEnumerable<string> rowFields)
    {
        var fields = rowFields
            .Select(field => $"Rows({field})");
        var rows = string.Join(",", fields);

        return $"Extract({expression}, {rows})";
    }

    public static string Union(IEnumerable<string> expressions)
    {
        var enumerable = expressions.ToList();
        if (enumerable.Count == 1)
            return enumerable[0];

        var unions = string.Join(",", enumerable);
        return $"Union({unions})";
    }


    public static string Intersect(IEnumerable<string> expressions)
    {
        var enumerable = expressions.ToList();
        if (enumerable.Count == 1)
            return enumerable[0];

        var intersection = string.Join(",", enumerable);
        return $"Intersect({intersection})";
    }
}
