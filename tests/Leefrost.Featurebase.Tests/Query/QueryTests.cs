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

    [Fact]
    public void GroupBy_GroupByCity_QueryIsValid()
    {
        var row = new GroupBy(new Rows("city"));

        var result = row.Build();

        result.Should().Be("GroupBy(Rows(city))");
    }

    [Fact]
    public void TopK_GetCountForAllFromLvivCity_QueryIsValid()
    {
        var row = new TopK(new Rows("city"), new() { Filter = new Row("city", "Lviv") });

        var result = row.Build();

        result.Should().Be("TopK(Rows(city), filter=Row(city=Lviv))");
    }

    [Fact]
    public void TopN_GetCountForAllMaleGender_QueryIsValid()
    {
        var row = new TopN(new Rows("city"), new() { Filter = new Row("gender", "male") });

        var result = row.Build();

        result.Should().Be("TopN(Rows(city), Row(gender=male))");
    }

    [Fact]
    public void Max_GetMaxAge_QueryIsValid()
    {
        var row = new Max("age");

        var result = row.Build();

        result.Should().Be("Max(field=age)");
    }

    [Fact]
    public void Min_GetMinAge_QueryIsValid()
    {
        var row = new Min("age");

        var result = row.Build();

        result.Should().Be("Min(field=age)");
    }

    [Fact]
    public void Percentile_GetFrequencyOfMiddleAge_QueryIsValid()
    {
        var row = new Percentile("age", 50.0f);

        var result = row.Build();

        result.Should().Be("Percentile(field=age, nth=50)");
    }

    [Fact]
    public void Sum_GetTheSumOfAllAges_QueryIsValid()
    {
        var row = new Sum("age");

        var result = row.Build();

        result.Should().Be("Sum(field=age)");
    }

    [Fact]
    public void Extract_GetDataAboutWomen_QueryIsValid()
    {
        var row = new Extract(new Row("gender", "female"), new Rows("age"));

        var result = row.Build();

        result.Should().Be("Extract(Row(gender=female), Rows(age))");
    }
}
