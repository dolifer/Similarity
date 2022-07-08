using FluentAssertions;
using Similarity.Core.Algorithms;
using Xunit;

namespace Similarity.Tests.Jaccard;

public class JaccardSimilarityCalculatorTests
{
    private const string Left = "name == \"John\" && age > 10 && country == \"UA\" && isVerified == true";
    private readonly JaccardSimilarityCalculator _calc;

    public JaccardSimilarityCalculatorTests() => _calc = new JaccardSimilarityCalculator();

    [Fact]
    public void Exact_Match()
    {
        // arrange
        _calc.Calculate(Left, Left).Should().Be(1);
    }

    [Theory]
    [InlineData("age < 10")]
    [InlineData("age > 10")]
    [InlineData("name == \"John\"")]
    [InlineData("name != \"John\"")]
    [InlineData("country == \"UA\"")]
    [InlineData("country != \"UA\"")]
    [InlineData("isVerified == true")]
    [InlineData("isVerified == false")]
    public void Partial_Match(string right)
    {
        _calc.Calculate(Left, right).Should().BeApproximately(0.25, 0.1);
    }

    [Theory]
    [InlineData("name == \"John\" && age > 10")]
    [InlineData("name == \"John\" && age < 10")]
    [InlineData("age > 10 && country == \"UA\"")]
    [InlineData("age < 10 && country == \"UA\"")]
    [InlineData("country == \"UA\" && isVerified == true")]
    [InlineData("country == \"UA\" && isVerified != true")]
    public void Almost_Match(string right)
    {
        _calc.Calculate(Left, right).Should().BeApproximately(0.6, 0.1);
    }

    [Fact]
    public void Almost_Identical()
    {
        // arrange
        const string right = "age > 10 && country == \"UA\" && isVerified";

        _calc.Calculate(Left, right).Should().BeApproximately(0.8, 0.1);
    }
}
