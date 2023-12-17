using FluentAssertions;
using Leefrost.Featurebase.Query.Pql;

namespace Leefrost.Featurebase.Tests.Query;
public class QueryTests
{
    [Fact]
    public void Unique_OnlyMalesWithDiscount_QueryIsValid()
    {
        var row = new Distinct(new Row("gender", "male"), "has_discount");

        var result = row.Build();

        result.Should().Be("Distinct(Row(gender=male), field=has_discount)");
    }

    [Fact]
    public void Unique_FromLvivOrOlderThan21_QueryIsValid()
    {
        var row = new Distinct(new Intersect(new Row("age>21"), new Row("city", "Lviv")), "has_discount");

        var result = row.Build();

        result.Should().Be("Distinct(Intersect(Row(age>21),Row(city=Lviv)), field=has_discount)");
    }
}
