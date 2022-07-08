namespace Similarity.Core.Helpers;

public static class Shingles
{
    private const int DefaultNGramSize = 1;

    public static string[] ExtractShingles(this string source, char separator = ' ', int ngrams = DefaultNGramSize)
    {
        if (string.IsNullOrWhiteSpace(source))
            return Array.Empty<string>();

        var words = source.Split(separator).ToList();

        if (words.Count < ngrams)
            throw new ArgumentException(null, nameof(source));

        if (words.Count == ngrams)
            return new[] { source };

        var shingles = new List<string>();

        for(var i = 0; i <= words.Count - ngrams;  i++)
        {
            var shingle = string.Join(' ', words.Skip(i).Take(ngrams));
            shingles.Add(shingle);
        }

        return shingles.Distinct().ToArray();
    }
}
