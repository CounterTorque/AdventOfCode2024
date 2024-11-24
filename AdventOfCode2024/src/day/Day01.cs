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
        foreach (string line in lines)
        {  
            part1 += FindFirstAndLast(line);
        }

        return new($"Solution 1.1 {part1}");
    }


    private static int FindFirstAndLast(string line)
    {
        MatchCollection matches = Regex.Matches(line, @"\d");
        Debug.Assert(matches.Count != 0);
        return int.Parse(matches[0].Value + matches[matches.Count - 1].Value);
    }

    Dictionary<string, string> wordNumberMap = new Dictionary<string, string>
    {
        {"one", "1"},
        {"two", "2"},
        {"three", "3"},
        {"four", "4"},
        {"five", "5"},
        {"six", "6"},
        {"seven", "7"},
        {"eight", "8"},
        {"nine", "9"},
        {"zero", "0"},
        {"1", "1"},
        {"2", "2"},
        {"3", "3"},
        {"4", "4"},
        {"5", "5"},
        {"6", "6"},
        {"7", "7"},
        {"8", "8"},
        {"9", "9"},
        {"0", "0"},
    };

    public override async ValueTask<string> Solve_2()
    {
        string t_inputFilePath = InputFilePath(2);
        if (!File.Exists(t_inputFilePath))
        {
            throw new FileNotFoundException(t_inputFilePath);
        }

        int part2 = 0;
        string[] lines = await File.ReadAllLinesAsync(t_inputFilePath);
        Debug.Assert(lines.Length != 0);
        foreach (string line in lines)
        {
            int lowestIndex = int.MaxValue;
            string firstNumber = string.Empty;

            int highestIndex = int.MinValue;
            string lastNumber = string.Empty;

            foreach (var pair in wordNumberMap)
            {
                int index = line.IndexOf(pair.Key, StringComparison.OrdinalIgnoreCase);
                if (index >= 0 && index < lowestIndex)
                {
                    lowestIndex = index;
                    firstNumber = pair.Value;
                }
                index = line.LastIndexOf(pair.Key, StringComparison.OrdinalIgnoreCase);
                if (index >= 0 && index > highestIndex)
                {
                    highestIndex = index;
                    lastNumber = pair.Value;
                }
            }
                     
            part2 += int.Parse(firstNumber + lastNumber);;
        }

        return new($"Solution 1.2 {part2}");
    }
}
