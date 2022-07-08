using Similarity.Core.Helpers;

namespace Similarity.Core.Algorithms;

/// <summary>
/// Calculates Jaccard Similarity between two collections. 
/// </summary>
/// More details at <a href="https://en.wikipedia.org/wiki/Jaccard_index">Wikipedia article about Jaccard index</a>
public sealed class JaccardSimilarityCalculator : ISimilarityCalculator
{
    public double Calculate(string first, string second)
    {
        var left = first.ExtractShingles();
        var right = second.ExtractShingles();

        return JaccardIndexCalculator.GetIndex(left, right);
    }
}

/// <summary>
/// Calculates Jaccard Index/Similarity between two collections. 
/// </summary>
public static class JaccardIndexCalculator
{
    /// <summary>
    /// Inverse Jaccard Index
    /// </summary>
    /// <param name="left">First collection.</param>
    /// <param name="right">Second collection.</param>
    /// <returns>Value between 0 and 1, where 0 means given collections are identical and 1 that they are different.</returns>
    public static double GetDistance<T>(ICollection<T>? left, ICollection<T>? right) 
        => Math.Round(1 - GetIndex(left, right), 2);

    /// <summary>
    /// Jaccard Index
    /// </summary>
    /// <param name="left">First collection.</param>
    /// <param name="right">Second collection.</param>
    /// <returns>Value between 0 and 1, where 0 means given collections are different, and 1 that they are identical.</returns>
    public static double GetIndex<T>(ICollection<T>? left, ICollection<T>? right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        if ((left.Count > 0 && right.Count == 0) || (right.Count > 0 && left.Count == 0))
        {
            return 0;
        }

        if (left.Count == 0 && right.Count == 0)
        {
            return 1;
        }

        var intersection = left.Intersect(right).Count();
        var union = left.Union(right).Count();

        var index = (intersection / (double) union);
        return Math.Round(index, 2);
    }
}
