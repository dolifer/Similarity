# Jaccard
Utility that calculates Jaccard Index/Similarity between two collections. 

https://www.learndatasci.com/glossary/jaccard-similarity

The Jaccard similarity measures the similarity between two sets of data to see which members are shared and distinct. 
The Jaccard similarity is calculated by dividing the number of observations in both sets by the number of observations in either set. 
In other words, the Jaccard similarity can be computed as the size of the intersection divided by the size of the union of two sets.

See usage below, or check tests.

```c#
var left = new[] { 0,1,2,5,6 };
var right = new[] { 0,2,3,4,5,7,9 };


var index = JaccardIndexCalculator.GetIndex(left, right); // 0.33
```
