namespace Jaccard;

/// <summary>
/// Calculates Jaccard Index/Similarity between two collections. 
/// </summary>
/// https://www.learndatasci.com/glossary/jaccard-similarity
public static class JaccardIndexCalculator
{
    /// <summary>
    /// Inverse Jaccard Index
    /// </summary>
    /// <param name="left">First collection.</param>
    /// <param name="right">Second collection.</param>
    /// <returns>Value between 0 and 1, where 0 means given collections are identical and 1 that they are different.</returns>
    public static double GetDistance<T>(ICollection<T>? left, ICollection<T>? right) 
        => 1 - GetIndex(left, right);

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