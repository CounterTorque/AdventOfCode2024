using System.Diagnostics;

using AdventOfCode2024;


public class Day07 : BaseDay
{

    struct TestCalibration
    {
        public int Target { get; set; }
        public int[] Values { get; set; }

    }

    List<TestCalibration> testcalibrations = new List<TestCalibration>();
    
    public Day07(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            TestCalibration testCalibration = new TestCalibration();

            string[] targetValues = line.Split(":");

            testCalibration.Target = int.Parse(targetValues[0]);
            testCalibration.Values = targetValues[1].Trim().Split(" ").Select(int.Parse).ToArray();

            testcalibrations.Add(testCalibration);
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            foreach (TestCalibration testCalibration in testcalibrations)
            {
                bool solvable = TrySolve(testCalibration);

                if (solvable)
                {
                    part1 += testCalibration.Target;
                }
            }
        });
        
        return part1;
    }

    private bool TrySolve(TestCalibration testCalibration)
    {
        //Walking each value
        int curValue = 0;

        return false;
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
