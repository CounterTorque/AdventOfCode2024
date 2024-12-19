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

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

        });

        return part2;
    }
}
