using System.Diagnostics;
using System.Drawing;

using AdventOfCode2024;


public class Day08 : BaseDay
{

    Dictionary<char, List<Point>> AntennaNodes = new Dictionary<char, List<Point>>();
    Dictionary<char, List<Point>> AntiNodes = new Dictionary<char, List<Point>>();

    int xMax = 0;
    int yMax = 0;

    public Day08(string[]? inputLines = null) : base(inputLines)
    {
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;

        for (int y = 0; y < InputLines.Length; y++)
        {
            char[] line = InputLines[y].ToCharArray();
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                if (c == '.')
                {
                    continue;
                }
                
                if (!AntennaNodes.ContainsKey(c))
                {
                    AntennaNodes[c] = new List<Point>();
                }
                AntennaNodes[c].Add(new Point(x, y));
            }
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            //foreach (char c in AntennaNodes.Keys)
            // For each Node, find the delta between it an all other nodes of its type
            // For each delta, create an antinode between the two nodes. This is a list of points

            foreach (char c in AntennaNodes.Keys)
            {
                BuildAntiNodes(c);
            }

            HashSet<Point> uniqueNodes = new HashSet<Point>();
            foreach (char c in AntiNodes.Keys)
            {
                foreach (Point p in AntiNodes[c])
                {
                    uniqueNodes.Add(p);
                }
            }

            part1 = uniqueNodes.Count;

        });
        
        return part1;
    }

    private void BuildAntiNodes(char c)
    {
        List<Point> antennaNodes = AntennaNodes[c];
        for (int i = 0; i < antennaNodes.Count; i++)
        {
            Point p1 = antennaNodes[i];
            for (int j = i + 1; j < antennaNodes.Count; j++)
            {
                Point p2 = antennaNodes[j];
                Point delta = new Point(p2.X - p1.X, p2.Y - p1.Y);

                Point a1 = new Point(p1.X - delta.X, p1.Y - delta.Y);
                Point a2 = new Point(p2.X + delta.X, p2.Y + delta.Y);

                if (!AntiNodes.ContainsKey(c))
                {
                    AntiNodes[c] = new List<Point>();
                }
                
                
                //Check if the antinode already exists (VERIFY THIS IS NOT OBJECT BASED EQUALITY)
                if (!AntiNodes[c].Contains(a1) && (a1.X >= 0) && (a1.Y >= 0) && (a1.X < xMax) && (a1.Y < yMax))
                {
                    AntiNodes[c].Add(a1);
                }

                if (!AntiNodes[c].Contains(a2) && (a2.X >= 0) && (a2.Y >= 0) && (a2.X < xMax) && (a2.Y < yMax))
                {
                    AntiNodes[c].Add(a2);
                }
            }
        }
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
