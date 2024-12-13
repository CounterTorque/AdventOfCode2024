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

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

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
