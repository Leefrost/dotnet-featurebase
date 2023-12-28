namespace Leefrost.Featurebase.Pql;

public class Store : WriteQuery
{
    private readonly RowQuery _query;
    private readonly string _fieldValue;

    public Store(RowQuery query, string fieldValue)
    {
        if (string.IsNullOrEmpty(fieldValue))
            throw new ArgumentException("Field value can not be null or empty", nameof(fieldValue));

        _query = query;
        _fieldValue = fieldValue;
    }

    public override string Build()
    {
        return $"Store({_query.Build()}, field={_fieldValue})";
    }
}
