using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day10 : BaseDay
{
    struct TrailPoint
    {
        public TrailPoint(Point p, int h)
        {
            point = p;
            height = h;
        }

        public Point point;
        public int height;
    }

    int[,] puzzleMap;
    int xMax;
    int yMax;

    List<TrailPoint> trailHeads = new List<TrailPoint>();

    public Day10(string[]? inputLines = null) : base(inputLines)
    {
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        puzzleMap = new int[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            char[] line = InputLines[y].ToCharArray();
            Debug.Assert(line.Length == xMax);
            for (int x = 0; x < xMax; x++)
            {
                int height = int.Parse(line[x].ToString());
                puzzleMap[x, y] = height;
                if (height == 0)
                {
                    trailHeads.Add(new TrailPoint(new Point(x, y), height));
                }
            }
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            foreach (TrailPoint trailHead in trailHeads)
            {
                part1 += WalkTrailHead(trailHead);
            }
        });

        return part1;
    }

    private int WalkTrailHead(TrailPoint trailHead)
    {
        int score = 0;
        List<TrailPoint> trailPath = new List<TrailPoint>();
        trailPath.Add(trailHead);

        for (int i = 0; i < trailPath.Count; i++)
        {
            TrailPoint currentTP = trailPath[i];
            if (currentTP.height == 9)
            {
                score++;
                continue;
            }

            int nextHeight = currentTP.height + 1;
            Point pUp = new Point(currentTP.point.X, currentTP.point.Y - 1);
            EnsureNextPoint(nextHeight, pUp, ref trailPath);

            Point pDown = new Point(currentTP.point.X, currentTP.point.Y + 1);
            EnsureNextPoint(nextHeight, pDown, ref trailPath);
            
            Point pLeft = new Point(currentTP.point.X - 1, currentTP.point.Y);
            EnsureNextPoint(nextHeight, pLeft, ref trailPath);
            
            Point pRight = new Point(currentTP.point.X + 1, currentTP.point.Y);
            EnsureNextPoint(nextHeight, pRight, ref trailPath);
        }
        
        return score;
    }

    private void EnsureNextPoint(int expecteHeight, Point nextPoint, ref List<TrailPoint> trailHeads)
    {
        if (nextPoint.Y >= 0 && nextPoint.X >= 0 && nextPoint.X < xMax && nextPoint.Y < yMax)
        {
            int nextHeight = puzzleMap[nextPoint.X, nextPoint.Y];
            if (expecteHeight == nextHeight)
            {
                //Check that this point isn't already in the list as visited
                TrailPoint tp = new TrailPoint(nextPoint, nextHeight);
                if (!trailHeads.Contains(tp))
                {
                    trailHeads.Add(tp);
                }
            }
        }

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
