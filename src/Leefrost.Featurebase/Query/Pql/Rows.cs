using System.Text;

namespace Leefrost.Featurebase.Query.Pql;

public class Rows : RowQuery
{
    private readonly string? _previous;
    private readonly string _field;
    
    private readonly string? _like;
    private readonly string? _column;

    private readonly TimeSpan _from = TimeSpan.Zero;
    private readonly TimeSpan _to = TimeSpan.Zero;

    public Rows(string field)
    {
        if (string.IsNullOrEmpty(field))
            throw new ArgumentException("Field is required for Rows query", nameof(field));

        _field = field;
    }

    public Rows(string field, string? like)
        :this(field)
    {
        _like = like;
    }

    public Rows(string field, string? like, string? column)
        : this(field, like)
    {
        _column = column;
    }

    public Rows(string field, string? like, uint? column)
        : this(field, like, column?.ToString())
    { }

    public Rows(string field, string? like, string? column, TimeSpan from, TimeSpan to)
        : this(field, like, column)
    {
        _from = from;
        _to = to;
    }


    public Rows(string field, string? like, uint? column, TimeSpan from, TimeSpan to)
        : this(field, like, column?.ToString(), from, to)
    { }


    public Rows(string field, string? like, string? column, string? previous, TimeSpan from, TimeSpan to)
        : this(field, like, column, from, to)
    {
        _previous = previous;
    }

    public Rows(string field, string? like, string? column, uint? previous, TimeSpan from, TimeSpan to)
        : this(field, like, column, previous?.ToString(), from, to)
    { }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Rows(");
        builder.Append($"{_field}");
        
        if (_like is not null) 
            builder.Append($"like={_like}");
        
        if (_column is not null) 
            builder.Append($"column={_column}");
        
        if (_previous is not null) 
            builder.Append($"previous={_previous}");
        
        if (_from != TimeSpan.Zero && _to != TimeSpan.Zero) 
            builder.Append($"from={_from.ToString()} to={_to.ToString()}");

        builder.Append(')');
        
        return builder.ToString();
    }
}
