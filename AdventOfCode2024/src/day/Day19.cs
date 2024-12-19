using System.Diagnostics;

using AdventOfCode2024;


public class Day19 : BaseDay
{
    List<string> TowelPatterns = new List<string>();

    List<string> TowelDesigns = new List<string>();

    public Day19(string[]? inputLines = null) : base(inputLines)
    {
        bool isDesignLoad = false;
        foreach (string line in InputLines)
        {
            if (line == "")
            {
                isDesignLoad = true;
                continue;
            }
            if (isDesignLoad)
            {
                TowelDesigns.Add(line);
            }
            else
            {
                string[] patterns = line.Split(",").Select(x => x.Trim()).ToArray();
                TowelPatterns.AddRange(patterns);
            }
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            int i = 0;

            foreach (string design in TowelDesigns)
            {
                Console.WriteLine($"{i++} / {TowelDesigns.Count}");
                bool isDesignAble = CheckDesign(design);
                if (isDesignAble)
                {
                    part1 += 1;
                }
            }

        });

        return part1;
    }

    private bool CheckDesign(string design)
    {
        var queue = new Queue<int>();
        var visited = new HashSet<int>();

        queue.Enqueue(0);
        visited.Add(0);

        while (queue.Count > 0)
        {
            int startIndex = queue.Dequeue();

            foreach (var pattern in TowelPatterns)
            {
                if (design.AsSpan(startIndex).StartsWith(pattern))
                {
                    int nextIndex = startIndex + pattern.Length;

                    if (nextIndex == design.Length)
                    {
                        return true;
                    }

                    if (!visited.Contains(nextIndex))
                    {
                        queue.Enqueue(nextIndex);
                        visited.Add(nextIndex);
                    }
                }
            }
        }

        return false;
    }

    private long CountValidPatterns(string design)
    {
        var queue = new Queue<int>();
        var patternCounts = new Dictionary<int, long>();

        queue.Enqueue(0);
        patternCounts[0] = 1; // Start with one way to begin at index 0

        while (queue.Count > 0)
        {
            int startIndex = queue.Dequeue();

            foreach (var pattern in TowelPatterns)
            {
                if (design.AsSpan(startIndex).StartsWith(pattern))
                {
                    int nextIndex = startIndex + pattern.Length;

                    if (!patternCounts.ContainsKey(nextIndex))
                    {
                        queue.Enqueue(nextIndex);
                        patternCounts[nextIndex] = 0;
                    }

                    // Accumulate the number of ways to reach `nextIndex`
                    patternCounts[nextIndex] += patternCounts[startIndex];
                }
            }
        }

        // Return the count of ways to reach the end of the string
        return patternCounts.ContainsKey(design.Length) ? patternCounts[design.Length] : 0;
    }


    public override async ValueTask<int> Solve_2()
    {
        long part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            int i = 0;

            foreach (string design in TowelDesigns)
            {
                long cDesigns = CountValidPatterns(design);
                Console.WriteLine($"{i++} / {TowelDesigns.Count} : {cDesigns}");
                part2 += cDesigns;
            }
        });

        Console.WriteLine($"Part 2: {part2}");

        
        //25629551013286 TO LOW
        return 0;
    }
}
