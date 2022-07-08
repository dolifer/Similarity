namespace Similarity.Core.Helpers;

public sealed class MinHash
{
    // will get the same hash functions each time since the same random number seed is used
    private readonly Random _random = new(42);
    private delegate uint Hash(int toHash);
    private readonly Hash[] _hashFunctions;

    private MinHash(int universeSize, int numHashFunctions)
    {
        _hashFunctions = new Hash[numHashFunctions];
        GenerateHashFunctions(universeSize);
    }

    /// <summary>
    /// Creates a MinHash helper with a given <paramref name="universeSize"/> and <paramref name="numHashFunctions"/>
    /// </summary>
    /// <param name="universeSize">The number of distinct terms expected across all sets</param>
    /// <param name="numHashFunctions"></param>
    /// <returns></returns>
    public static MinHash Create(int universeSize, int numHashFunctions) => new(universeSize, numHashFunctions);

    /// <summary>
    /// Generates the Universal Random Hash functions
    /// <para>
    /// http://en.wikipedia.org/wiki/Universal_hashing
    /// </para>
    /// </summary>
    /// <param name="universeSize"></param>
    private void GenerateHashFunctions(int universeSize)
    {
        var u = BitsForUniverse(universeSize);
        
        for (var i = 0; i < _hashFunctions.Length; i++)
        {
            uint a = 0;
            // parameter a is an odd positive
            while (a % 1 == 1 || a <= 0)
                a = (uint)_random.Next();
            uint b = 0;
            var maxb = 1 << u;
            // parameter b must be greater than zero and less than universe size
            while (b <= 0)
                b = (uint)_random.Next(maxb);
            _hashFunctions[i] = x => QHash(x, a, b, u);
        }
    }
 
    // Returns the number of bits needed to store the universe
    private static int BitsForUniverse(int universeSize) => (int)Math.Truncate(Math.Log(universeSize, 2.0)) + 1;

    // Universal hash function with two parameters a and b, and universe size in bits
    private static uint QHash(int x, uint a, uint b, int u) => (a * (uint)x + b) >> (32 - u);

    /// <summary>
    /// Returns the list of min hashes for the given <paramref name="source"/>
    /// </summary>
    /// <param name="source">Source set to get MinHashes for.</param>
    /// <returns>Collection of MinHash fingerprints.</returns>
    public IEnumerable<uint> GetMinHash(IEnumerable<int> source)
    {
        var minHashes = new uint[_hashFunctions.Length];
        for (var h = 0; h < _hashFunctions.Length; h++)
        {
            minHashes[h] = int.MaxValue;
        }
        foreach (var value in source)
        {
            for (var h = 0; h < _hashFunctions.Length; h++)
            {
                var hash = _hashFunctions[h](value);
                minHashes[h] = Math.Min(minHashes[h], hash);
            }
        }
        return minHashes.ToList();
    }
}
