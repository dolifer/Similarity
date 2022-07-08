# Similarity

A sample project to test different techniques to calculate the similarity between two sets.

Based on “Mining of Massive Datasets” by A.Rajaraman, J. Leskovec, J.D. Ullman, see http://www.mmds.org/

## Jaccard
Utility that calculates Jaccard Index/Similarity between two collections. 

The Jaccard similarity of sets `S` and `T` is `|S ∩ T |/|S ∪ T |`, that is, the ratio
of the size of the intersection of `S` and `T` to the size of their union.

See usage below, or check tests.

```c#
var left = new[] { 0,1,2,5,6 };
var right = new[] { 0,2,3,4,5,7,9 };

var index = JaccardIndexCalculator.GetIndex(left, right); // 0.33
```

# Helpers

## MinHash

A helper class that implements MinHash algorithm for "documents", result can be used to calculate similarity.

## Shingles

Simple helper that extracts and return an array of strings that represents the source document as shingle-set.