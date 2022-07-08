using Similarity.Core.Algorithms;
using Similarity.Core.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Similarity.Tests;

public class StringSimilarityTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    public StringSimilarityTests(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("a test string", "a")]
    [InlineData("a test string", "a test")]
    [InlineData("a test string", "a test string")]
    [InlineData("a test string", "test a string")]
    [InlineData("a test string", "test string a")]
    [InlineData("a test string", "a string test")]
    [InlineData("a test string", "string a test")]
    [InlineData("a test string", "another string for a test")]
    [InlineData("black", "white")]
    public void CheckStringSimilarity(string? left, string? right)
    {
        var mh = MinHash.Create(1000, 10000);

        IEnumerable<int> GetHashes(string source) => source?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.GetHashCode()) ?? ArraySegment<int>.Empty;

        var hvs1 = mh.GetMinHash(GetHashes(left)).ToList();
        var hvs2 = mh.GetMinHash(GetHashes(right)).ToList();
        
        _testOutputHelper.WriteLine("MinHash Jaccard similarity: " + JaccardIndexCalculator.GetIndex(hvs1, hvs2));
        _testOutputHelper.WriteLine("Jaccard similarity: " + new JaccardSimilarityCalculator().Calculate(left, right));
    }
}
