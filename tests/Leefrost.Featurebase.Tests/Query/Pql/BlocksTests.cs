using FluentAssertions;
using static Leefrost.Featurebase.Query.Blocks;

namespace Leefrost.Featurebase.Tests.Query.Pql;

public class BlocksTests
{
    [Fact]
    public void All_CreateAllCommand_ReturnAll()
    {
        var result = All();

        result.Should().Be("All()");
    }

    [Theory]
    [InlineData("row", 0, "Row(row=0)")]
    [InlineData("row", 10, "Row(row=10)")]
    public void Row_CreateIntRowCommand_RowIsCorrect(string field, int value, string expected)
    {
        var result = Row(field, value);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("row", "item1", "Row(row=\"item1\")")]
    public void Row_CreateStringRowCommand_RowIsCorrect(string field, string value, string expected)
    {
        var result = Row(field, value);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("row", true, "Row(row=true)")]
    [InlineData("row", false, "Row(row=false)")]
    public void Row_CreateBooleanRowCommand_RowIsCorrect(string field, bool value, string expected)
    {
        var result = Row(field, value);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("row", 10, "Row(row<10)")]
    public void Row_CreateRowLessCommand_RowIsCorrect(string field, int value, string expected)
    {
        var result = RowLess(field, value);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("row", 10, "Row(10<row)")]
    public void Row_CreateRowGreaterCommand_RowIsCorrect(string field, int value, string expected)
    {
        var result = RowGreater(field, value);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("row", 10, 20, "Row(10<row<20)")]
    public void Row_CreateRowInRangeCommand_RowIsCorrect(string field, int less, int greater, string expected)
    {
        var result = RowInRange(field, greater, less);

        result.Should().Be(expected);
    }

    [Fact]
    public void Distinct_CreateDistinctCommand_DistinctIsCorrect()
    {
        var result = Distinct("Row(field=1)", "index");

        result.Should().Be("Distinct(Row(field=1), field=index)");
    }

    [Fact]
    public void Count_CreateCountCommand_CountIsCorrect()
    {
        var result = Count("Row(field=1)");

        result.Should().Be("Count(Row(field=1))");
    }

    [Fact]
    public void Extract_CreateExtractCommand_ExtractIsCorrect()
    {
        var result = Extract("Row(field=1)", "otherField");

        result.Should().Be("Extract(Row(field=1), Rows(otherField))");
    }

    [Fact]
    public void Extract_CreateExtractWithMultipleRowsCommand_ExtractIsCorrect()
    {
        var result = Extract("Row(field=1)", new[] { "field1", "field2" });

        result.Should().Be("Extract(Row(field=1), Rows(field1),Rows(field2))");
    }

    [Theory]
    [InlineData(new[] { "Row(row1=1)", "Row(row2=2)" }, "Union(Row(row1=1),Row(row2=2))")]
    [InlineData(new[] { "Row(row1=1)" }, "Row(row1=1)")]
    public void Union_CreateUnionCommand_UnionIsCorrect(IEnumerable<string> expressions, string expected)
    {
        var result = Union(expressions);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(new[] { "Row(row1=1)", "Row(row2=2)" }, "Intersect(Row(row1=1),Row(row2=2))")]
    [InlineData(new[] { "Row(row1=1)" }, "Row(row1=1)")]
    public void Intersect_CreateIntersectCommand_IntersectIsCorrect(IEnumerable<string> expressions, string expected)
    {
        var result = Intersect(expressions);

        result.Should().Be(expected);
    }
}
