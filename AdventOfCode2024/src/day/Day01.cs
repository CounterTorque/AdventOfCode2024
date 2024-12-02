using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day01 : BaseDay
{
    public Day01(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        int part1 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        
        List<int> leftNumbers = [];
        List<int> rightNumbers = [];
        foreach(string line in lines)
        {
            string[] nums = Regex.Split(line, @"\s+");
            leftNumbers.Add(int.Parse(nums[0]));
            rightNumbers.Add(int.Parse(nums[1]));
        }

        leftNumbers.Sort();
        rightNumbers.Sort();
        Debug.Assert(leftNumbers.Count == rightNumbers.Count);

        part1 = leftNumbers.Zip(rightNumbers)
            .Select(x => Math.Abs(x.Second - x.First))
            .Sum();
        
        // part1 = leftNumbers.Select((l, i) => Math.Abs(rightNumbers[i] - l)).Sum();

        // for(int i = 0; i < leftNumbers.Count; i++)
        // {
        //     part1 += Math.Abs(rightNumbers[i] - leftNumbers[i]);
        // }

        return new($"Solution 1 {part1}");
    }

    public override async ValueTask<string> Solve_2()
    {
        int part2 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);

        List<int> leftNumbers = [];
        List<int> rightNumbers = [];
        foreach(string line in lines)
        {
            string[] nums = Regex.Split(line, @"\s+");
            leftNumbers.Add(int.Parse(nums[0]));
            rightNumbers.Add(int.Parse(nums[1]));
        }
        
        for(int i = 0; i < leftNumbers.Count; i++)
        {
            int left = leftNumbers[i];
            int occurs = rightNumbers.Count(x => x == left);
            part2 += occurs * left;
        }

        return new($"Solution 2 {part2}");
    }
}
