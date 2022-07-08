namespace Similarity.Core;

/// <summary>
/// Calculates similarity between two given strings.
/// </summary>
public interface ISimilarityCalculator
{
    /// <summary>
    /// Calculates similarity between two given strings.
    /// </summary>
    /// <param name="first">The first string.</param>
    /// <param name="second">The second string.</param>
    /// <returns></returns>
    double Calculate(string first, string second);
}
