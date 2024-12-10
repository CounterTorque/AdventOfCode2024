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
                part1 = WalkTrailHead(trailHead);
            }
        });

        return part1;
    }

    private int WalkTrailHead(TrailPoint trailHead)
    {
        int score = 0;
        ConcurrentQueue<TrailPoint> trailHeads = new ConcurrentQueue<TrailPoint>();
        trailHeads.Enqueue(trailHead);
        foreach (TrailPoint trailPoint in trailHeads)
        {
            TrailPoint currentTP;
            bool success = trailHeads.TryDequeue(out currentTP);
            Debug.Assert(success);

            if (currentTP.height == 9)
            {
                score++;
                continue;
            }

            int nextHeight = currentTP.height + 1;
            Point pUp = new Point(currentTP.point.X, currentTP.point.Y - 1);
            if (ValidNextPoint(currentTP.height, pUp))
            {
                //Check that this point isn't already in the list as visited
            }

            Point pDown = new Point(currentTP.point.X, currentTP.point.Y + 1);
            if (ValidNextPoint(currentTP.height, pDown))
            {
                
            }

            Point pLeft = new Point(currentTP.point.X - 1, currentTP.point.Y);
            if (ValidNextPoint(currentTP.height, pLeft))
            {
                
            }

            Point pRight = new Point(currentTP.point.X + 1, currentTP.point.Y);
            if (ValidNextPoint(currentTP.height, pRight))
            {

            }
        }
        
        return score;
    }

    private bool ValidNextPoint(int expecteHeight, Point nextPoint)
    {
        if (nextPoint.Y >= 0 && nextPoint.X >= 0 && nextPoint.X < xMax && nextPoint.Y < yMax)
        {
            int nextHeight = puzzleMap[nextPoint.X, nextPoint.Y];
            return expecteHeight == nextHeight;
        }

        return false;
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
