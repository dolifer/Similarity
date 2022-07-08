using FluentAssertions;
using Similarity.Core.Algorithms;
using Xunit;

namespace Similarity.Tests.Jaccard;

public class IndexCalculatorTests
{
    [Fact]
    public void GetIndex_Throws_ArgumentNullException()
    {
        // arrange
        var col = new[] { 0 };

        // act & assert
        Assert.Throws<ArgumentNullException>(() => JaccardIndexCalculator.GetIndex(null, col));
        Assert.Throws<ArgumentNullException>(() => JaccardIndexCalculator.GetIndex(col, null));
        Assert.Throws<ArgumentNullException>(() => JaccardIndexCalculator.GetIndex<int>(null, null));
    }

    [Fact]
    public void GetIndex_EmptyCollection()
    {
        // arrange
        var col = new[] { 0 };
        var empty = Array.Empty<int>();

        // act & assert
        JaccardIndexCalculator.GetIndex(empty, col).Should().Be(0);
        JaccardIndexCalculator.GetIndex(col, empty).Should().Be(0);
        JaccardIndexCalculator.GetIndex(empty, empty).Should().Be(1);
    }
    
    [Fact]
    public void GetIndex_SameCollection()
    {
        // arrange
        var col = new[] { 0, 1, 2 };

        // act & assert
        JaccardIndexCalculator.GetIndex(col, col).Should().Be(1);
    }

    [Fact]
    public void GetIndex_ReverseCollection()
    {
        // arrange
        var col1 = new[] { 0, 1, 2 };
        var col2 = new[] { 2, 1, 0 };

        // act & assert
        JaccardIndexCalculator.GetIndex(col1, col2).Should().Be(1);
    }
    
    [Fact]
    public void GetIndex_IdenticalCollection()
    {
        // arrange
        var col1 = new[] { 0, 1, 2 };
        var col2 = new[] { 0, 2, 1 };

        // act & assert
        JaccardIndexCalculator.GetIndex(col1, col2).Should().Be(1);
    }

    [Fact]
    public void GetIndex_DifferentCollection()
    {
        // arrange
        var col1 = new[] { 0 };
        var col2 = new[] { 1 };

        // act & assert
        JaccardIndexCalculator.GetIndex(col1, col2).Should().Be(0);
    }
    
    [Fact]
    public void GetIndex_Example1()
    {
        // arrange
        var col1 = new[] { 0,1,2,5,6 };
        var col2 = new[] { 0,2,3,4,5,7,9 };

        // act & assert
        JaccardIndexCalculator.GetIndex(col1, col2).Should().Be(0.33);
    }
}
