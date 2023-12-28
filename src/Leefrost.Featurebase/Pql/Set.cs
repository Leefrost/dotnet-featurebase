namespace Leefrost.Featurebase.Pql;
public class Set : WriteQuery
{
    private readonly string _recordId;
    private readonly string _fieldValue;
    private readonly TimeSpan? _setAt;

    public Set(string recordId, string fieldValue)
    {
        if (string.IsNullOrEmpty(recordId))
            throw new ArgumentException("Record id can not be null or empty", nameof(recordId));

        if (string.IsNullOrEmpty(fieldValue))
            throw new ArgumentException("Field value can not be null or empty", nameof(fieldValue));

        _recordId = recordId;
        _fieldValue = fieldValue;
    }

    public Set(uint recordId, string fieldValue)
        : this(recordId.ToString(), fieldValue)
    { }

    public Set(string recordId, string fieldValue, TimeSpan setAt)
        : this(recordId, fieldValue)
    {
        _setAt = setAt;
    }

    public Set(uint recordId, string fieldValue, TimeSpan setAt)
        : this(recordId.ToString(), fieldValue, setAt)
    {
    }

    public override string Build()
    {
        return _setAt is not null 
            ? $"Set({_recordId}, field={_fieldValue}, {_setAt})" 
            : $"Set({_recordId}, field={_fieldValue})";
    }
}
