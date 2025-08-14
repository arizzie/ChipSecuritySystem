# Chip Security System 

I wanted to showcase a bit of my work and then some optimization (even if very little) and then end it with a typical DFS solution without any fancy data structures, so I crafted 3 algorithms.

Below is an explanation of how to run the program and command line arguments to run the different algorithms. 

Thank you for your consideration, I really had a lot fun with this excercise.

## Command Line Arguments

The application supports several command line arguments:

- `test` - Runs all test cases (fair warning there's a really long one at the end). This compares all algorithms and shows timings and results.
- `boring` - Runs the memory optimized version (DFS with lists and HashSet)
- `faster` - Runs the optimized mapping algorithm
- `slow` - Runs the unoptimized mapping algorithm
- No arguments - Default behavior using the optimized mapping algorithm

## Solution Configurations

Visual Studio Professional solution configurations have been created for each algorithm:

- **Test** - Runs all test cases
- **Boring** - Memory optimized DFS solution
- **Optimized** - Optimized mapping algorithm
- **Original** - Unoptimized mapping algorithm

## Three Solution Approaches

### 1. Original Solution (Mapping with Tuples)
Uses a Dictionary to map colors to tuples containing lists of chip indices that start and end with each color. This provides efficient O(1) lookups for finding the next possible chips in the path.

**How it works:**
- Creates a color map: `Dictionary<Color, Tuple<List<int>, List<int>>>`
- First list contains chips that start with that color
- Second list contains chips that end with that color
- Uses DFS to explore all possible paths and find the longest one

### 2. Optimized Solution (Reduced Data Structures)
A more efficient version of the mapping approach that reduces overhead by eliminating the Stack for path tracking and returning paths directly as Lists.

**Key optimizations:**
- Removes Stack overhead for path tracking
- Direct List return instead of Stack conversion
- Pre-allocated List capacity for better performance

### 3. Memory Optimized Solution (Simple DFS)
A straightforward DFS implementation using only basic data structures:

**Data structures used:**
- `List<int>` for paths
- `HashSet<int>` for visited tracking
- No complex mapping structures

This solution prioritizes memory efficiency over lookup speed by using simple iteration through all chips to find connections.

## Usage

Build the project and run with your desired argument:

```
.\bin\Debug\ChipSecuritySystem.exe test
.\bin\Debug\ChipSecuritySystem.exe boring
.\bin\Debug\ChipSecuritySystem.exe faster
.\bin\Debug\ChipSecuritySystem.exe slow
```

All three solutions produce identical results but with different performance characteristics depending on the input size and complexity.
