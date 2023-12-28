namespace Leefrost.Featurebase.Pql;
public class ClearRow : WriteQuery
{
    private readonly string _fieldValue;

    public ClearRow(string fieldValue)
    {
        if (string.IsNullOrEmpty(fieldValue))
            throw new ArgumentException("Field value can not be null or empty", nameof(fieldValue));

        _fieldValue = fieldValue;
    }

    public override string Build()
    {
        return $"ClearRow(field={_fieldValue})";
    }
}
