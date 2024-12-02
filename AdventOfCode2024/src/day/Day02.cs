using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day02 : BaseDay
{
 
    public Day02()
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
            string[] nums = Regex.Split(line, @"\s+");
            Debug.Assert(nums.Length >= 2);
            bool isIncreasing = int.Parse(nums[0]) < int.Parse(nums[1]);
            bool isValid = true;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                int left = int.Parse(nums[i]);
                int right = int.Parse(nums[i + 1]);
                int delta = Math.Abs(right - left);
                
                if (delta == 0 || delta > 3)
                {
                    isValid = false;
                    break;
                }

                if (isIncreasing)
                {
                    if (left >= right)
                    {
                        isValid = false;
                        break;
                    }
                }   
                else
                {
                    if (right >= left)
                    {
                        isValid = false;
                        break;
                    }
                }

            }

            if (isValid)
            {
                part1 += 1;
            }
        }
        
        //358 too high
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
