using FluentAssertions;
using Leefrost.Featurebase.Query.Pql;

namespace Leefrost.Featurebase.Tests.Query.Pql;

public class RowTests
{
    [Theory]
    [InlineData("field=10", "Row(field=10)")]
    [InlineData("0<field<10", "Row(0<field<10)")]
    [InlineData("0<=field<=10", "Row(0<=field<=10)")]
    [InlineData("filed!='text'", "Row(filed!='text')")]
    public void Constructor_ValidExpression_QueryIsCreated(string expression, string expected)
    {
        var row = new Row(expression);

        var query = row.Build();

        query.Should().Be(expected);
    }

    [Theory]
    [InlineData("=10")]
    [InlineData("1")]
    public void Constructor_InvalidExpression_ExceptionThrown(string input)
    {
        var act = () => new Row(input);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Constructor_ValidExpressionWithRange_QueryIsCreated()
    {
        var row = new Row("field=value", TimeSpan.FromDays(1), TimeSpan.FromDays(2));

        var query = row.Build();

        query.Should().Be("Row(field=value,from=1.00:00:00,to=2.00:00:00)");
    }

    [Theory]
    [InlineData("field", "value", "Row(field=value)")]
    [InlineData("field", "has_enabled", "Row(field=has_enabled)")]
    public void Constructor_ValidStringParameter_QueryIsCreated(string field, string value, string expected)
    {
        var row = new Row(field, value);

        var query = row.Build();

        query.Should().Be(expected);
    }

    [Theory]
    [InlineData("field", 10, "Row(field=10)")]
    [InlineData("field", -10, "Row(field=-10)")]
    public void Constructor_ValidNumberParameter_QueryIsCreated(string field, int value, string expected)
    {
        var row = new Row(field, value);

        var query = row.Build();

        query.Should().Be(expected);
    }

    [Theory]
    [InlineData("field", 10L, "Row(field=10)")]
    [InlineData("field", -10L, "Row(field=-10)")]
    public void Constructor_ValidLongParameter_QueryIsCreated(string field, long value, string expected)
    {
        var row = new Row(field, value);

        var query = row.Build();

        query.Should().Be(expected);
    }

    [Theory]
    [InlineData("field", true, "Row(field=true)")]
    [InlineData("field", false, "Row(field=false)")]
    public void Constructor_ValidBooleanParameter_QueryIsCreated(string field, bool value, string expected)
    {
        var row = new Row(field, value);

        var query = row.Build();

        query.Should().Be(expected);
    }
}
