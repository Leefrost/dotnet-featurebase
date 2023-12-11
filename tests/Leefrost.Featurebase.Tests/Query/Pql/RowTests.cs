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
    public void Constructor_ValidExpression_CommandIsCreated(string input, string expected)
    {
        var row = new Row(input);

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

}
