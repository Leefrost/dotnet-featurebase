using FluentAssertions;
using Leefrost.Featurebase.Query.Pql;

namespace Leefrost.Featurebase.Tests.Query;
public class QueryTests
{
    [Fact]
    public void Unique_OnlyMalesByDiscount_QueryIsValid()
    {
        var row = new Distinct(new Row("gender", "male"), "has_discount");

        var result = row.Build();

        result.Should().Be("Distinct(Row(gender=male), field=has_discount)");
    }

    [Fact]
    public void FromLvivOrOlderThan21_QueryIsValid()
    {
        var row = new Intersect(new Row("age>21"), new Row("city", "Lviv"));

        var result = row.Build();

        result.Should().Be("Intersect(Row(age>21),Row(city=Lviv))");
    }

    [Fact]
    public void GenderMaleOrFemale_QueryIsValid()
    {
        var row = new Union(new Row("gender", "male"), new Row("gender", "female"));

        var result = row.Build();

        result.Should().Be("Union(Row(gender=male),Row(gender=female))");
    }

    [Fact]
    public void Diff_BetweenMales_And_Females_QueryIsValid()
    {
        var row = new Difference(new Row("gender", "male"), new Row("gender", "female"));

        var result = row.Build();

        result.Should().Be("Difference(Row(gender=male),Row(gender=female))");
    }

    [Fact]
    public void Not_HasADiscount_QueryIsValid()
    {
        var row = new Not(new Row("has_discount", true));

        var result = row.Build();

        result.Should().Be("Not(Row(has_discount=true))");
    }
}
