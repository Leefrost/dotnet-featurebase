using FluentAssertions;
using Leefrost.Featurebase.Query.Pql;

namespace Leefrost.Featurebase.Tests.Query.Pql;

public class RowsTests
{
    [Theory]
    [InlineData("field_name", "Rows(field_name)")]
    public void Constructor_ValidExpression_QueryIsCreated(string field, string expected)
    {
        var row = new Rows(field);

        var query = row.Build();

        query.Should().Be(expected);
    }
}
