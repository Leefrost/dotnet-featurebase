using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Leefrost.Featurebase.Query.Pql;

public class Row : PqlRowQuery
{
    private readonly string? _field;
    private readonly string? _value;
    private readonly string? _expression;

    private readonly TimeSpan _from = TimeSpan.Zero;
    private readonly TimeSpan _to = TimeSpan.Zero;

    private static bool IsValidExpression(string expression)
    {
        expression = Regex.Replace(expression, @"\s+", "");
        if (string.IsNullOrEmpty(expression))
        {
            return false;
        }
        
        var parts = Regex.Split(expression, @"([><=!]+)");

        if (parts.Length < 2 || parts.Length % 2 == 0)
        {
            return false;
        }

        var validOperators = new[] { ">", "<", "=", "<=", ">=", "==", "!=" };

        for (int i = 0; i < parts.Length; i++)
        {
            if (i % 2 == 0)
            {
                if (!IsValidValue(parts[i]))
                {
                    return false;
                }
            }
            else
            {
                if (!validOperators.Contains(parts[i]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsValidValue(string value)
    {
        if (double.TryParse(value, out double _))
        {
            return true;
        }

        if (TimeSpan.TryParse(value, out TimeSpan _))
        {
            return true;
        }

        return !string.IsNullOrEmpty(value);
    }

    public Row(string expression)
    {
        if (!IsValidExpression(expression))
            throw new ArgumentException(
                $"Expression {expression} is not valid. Expression must be a valid logical one");

        _expression = expression;
    }

    public Row(string field, string value)
    {
        if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
            throw new ArgumentException("Field and Value must have a value");
        
        _field = field;
        _value = value;
    }

    public Row(string field, bool value)
        : this(field, value.ToString().ToLower())
    { }

    public Row(string field, int value)
        : this(field, value.ToString())
    { }

    public Row(string field, long value)
        : this(field, value.ToString())
    { }

    public Row(string field, double value)
        : this(field, value.ToString(CultureInfo.InvariantCulture))
    { }

    public Row(string field, TimeSpan value)
        : this(field, value.ToString())
    { }

    public Row(string field, string value, TimeSpan from, TimeSpan to)
        : this(field, value)
    {
        _from = from;
        _to = to;
    }

    public Row(string expression, TimeSpan from, TimeSpan to)
        : this(expression)
    {
        _from = from;
        _to = to;
    }

    public override string Build()
    {
        var builder = new StringBuilder();
        builder.Append("Row(");

        if (_expression is not null)
            builder.Append(_expression);
        else
            builder.Append($"{_field}={_value}");

        if (_from != TimeSpan.Zero && _to != TimeSpan.Zero)
            builder.Append($",from={_from.ToString()},to={_to.ToString()}");

        builder.Append(')');

        return builder.ToString();
    }
}
