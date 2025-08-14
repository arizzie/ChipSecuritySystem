using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        // Static list of chips to use for testing
        // More test cases are in TestCases.cs
        static List<ColorChip> chips = new List<ColorChip>()
        {
            new ColorChip(Color.Red, Color.Blue),
            new ColorChip(Color.Green, Color.Yellow),
            new ColorChip(Color.Blue, Color.Red),
            new ColorChip(Color.Yellow, Color.Orange),
            new ColorChip(Color.Red, Color.Purple),
            new ColorChip(Color.Purple, Color.Yellow),
            new ColorChip(Color.Purple, Color.Red),
            new ColorChip(Color.Orange, Color.Green)
        };


        static void Main(string[] args)
        {

            var path = new List<int>();

            if (args.Length > 0 && args[0].ToLower() == "test")
            {
                // Run all test cases
                TestCases.RunAllTests();
            }
            else if (args.Length > 0 && args[0].ToLower() == "boring")
            {
                // Run the memory optimized version of the algorithm
                Console.WriteLine("Running memory optimized version of the algorithm...");
                path = FindLongestBoring(chips);
                
            }
            else if (args.Length > 0 && args[0].ToLower() == "faster")
            {
                // Run the optimized version of the the mapping algorithm
                Console.WriteLine("Running optimized version of the mapping algorithm...");
                path = FindLongestPath(chips, true);

            }
            else if (args.Length > 0 && args[0].ToLower() == "slow")
            {
                // Run the unoptimized version of the mapping algorithm
                Console.WriteLine("Running unoptimized version of the mapping algorithm...");
                path = FindLongestPath(chips, false);

            }
            else
            {
                // Default behavior: find the longest path using the optimized mapping algorithm
                Console.WriteLine("Finding the longest path using the optimized mapping algorithm...");
                path = FindLongestPath(chips, true);
            }

            if (path.Count > 0)
            {
                Console.WriteLine($"Longest path found with length: {path}");
                Console.WriteLine($"  Blue → {string.Join(" → ", path.Select(i => chips[i].ToString()))} → Green");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }


        }

        /// <summary>
        /// Use DFSs to find the longest path but using a map to speed up lookups
        /// </summary>
        /// <param name="allChips">List of all available chips</param>
        /// <param name="optimized">Whether to use optimized or unoptimized</param>
        /// <param name="startColor">Start color</param>
        /// <param name="endColor">End Color</param>
        /// <returns>Longest path if it finds one</returns>
        static public List<int> FindLongestPath(List<ColorChip> allChips, bool optimized = true, Color startColor = Color.Blue, Color endColor = Color.Green)
        {
            if (allChips.Count == 0)
                return new List<int>();
            //keep track of visited notes
            HashSet<int> visited = new HashSet<int>();
            //keep track of the longest path found
            List<int> longestPath = new List<int>();
            //keep track of the current path
            Stack<int> path = new Stack<int>();
            //map of colors to list of indexes of allChips that start with that color and a list of allChips that end with that color
            Dictionary<Color, Tuple<List<int>, List<int>>> colorMap = new Dictionary<Color, Tuple<List<int>, List<int>>>();

            //create a map of colors creating a list of indexes of allChips that start with that color and a list of allChips that end with that color
            for (int i = 0; i < allChips.Count; i++)
            {
                if (colorMap.ContainsKey(allChips[i].StartColor))
                {
                    colorMap[allChips[i].StartColor].Item1.Add(i);
                }
                else
                {
                    colorMap[allChips[i].StartColor] = Tuple.Create(new List<int> { i }, new List<int>());
                }
                if (colorMap.ContainsKey(allChips[i].EndColor))
                {
                    colorMap[allChips[i].EndColor].Item2.Add(i);
                }
                else
                {
                    colorMap[allChips[i].EndColor] = Tuple.Create(new List<int>(), new List<int> { i });
                }

            }

            if (!colorMap.ContainsKey(startColor) || !colorMap.ContainsKey(endColor))
            {
                //no path found if either start or end color is not in the map
                return new List<int>();
            }

            if (colorMap[startColor].Item1.Count == 0 || colorMap[endColor].Item2.Count == 0)
            {
                // no path found if there are no allChips starting with startColor or ending with endColor
                return new List<int>();
            }

            // Try each starting chip
            for (int i = 0; i < colorMap[startColor].Item1.Count; i++)
            {
                var startChip = colorMap[startColor].Item1[i];
                visited.Clear();
                path.Clear();
                var currentResult = new List<int>();

                if (optimized)
                {
                    // Use optimized DFS
                    currentResult = DfsOptimized(allChips, startChip, visited, colorMap, endColor);
                }
                else
                {
                    // Use unoptimized DFS
                    currentResult = DfsUnoptimized(visited, colorMap, path, new Stack<int>(), allChips, startChip, endColor).ToList();
                }

                if (currentResult.Count > longestPath.Count)
                {
                    longestPath = currentResult.ToList();
                }
            }

            return longestPath;

        }

        /// <summary>My first attempt at this solution</summary>
        /// <param name="visited">HashSet to keep track of visited allChips</param>
        /// <param name="colorMap">Map for efficient lookups of next possibilities</param>
        /// <param name="path">Stack to keep track of the current path</param>
        /// <param name="result">Stack to keep track of the longest path found so far</param>
        /// <param name="allChips">List of all available allChips</param>
        /// <param name="currentChipIndex">Current chip index</param>
        /// <param name="endColor">Color that tells us that we're done</param>
        /// <returns>Longest longest path found</returns>
        public static Stack<int> DfsUnoptimized(HashSet<int> visited, Dictionary<Color, Tuple<List<int>, List<int>>> colorMap, Stack<int> path, Stack<int> result, List<ColorChip> allChips, int currentChipIndex, Color endColor)
        {
            var currentChip = allChips[currentChipIndex];

            path.Push(currentChipIndex);
            visited.Add(currentChipIndex);

            // Set the result if we found a longer path
            if (currentChip.EndColor == endColor)
            {
                if (path.Count > result.Count)
                {
                    result = new Stack<int>(path);
                }
            }
            //check if there are any chips that can be used in our map
            if (colorMap.ContainsKey(currentChip.EndColor))
            {
                // Iterate through all chips that can be used next
                for (int i = 0; i < colorMap[currentChip.EndColor].Item1.Count; i++)
                {
                    var nextChip = colorMap[currentChip.EndColor].Item1[i];

                    // If the next chip is not visited, we can continue the search
                    if (!visited.Contains(nextChip))
                    {
                        result = DfsUnoptimized(visited, colorMap, path, result, allChips, nextChip, endColor);
                    }
                }
            }

            // Backtrack
            path.Pop();
            visited.Remove(currentChipIndex);

            // return the path
            return result;
        }


        /// <summary>
        /// A little optimized version of the DFS algorithm. It removes a bit of overhead by not using a stack for the path and instead returning the path directly.
        /// </summary>
        /// <param name="allChips">List of availableChips to user our index on</param>
        /// <param name="currentChipIndex">Current chip index</param>
        /// <param name="visited">hash map to keep track of visited allChips</param>
        /// <param name="colorMap">Map for effecient look ups of next possiblity</param>
        /// <param name="endColor">Color that tells us that we're done</param>
        /// <returns>Longest path it can find</returns>
        static List<int> DfsOptimized(List<ColorChip> allChips, int currentChipIndex,
           HashSet<int> visited, Dictionary<Color, Tuple<List<int>, List<int>>> colorMap, Color endColor)
        {
            var currentChip = allChips[currentChipIndex];
            visited.Add(currentChipIndex);

            var longestPath = new List<int>();

            // Set the result if we found a longer path
            if (currentChip.EndColor == endColor)
            {
                longestPath.Add(currentChipIndex);
            }

            //check if there are any chips that can be used in our map
            if (colorMap.ContainsKey(currentChip.EndColor))
            {
                // Iterate through all chips that can be used next
                for (int i = 0; i < colorMap[currentChip.EndColor].Item1.Count; i++)
                {
                    var nextChip = colorMap[currentChip.EndColor].Item1[i];

                    // If the next chip is not visited continue
                    if (!visited.Contains(nextChip))
                    {
                        var path = DfsOptimized(allChips, nextChip, visited, colorMap, endColor);
                        visited.Remove(nextChip);

                        // Set the path if we found a longer one
                        if (path.Count + 1 > longestPath.Count)
                        {
                            longestPath = new List<int>(path.Count + 1);
                            longestPath.Add(currentChipIndex);
                            longestPath.AddRange(path);
                        }
                    }
                }
            }

            return longestPath;
        }

        /// <summary>
        /// Finds the longest path but not using any big data structures in the other solution
        /// </summary>
        /// <param name="allChips">All given chips</param>
        /// <param name="startColor">Color that starts</param>
        /// <param name="endColor">Color that ends</param>
        /// <returns>Longest Path</returns>
        public static List<int> FindLongestBoring(List<ColorChip> allChips, Color startColor = Color.Blue, Color endColor = Color.Green)
        {

            // Find all chips that start with startColor
            var startChips = new List<int>();
            for (int i = 0; i < allChips.Count; i++)
            {
                if (allChips[i].StartColor == startColor)
                {
                    startChips.Add(i);
                }
            }

            if (startChips.Count == 0)
            {
                return new List<int>();
            }

            var longestPath = new List<int>();

            // Try each starting chip to find the longest path
            foreach (var startChipIndex in startChips)
            {
                var visited = new HashSet<int> { startChipIndex };
                var path = DfsBoring(allChips, startChipIndex, visited, endColor);

                if (path.Count > longestPath.Count)
                {
                    longestPath = new List<int>(path);
                }
            }

            return longestPath;

        }

        /// <summary>Attempt at a more "boring" solution without fancy structures</summary>
        /// <param name="allChips">List of available allChips</param>
        /// <param name="currentChipIndex"> Current chip index to start from</param>
        /// <param name="visited"> HashSet to keep track of visited allChips</param>
        /// <param name="result"> List to store the current path</param>
        /// <param name="endColor"> Color that tells us that we're done</param>
        static List<int> DfsBoring(List<ColorChip> allChips, int currentChipIndex, HashSet<int> visited, Color endColor)
        {
            var currentChip = allChips[currentChipIndex];
            visited.Add(currentChipIndex);

            var longestPath = new List<int>();

            if (currentChip.EndColor == endColor)
            {
                longestPath.Add(currentChipIndex);
            }

            // Find all chips that can connect to the current chip's end color
            for (int i = 0; i < allChips.Count; i++)
            {
                if (visited.Contains(i))
                    continue;

                if (allChips[i].StartColor == currentChip.EndColor)
                {

                    var path = DfsBoring(allChips, i, visited, endColor);
                    visited.Remove(i);

                    if (path.Count + 1 > longestPath.Count)
                    {
                        longestPath = new List<int> { currentChipIndex };
                        longestPath.AddRange(path);
                    }
                }
            }

            return longestPath;

        }
    }
}
