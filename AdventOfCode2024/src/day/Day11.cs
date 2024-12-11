using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day11 : BaseDay
{
    List<long> stones = new List<long>();

    public Day11(string[]? inputLines = null) : base(inputLines)
    {
        Debug.Assert(InputLines.Length == 1);
        string[] nums = Regex.Split(InputLines[0], @"\s+");
        foreach (string num in nums)
        {
            stones.Add(int.Parse(num));
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int iterations = 0;
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            while(true)
            {
                for (int i = 0; i < stones.Count; i++)
                {
                    if (stones[i] == 0)
                    {
                        stones[i] = 1;
                        continue;
                    }
                    string numStr = stones[i].ToString();
                    int digits = numStr.Length;
                    bool isEven = digits % 2 == 0;
                    if (isEven)
                    {
                        string lefthalf = numStr.Substring(0, digits / 2);
                        string righthalf = numStr.Substring(digits / 2, digits / 2);
                        long left = long.Parse(lefthalf);
                        long right = long.Parse(righthalf);
                        stones[i] = left;
                        stones.Insert(i + 1, right);
                        i++;
                        continue;
                    }

                    long newNum = stones[i] * 2024;
                    stones[i] = newNum;
                }
                iterations++;
                if (iterations == 25)
                {
                    break;
                }
            }

            part1 = stones.Count;
            

        });
        
        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

        });

        return part2;
    }
}
