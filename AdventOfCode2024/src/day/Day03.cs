using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day03 : BaseDay
{

    public Day03(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        int part1 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        string pattern = "mul\\([0-9]+,[0-9]+\\)";
        foreach (string line in lines)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            string[] results = new string[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                results[i] = matches[i].Value;
                string[] nums = Regex.Matches(results[i], @"[0-9]+").Select(x => x.Value).ToArray();
                Debug.Assert(nums.Length == 2);
                part1 += int.Parse(nums[0]) * int.Parse(nums[1]);
            }
        }       
       
        return new($"Solution 1 {part1}");
    }


    public override async ValueTask<string> Solve_2()
    {
        int part2 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);

       
        return new($"Solution 2 {part2}");
    }    
}
