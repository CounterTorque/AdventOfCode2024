using System.Diagnostics;

using AdventOfCode2024;


public class Day22 : BaseDay
{

    List<int> InitialSecretNumbers = new List<int>();
    List<int> FinalSecretNumbers = new List<int>();

    public Day22(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            int number = int.Parse(line);
            Debug.Assert(number > 0);
            InitialSecretNumbers.Add(number);
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        long part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {
            foreach(int num in InitialSecretNumbers)
            {
                int finalSecret = CalculateSecretTo(num, 2000);
                FinalSecretNumbers.Add(finalSecret);
                Console.WriteLine($"{num}: {finalSecret}");
                part1 += finalSecret;
            }
        });
        
        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }

    private int CalculateSecretTo(int curSecret, int generations)
    {
        for (int i = 0; i < generations; i++)
        {
            //Step 1
            //Mul curSecretby 64
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            int valMul = curSecret << 6;
            int valMix = valMul ^ curSecret;
            curSecret = valMix & 16777215;

            //Step 2
            //curSecret dikide by 32
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            int valDiv = curSecret >> 5;
            valMix = valDiv ^ curSecret;
            curSecret = valMix & 16777215;

            //Step 3
            //Mul curSecretby 2048
            //XOR Val by curSecret - Set to curSecret
            //Prune (bitwise & with 16777216-1)
            valMul = curSecret << 11;
            valMix = valMul ^ curSecret;
            curSecret = valMix & 16777215;
        }
        
        return curSecret;
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
