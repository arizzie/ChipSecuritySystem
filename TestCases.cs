using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ChipSecuritySystem
{
    public class TestCases
    {
        public static void RunAllTests()
        {
            Console.WriteLine("Chip Security System Tests");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            CustomProblem();
            ReadmeExample();
            NoPath();
            SingleChip();
            SameStartColors();
            MultiPathSameLength();
            CircularPath();
            DisconnectedChips();
            LongChain();
            EmptyChipSet();
            SameStartColorsNoOutlet();
            PerformanceTest();

        }

        private static void CustomProblem()
        {
            Console.WriteLine("Test Case 1: Original Problem");
            Console.WriteLine("Expected: 5-chip solution");

            var chips = new List<ColorChip>()
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

            RunTest(chips, "Original Problem");
        }

        private static void ReadmeExample()
        {
            Console.WriteLine("Test Case 2: README Example");
            Console.WriteLine("Expected: 3-chip solution");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            RunTest(chips, "README Example");
        }

        private static void NoPath()
        {
            Console.WriteLine("Test Case 3: No Path Exists");
            Console.WriteLine("Expected: No solution found");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Red, Color.Blue),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Purple, Color.Orange)
            };

            RunTest(chips, "No Path Exists");
        }

        private static void SingleChip()
        {
            Console.WriteLine("Test Case 4: Single Chip Solution");
            Console.WriteLine("Expected: 1-chip solution");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Green),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Purple, Color.Orange)
            };

            RunTest(chips, "Single Chip Solution");
        }

        private static void SameStartColors()
        {
            Console.WriteLine("Test Case 5: Chip with Same Start/End Colors");
            Console.WriteLine("Expected: Find a solution");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Blue),
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green),
                new ColorChip(Color.Green, Color.Green)  
            };

            RunTest(chips, "Same Start/End Colors");
        }


        private static void MultiPathSameLength()
        {
            Console.WriteLine("Test Case 6: Multiple Paths of Same Length");
            Console.WriteLine("Expected: One of the 3-chip solutions");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Blue, Color.Yellow), 
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Green), 
                new ColorChip(Color.Red, Color.Purple),
                new ColorChip(Color.Purple, Color.Green)  
            };

            RunTest(chips, "Multiple Paths Same Length");
        }

        private static void CircularPath()
        {
            Console.WriteLine("Test Case 7: Circular Path");
            Console.WriteLine("Expected: Should find valid path without infinite loop");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red), 
                new ColorChip(Color.Yellow, Color.Green)
            };

            RunTest(chips, "Circular Path");
        }

        private static void DisconnectedChips()
        {
            Console.WriteLine("Test Case 8: Disconnected Chips");
            Console.WriteLine("Expected: Should find path ignoring disconnected chips");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),  
                new ColorChip(Color.Orange, Color.Yellow)   
            };

            RunTest(chips, "Disconnected Chips");
        }

        private static void LongChain()
        {
            Console.WriteLine("Test Case 9: Long Chain");
            Console.WriteLine("Expected: 6-chip solution");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Red),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Green),
                new ColorChip(Color.Blue, Color.Green), 
                new ColorChip(Color.Red, Color.Green) 
            };

            RunTest(chips, "Long Chain");
        }

        private static void EmptyChipSet()
        {
            Console.WriteLine("Test Case 10: Empty Chip Set");
            Console.WriteLine("Expected: No solution found");

            var chips = new List<ColorChip>();

            RunTest(chips, "Empty Chip Set");
        }

        private static void SameStartColorsNoOutlet()
        {
            Console.WriteLine("Test Case 11: Chip with Same Start Colors But No Outlet");
            Console.WriteLine("Expected: No Solution Found");

            var chips = new List<ColorChip>()
            {
                new ColorChip(Color.Blue, Color.Blue),
                new ColorChip(Color.Red, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green),
                new ColorChip(Color.Green, Color.Green)
            };

            RunTest(chips, "Same Same Start Colors No Outlet");
        }

        public static void PerformanceTest()
        {
            Console.WriteLine("Test Case 12: Long 22-Chip Set (Performance Test)");
            Console.WriteLine("Expected: Long path that takes time to compute");
            Console.WriteLine("This test is designed to demonstrate performance differences between solutions");

            var chips = new List<ColorChip>()
             {
                 new ColorChip(Color.Blue, Color.Red),
                 new ColorChip(Color.Red, Color.Yellow),
                 new ColorChip(Color.Yellow, Color.Purple),
                 new ColorChip(Color.Purple, Color.Orange),
                 new ColorChip(Color.Orange, Color.Blue),
                 new ColorChip(Color.Blue, Color.Yellow),
                 new ColorChip(Color.Yellow, Color.Green),
                 new ColorChip(Color.Green, Color.Red),
                 new ColorChip(Color.Red, Color.Purple),
                 new ColorChip(Color.Purple, Color.Blue),
                 new ColorChip(Color.Blue, Color.Orange),
                 new ColorChip(Color.Orange, Color.Yellow),
                 new ColorChip(Color.Yellow, Color.Red),
                 new ColorChip(Color.Red, Color.Blue),
                 new ColorChip(Color.Blue, Color.Purple),
                 new ColorChip(Color.Purple, Color.Yellow),
                 new ColorChip(Color.Yellow, Color.Orange),
                 new ColorChip(Color.Orange, Color.Green),
                 new ColorChip(Color.Green, Color.Blue),
                 new ColorChip(Color.Blue, Color.Red),
                 new ColorChip(Color.Red, Color.Yellow),
                 new ColorChip(Color.Yellow, Color.Purple)
             };

            RunTest(chips, "Long Test Don't Care");
        }



        private static void RunTest(List<ColorChip> chips, string testName, Color startColor = Color.Blue, Color endColor = Color.Green)
        {
            Console.WriteLine($"Running: {testName}");
            Console.WriteLine($"Chips: {chips.Count}");

            if (chips.Count > 0)
            {
                for (int i = 0; i < chips.Count; i++)
                {
                    Console.WriteLine($"  [{i}] {chips[i]}");
                }
            }

            // Test all solutions with timing
            var stopwatch = new Stopwatch();

            // Original solution
            stopwatch.Restart();
            var result1 = Program.FindLongestPath(chips, false, startColor, endColor);
            stopwatch.Stop();
            var time1 = stopwatch.Elapsed;

            // Optimized solution
            stopwatch.Restart();
            var result2 = Program.FindLongestPath(chips, true, startColor, endColor);
            stopwatch.Stop();
            var time2 = stopwatch.Elapsed;

            // Boring Solution
            stopwatch.Restart();
            var result3 = Program.FindLongestBoring(chips, startColor, endColor);
            stopwatch.Stop();
            var time3 = stopwatch.Elapsed;



            Console.WriteLine("Original Solution:");
            if (result1.Count > 0)
            {
                Console.WriteLine($"✓ Solution found: {result1.Count} chips (Time: {time1.TotalMilliseconds:F2} ms)");
                Console.WriteLine($"  Blue → {string.Join(" → ", result1.Select(i => chips[i].ToString()))} → Green");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage + $"(Time: {time1.TotalMilliseconds:F2} ms)");
            }

            Console.WriteLine("Optimized Solution:");
            if (result2.Count > 0)
            {
                Console.WriteLine($"✓ Solution found: {result2.Count} chips (Time: {time2.TotalMilliseconds:F2} ms)");
                Console.WriteLine($"  Blue → {string.Join(" → ", result2.Select(i => chips[i].ToString()))} → Green");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage + $"(Time: {time2.TotalMilliseconds:F2} ms)");
            }

            Console.WriteLine("Boring Solution:");
            if (result3.Count > 0)
            {
                Console.WriteLine($"✓ Solution found: {result3.Count} chips (Time: {time3.TotalMilliseconds:F2} ms)");
                Console.WriteLine($"  Blue → {string.Join(" → ", result3.Select(i => chips[i].ToString()))} → Green");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage + $"(Time: {time3.TotalMilliseconds:F2} ms)");
            }


            // Verify all solutions give the same result
            if (result1.Count == result2.Count && result2.Count == result3.Count)
            {
                Console.WriteLine("✓ All solutions agree");
            }
            else
            {
                Console.WriteLine("X Solutions differ!");
            }

            Console.WriteLine();
        }
    }
}
