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

    [Fact]
    public void Xor_LivedInLvivOrKyivButNotInBoth_QueryIsValid()
    {
        var row = new Xor(new Row("city", "Lviv"), new Row("city", "Kyiv"));

        var result = row.Build();

        result.Should().Be("Xor(Row(city=Lviv),Row(city=Kyiv))");
    }

    [Fact]
    public void IncludesColumn_HavePersonLiveInLviv_QueryIsValid()
    {
        var row = new IncludesColumn(new Row("city", "Lviv"), "person");

        var result = row.Build();

        result.Should().Be("IncludesColumn(Row(city=Lviv), column=person)");
    }

    [Fact]
    public void Count_CountAllMens_QueryIsValid()
    {
        var row = new Count(new Row("gender", "male"));

        var result = row.Build();

        result.Should().Be("Count(Row(gender=male))");
    }

    [Fact]
    public void Count_CountAllUniqueMens_QueryIsValid()
    {
        var row = new Count(new Distinct(new Row("gender", "male"), "personId"));

        var result = row.Build();

        result.Should().Be("Count(Distinct(Row(gender=male), field=personId))");
    }
}
