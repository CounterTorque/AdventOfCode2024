using System.Diagnostics;

using AdventOfCode2024;


public class Day07 : BaseDay
{

    struct TestCalibration
    {
        public long Target { get; set; }
        public long[] Values { get; set; }

    }

    List<TestCalibration> testcalibrations = new List<TestCalibration>();

    bool useCat = false;

    public Day07(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            TestCalibration testCalibration = new TestCalibration();

            string[] targetValues = line.Split(":");

            testCalibration.Target = long.Parse(targetValues[0]);
            testCalibration.Values = targetValues[1].Trim().Split(" ").Select(long.Parse).ToArray();

            testcalibrations.Add(testCalibration);
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        long part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

            foreach (TestCalibration testCalibration in testcalibrations)
            {
                bool solvable = SolveStep(testCalibration.Target, 0, testCalibration.Values);

                if (solvable)
                {
                    part1 += testCalibration.Target;
                }
            }
        });

        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }


    private bool SolveStep(long target, long curSum, long[] values)
    {
        if (values.Length == 0)
        {
            return curSum == target;
        }

        bool plus = SolveStep(target, curSum + values[0], values.Skip(1).ToArray());
        bool mult = SolveStep(target, curSum * values[0], values.Skip(1).ToArray());

        bool cat = false;
        if (useCat && values.Length >= 1)
        {
            long catNum = long.Parse(curSum.ToString() + values[0].ToString());
            cat = SolveStep(target, catNum, values.Skip(1).ToArray());
        }

        return plus || mult || cat;
    }

    public override async ValueTask<int> Solve_2()
    {
        long part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            useCat = true;
            foreach (TestCalibration testCalibration in testcalibrations)
            {
                bool solvable = SolveStep(testCalibration.Target, 0, testCalibration.Values);

                if (solvable)
                {
                    part2 += testCalibration.Target;
                }
            }
        });

        Console.WriteLine($"Part 2: {part2}");
        return 0;
    }
}
