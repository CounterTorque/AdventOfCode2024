using System.Diagnostics;
using System.Drawing;

using AdventOfCode2024;


public class Day08 : BaseDay
{

    Dictionary<char, List<Point>> AntennaNodes = new Dictionary<char, List<Point>>();
    Dictionary<char, HashSet<Point>> AntiNodes = new Dictionary<char, HashSet<Point>>();

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

            foreach (char c in AntennaNodes.Keys)
            {
                BuildAntiNodes(c, false);
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

    private void BuildAntiNodes(char c, bool useResonants = false)
    {
        List<Point> antennaNodes = AntennaNodes[c];
         if (!AntiNodes.ContainsKey(c))
        {
            AntiNodes[c] = new HashSet<Point>();
        }

        for (int i = 0; i < antennaNodes.Count; i++)
        {
            Point p1 = antennaNodes[i];
            for (int j = i + 1; j < antennaNodes.Count; j++)
            {
                Point p2 = antennaNodes[j];
                Point delta = new Point(p2.X - p1.X, p2.Y - p1.Y);
                Point? a1 = AddAntiNode(c, p1, delta, false);

                Point? a2 = AddAntiNode(c, p2, delta, true);
            }
        }
    }

    private Point? AddAntiNode(char c, Point p1, Point delta, bool positive)
    {
        Point pd;
        if (positive)
        {
            pd = new Point(p1.X + delta.X, p1.Y + delta.Y);    
        }
        else
        {
            pd = new Point(p1.X - delta.X, p1.Y - delta.Y);
        }
        
        if ((pd.X >= 0) && (pd.Y >= 0) && (pd.X < xMax) && (pd.Y < yMax))
        {
            AntiNodes[c].Add(pd);
            return pd;
        }

        return null;
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

             foreach (char c in AntennaNodes.Keys)
            {
                BuildAntiNodes(c, true);
            }

            HashSet<Point> uniqueNodes = new HashSet<Point>();
            foreach (char c in AntiNodes.Keys)
            {
                foreach (Point p in AntiNodes[c])
                {
                    uniqueNodes.Add(p);
                }
            }

            part2 = uniqueNodes.Count;


        });

        return part2;
    }
}
