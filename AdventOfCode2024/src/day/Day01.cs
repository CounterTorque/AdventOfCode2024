using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day01 : BaseDay
{
 
    public Day01()
    {

    }

    public override async ValueTask<string> Solve_1()
    {
        string inputFilePath = InputFilePath(1);
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException(inputFilePath);
        }

        int part1 = 0;
        string[] lines = await File.ReadAllLinesAsync(inputFilePath);
        Debug.Assert(lines.Length != 0);
        
        List<int> leftNumbers = new();
        List<int> rightNumbers = new();
        foreach(string line in lines)
        {
            string[] nums = Regex.Split(line, @"\s+");
            leftNumbers.Add(int.Parse(nums[0]));
            rightNumbers.Add(int.Parse(nums[1]));
        }

        leftNumbers.Sort();
        rightNumbers.Sort();
        Debug.Assert(leftNumbers.Count == rightNumbers.Count);

        for(int i = 0; i < leftNumbers.Count; i++)
        {
            part1 += Math.Abs(rightNumbers[i] - leftNumbers[i]);
        }

        return new($"Solution 1 {part1}");
    }

    public override async ValueTask<string> Solve_2()
    {
        string inputFilePath = InputFilePath(2);
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException(inputFilePath);
        }

        int part2 = 0;
        string[] lines = await File.ReadAllLinesAsync(inputFilePath);
        Debug.Assert(lines.Length != 0);

        List<int> leftNumbers = new();
        List<int> rightNumbers = new();
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
