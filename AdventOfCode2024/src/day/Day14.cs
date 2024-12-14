using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day14 : BaseDay
{
    class Robot
    {
        public Point Position = new Point(0, 0);
        public Point Velocity = new Point(0, 0);

        public int CycleLength = 0;
    }

    List<Robot> robots = new List<Robot>();
    static int xMax = 101;
    static int yMax = 103;

    static int xQuadrantMid = xMax / 2;
    static int yQuadrantMid = yMax / 2;

    public Day14(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            string[] parts = line.Split(" ");
            string pattern = @"(?<=\w+=)(-?\d+)(?:,(-?\d+))?";

            Match matchPos = Regex.Match(parts[0], pattern);
            Debug.Assert(matchPos.Success, "Invalid line: " + parts[0]);

            int x = int.Parse(matchPos.Groups[1].Value);
            int y = int.Parse(matchPos.Groups[2].Value);

            Match matchVel = Regex.Match(parts[1], pattern);
            Debug.Assert(matchVel.Success, "Invalid line: " + parts[1]);
            int vx = int.Parse(matchVel.Groups[1].Value);
            int vy = int.Parse(matchVel.Groups[2].Value);

            robots.Add(new Robot { Position = new Point(x, y), Velocity = new Point(vx, vy) });
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int iterations = 100;
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            int[] quadrentCounts = new int[4] { 0, 0, 0, 0 };

            //Calculate the position after 100 iterations
            foreach (Robot robot in robots)
            {
                int xNext = robot.Position.X + (robot.Velocity.X * iterations);
                int yNext = robot.Position.Y + (robot.Velocity.Y * iterations);

                int xWrap = ((xNext % xMax) + xMax) % xMax;
                int yWrap = ((yNext % yMax) + yMax) % yMax;
                robot.Position = new Point(xWrap, yWrap);

                if (robot.Position.X < xQuadrantMid && robot.Position.Y < yQuadrantMid)
                {
                    quadrentCounts[0] += 1;
                }
                else if (robot.Position.X > xQuadrantMid && robot.Position.Y < yQuadrantMid)
                {
                    quadrentCounts[1] += 1;
                }
                else if (robot.Position.X < xQuadrantMid && robot.Position.Y > yQuadrantMid)
                {
                    quadrentCounts[2] += 1;
                }
                else if (robot.Position.X > xQuadrantMid && robot.Position.Y > yQuadrantMid)
                {
                    quadrentCounts[3] += 1;
                }
                //We are dropping those that are on the midline on purpose for the puzzle
            }

            int safetyFactor = quadrentCounts[0] * quadrentCounts[1] * quadrentCounts[2] * quadrentCounts[3];
            part1 = safetyFactor;


        });

        return part1;
    }

    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int LCM(int a, int b)
    {
        return (a / GCD(a, b)) * b;
    }

    int SingleCycle(int pos, int vel, int wrap)
    {
        int gcd = GCD(vel, wrap);
        if (gcd == 1)
        {
            return wrap;
        }

        return wrap * vel / gcd;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        var individualCycles = new List<int>();
        await Task.Run(() =>
        {

            //Calculate the position after 100 iterations
            foreach (Robot robot in robots)
            {
                int xCycle = SingleCycle(robot.Position.X, robot.Velocity.X, xMax);
                int yCycle = SingleCycle(robot.Position.Y, robot.Velocity.Y, yMax);
                robot.CycleLength = LCM(xCycle, yCycle);
                individualCycles.Add(robot.CycleLength);
            }

            part2 = individualCycles.Aggregate((a, b) => LCM(a, b));
         

        });

        //78573859  To HIGH
        //10403 TO HIGH
        return part2;
    }
}
