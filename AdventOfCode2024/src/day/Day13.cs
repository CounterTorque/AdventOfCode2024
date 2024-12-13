using System.Diagnostics;

using AdventOfCode2024;


public class Day13 : BaseDay
{
    class ClawProblem
    {
        public (long, long) AButton = (0, 0);
        public (long, long) BButton = (0, 0);
        public (long, long) Prize = (0, 0);

        public List<(long, long)> Solutions = new();
        public long BestSolution = long.MaxValue;
    }

    List<ClawProblem> ClawProblems = new();

    public Day13(string[]? inputLines = null) : base(inputLines)
    {
        ClawProblem problem = new ClawProblem();
        foreach (string line in InputLines)
        {
            if (line == "")
            {
                ClawProblems.Add(problem);
                problem = new ClawProblem();
                continue;
            }

            if (line.StartsWith("Button A:"))
            {
                problem.AButton = ParseCoordinates(line);
            }
            else if (line.StartsWith("Button B:"))
            {
                problem.BButton = ParseCoordinates(line);
            }
            else if (line.StartsWith("Prize:"))
            {
                problem.Prize = ParseCoordinates(line);
            }
        }
        ClawProblems.Add(problem);
    }

    static (int, int) ParseCoordinates(string line)
    {
        string[] parts = line.Split(new[] { ": " }, StringSplitOptions.None);
        string coordinates = parts[1];

        string[] coords = coordinates.Split(new[] { ", " }, StringSplitOptions.None);

        int x = int.Parse(coords[0].Substring(2));
        int y = int.Parse(coords[1].Substring(2));

        return (x, y);
    }

    
    public List<(long, long)> FindCombinations(long Pv, long Av, long Bv)
    {
        //Diophantine equation approach
        List<(long, long)> solutions = new List<(long, long)>();

        // Try possible values of nB
        for (long nB = 0; nB <= Pv / Bv; nB++)
        {
            // Calculate remaining amount after nB contribution
            long remaining = Pv - (nB * Bv);

            // Check if remaining is divisible by A value
            if (remaining % Av == 0)
            {
                long nA = remaining / Av;
                solutions.Add((nA, nB));
            }
        }

        return solutions;
    }

    public override async ValueTask<int> Solve_1()
    {
        long part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            foreach (ClawProblem problem in ClawProblems)
            {
                part1 += SolveClawProblem(problem);
            }

        });

        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }

    private long SolveClawProblem(ClawProblem problem, long pMod = 0)
    {
        long BestSolution = 0;
        List<(long, long)> xSolutions = FindCombinations(pMod+problem.Prize.Item1, problem.AButton.Item1, problem.BButton.Item1);
        List<(long, long)> ySolutions = FindCombinations(pMod+problem.Prize.Item2, problem.AButton.Item2, problem.BButton.Item2);
        //First check that there even are solutions for each button coordinate
        if (xSolutions.Count == 0 || ySolutions.Count == 0)
        {
            return 0;
        }
        
        //Now We need to find the intersection of the two sets of solutions
        List<(long, long)> solutions = new List<(long, long)>();
        foreach ((long nA, long nB) in xSolutions)
        {
            if (ySolutions.Contains((nA, nB)))
            {
                solutions.Add((nA, nB));
                long solutionCost = (nA * 3) + nB;
                if (solutionCost < problem.BestSolution)
                {
                    problem.BestSolution = solutionCost;
                }
            }
        }
        if (solutions.Count > 0)
        {
            problem.Solutions.AddRange(solutions);
            BestSolution = problem.BestSolution;
        }

        return BestSolution;
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

        });

        return part2;
    }
}
