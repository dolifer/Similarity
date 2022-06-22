using FluentAssertions;
using Xunit;

namespace Jaccard.Tests;

public class TextSimilarityTests
{
    private readonly string[] Left = { "name == \"John\"", "age > 10", "country == \"UA\"", "isVerified" };

    [Fact]
    public void Quarter_Identical()
    {
        // arrange
        var right = new[] { "age > 10" };

        JaccardIndexCalculator.GetIndex(Left, right).Should().Be(0.25);
    }

    [Fact]
    public void Half_Identical()
    {
        // arrange
        var right = new[] { "age > 10", "name == \"John\"" };

        JaccardIndexCalculator.GetIndex(Left, right).Should().Be(0.5);
    }

    [Fact]
    public void Almost_Identical()
    {
        // arrange
        var right = new[] { "age > 10", "country == \"UA\"", "isVerified" };

        JaccardIndexCalculator.GetIndex(Left, right).Should().Be(0.75);
    }
}