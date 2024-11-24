using System.Diagnostics;

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
        
        return new($"Solution 2 {part2}");
    }
}
