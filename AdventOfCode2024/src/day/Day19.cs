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

            foreach (string design in TowelDesigns)
            {
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
        var designQueue = new Queue<string>();
        designQueue.Enqueue(design);

        while (designQueue.Count > 0)
        {
            var currentPattern = designQueue.Dequeue();

            foreach (var pattern in TowelPatterns)
            {
                if (currentPattern.StartsWith(pattern))
                {
                    var subPattern = currentPattern.Substring(pattern.Length);

                    if (string.IsNullOrEmpty(subPattern))
                    {
                        return true;
                    }

                    designQueue.Enqueue(subPattern);
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
