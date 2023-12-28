namespace Leefrost.Featurebase.Pql.Membership;
public class IncludesColumn : Query
{
    private readonly RowQuery _query;
    private readonly string _column;

    public IncludesColumn(RowQuery query, string column)
    {
        if (string.IsNullOrEmpty(column))
            throw new ArgumentException("Column field should not be empty");

        _query = query;
        _column = column;
    }

    public IncludesColumn(RowQuery query, int column)
        : this(query, column.ToString())
    { }

    public override string Build()
    {
        return $"IncludesColumn({_query.Build()}, column={_column})";
    }
}
