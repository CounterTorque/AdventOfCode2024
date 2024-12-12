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

        await Task.Run(() =>
        {

            while (true)
            {
                List<long> nextStones = new List<long>();
                for (int i = 0; i < stones.Count; i++)
                {
                    long stone = stones[i];
                    if (stone == 0)
                    {
                        nextStones.Add(1);
                        continue;
                    }
                    string numStr = stone.ToString();
                    int digits = numStr.Length;
                    bool isEven = digits % 2 == 0;
                    if (isEven)
                    {
                        string lefthalf = numStr.Substring(0, digits / 2);
                        string righthalf = numStr.Substring(digits / 2, digits / 2);
                        long left = long.Parse(lefthalf);
                        long right = long.Parse(righthalf);
                        nextStones.Add(left);
                        nextStones.Add(right);
                        continue;
                    }

                    long newNum = stone * 2024;
                    nextStones.Add(newNum);
                }
                stones = nextStones;
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
        return await Task.Run(() =>
        {
            var stoneFrequency = new Dictionary<long, long>(); // {stone value, frequency}

            // Initialize with initial stones
            foreach (long stone in stones)
            {
                if (!stoneFrequency.ContainsKey(stone))
                    stoneFrequency[stone] = 0;
                stoneFrequency[stone]++;
            }

            for (int iterations = 0; iterations < 75; iterations++)
            {
                var nextFrequency = new Dictionary<long, long>();

                foreach (var kvp in stoneFrequency)
                {
                    long stone = kvp.Key;
                    long count = kvp.Value;

                    if (stone == 0)
                    {
                        if (!nextFrequency.ContainsKey(1))
                            nextFrequency[1] = 0;
                        nextFrequency[1] += count;
                    }
                    else
                    {
                        int digits = (int)Math.Log10(stone) + 1;
                        if (digits % 2 == 0)
                        {
                            int half = digits / 2;
                            long divisor = (long)Math.Pow(10, half);
                            long left = stone / divisor;
                            long right = stone % divisor;

                            if (!nextFrequency.ContainsKey(left))
                                nextFrequency[left] = 0;
                            nextFrequency[left] += count;

                            if (!nextFrequency.ContainsKey(right))
                                nextFrequency[right] = 0;
                            nextFrequency[right] += count;
                        }
                        else
                        {
                            long newStone = stone * 2024;

                            if (!nextFrequency.ContainsKey(newStone))
                                nextFrequency[newStone] = 0;
                            nextFrequency[newStone] += count;
                        }
                    }
                }

                stoneFrequency = nextFrequency; // Update for the next iteration
                Console.WriteLine($"Iteration {iterations + 1}: {stoneFrequency.Values.Sum()} stones");
            }

            return 0;
        });
    }
}
