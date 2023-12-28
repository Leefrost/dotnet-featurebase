namespace Leefrost.Featurebase.Pql;
public class Clear : WriteQuery
{
    private readonly string _recordId;
    private readonly string _fieldValue;

    public Clear(string recordId, string fieldValue)
    {
        if (string.IsNullOrEmpty(recordId))
            throw new ArgumentException("Record id can not be null or empty", nameof(recordId));

        if (string.IsNullOrEmpty(fieldValue))
            throw new ArgumentException("Field value can not be null or empty", nameof(fieldValue));

        _recordId = recordId;
        _fieldValue = fieldValue;
    }

    public Clear(uint recordId, string fieldValue)
        : this(recordId.ToString(), fieldValue)
    { }

    public override string Build()
    {
        return $"Clear({_recordId}, field={_fieldValue})";
    }
}
