using System.Diagnostics;

using AdventOfCode2024;


public class Day13 : BaseDay
{
    class ClawProblem
    {
        public (int, int) AButton = (0, 0);
        public (int, int) BButton = (0, 0);
        public (int, int) Prize = (0, 0);

        public List<(int, int)> Solutions = new();
        public int BestSolution = int.MaxValue;
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

    
    public List<(int, int)> FindCombinations(int Pv, int Av, int Bv)
    {
        //Diophantine equation approach
        List<(int, int)> solutions = new List<(int, int)>();

        // Try possible values of nB
        for (int nB = 0; nB <= Pv / Bv; nB++)
        {
            // Calculate remaining amount after nB contribution
            int remaining = Pv - (nB * Bv);

            // Check if remaining is divisible by A value
            if (remaining % Av == 0)
            {
                int nA = remaining / Av;
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
                List<(int, int)> xSolutions = FindCombinations(problem.Prize.Item1, problem.AButton.Item1, problem.BButton.Item1);
                List<(int, int)> ySolutions = FindCombinations(problem.Prize.Item2, problem.AButton.Item2, problem.BButton.Item2);
                //First check that there even are solutions for each button coordinate
                if (xSolutions.Count == 0 || ySolutions.Count == 0)
                {
                    continue;
                }
                //Now We need to find the intersection of the two sets of solutions
                List<(int, int)> solutions = new List<(int, int)>();
                foreach ((int nA, int nB) in xSolutions)
                {
                    if (ySolutions.Contains((nA, nB)))
                    {
                        solutions.Add((nA, nB));
                        int solutionCost = (nA*3) + nB;
                        if (solutionCost < problem.BestSolution)
                        {
                            problem.BestSolution = solutionCost;
                        }
                    }
                }
                if (solutions.Count > 0)
                {
                    problem.Solutions.AddRange(solutions);
                    part1 += problem.BestSolution;
                }
            }

        });

        Console.WriteLine($"Part 1: {part1}");
        return 0;
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
