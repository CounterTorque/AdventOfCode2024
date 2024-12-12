using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day12 : BaseDay
{
    [Flags]
    enum FenceSides
    {
        Up = 0x1,
        Down = 0x10,
        Left = 0x100,
        Right = 0x1000
    };

    class Plot
    {
        public char Type;
        public int PlotSides;

        public FenceSides Fenced { get; set; }
    }

    int xMax;
    int yMax;

    Plot[,] PlotMap;
    List<List<Point>> PlotSets = new List<List<Point>>();

    public Day12(string[]? inputLines = null) : base(inputLines)
    {
        List<Point> plotCollection = new List<Point>();
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        PlotMap = new Plot[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                plotCollection.Add(new Point(x, y));
                char plotType = InputLines[y][x];
                PlotMap[x, y] = new Plot() { Type = plotType, PlotSides = 0 };
            }
        }

        while (plotCollection.Count > 0)
        {
            Queue<Point> plotQueue = new Queue<Point>();
            Point curPoint = plotCollection.Last();
            plotCollection.Remove(curPoint);
            plotQueue.Enqueue(curPoint);
            char currentPlotType = PlotMap[curPoint.X, curPoint.Y].Type;
            List<Point> plotSet = new List<Point>();
            PlotSets.Add(plotSet);

            while (plotQueue.Count > 0)
            {
                curPoint = plotQueue.Dequeue();
                Plot curPlot = PlotMap[curPoint.X, curPoint.Y];
                plotSet.Add(curPoint);
                Debug.Assert(curPlot.Type == currentPlotType);

                //Look at all the cardinal neighbors. 
                Point pUp = new Point(curPoint.X, curPoint.Y - 1);
                Point pDown = new Point(curPoint.X, curPoint.Y + 1);
                Point pLeft = new Point(curPoint.X - 1, curPoint.Y);
                Point pRight = new Point(curPoint.X + 1, curPoint.Y);

                if (pUp.Y >= 0 && pUp.X >= 0 && pUp.X < xMax && pUp.Y < yMax && PlotMap[pUp.X, pUp.Y].Type == currentPlotType)
                {
                    if (plotCollection.Remove(pUp))
                    {
                        plotQueue.Enqueue(pUp);
                    }
                }
                else
                {
                    curPlot.PlotSides++;
                    curPlot.Fenced |= FenceSides.Up;
                }

                if (pDown.Y >= 0 && pDown.X >= 0 && pDown.X < xMax && pDown.Y < yMax && PlotMap[pDown.X, pDown.Y].Type == currentPlotType)
                {
                    if (plotCollection.Remove(pDown))
                    {
                        plotQueue.Enqueue(pDown);
                    };
                }
                else
                {
                    curPlot.PlotSides++;
                    curPlot.Fenced |= FenceSides.Down;
                }

                if (pLeft.Y >= 0 && pLeft.X >= 0 && pLeft.X < xMax && pLeft.Y < yMax && PlotMap[pLeft.X, pLeft.Y].Type == currentPlotType)
                {
                    if (plotCollection.Remove(pLeft))
                    {
                        plotQueue.Enqueue(pLeft);
                    }
                }
                else
                {
                    curPlot.PlotSides++;
                    curPlot.Fenced |= FenceSides.Left;
                }

                if (pRight.Y >= 0 && pRight.X >= 0 && pRight.X < xMax && pRight.Y < yMax && PlotMap[pRight.X, pRight.Y].Type == currentPlotType)
                {
                    if (plotCollection.Remove(pRight))
                    {
                        plotQueue.Enqueue(pRight);
                    }
                }
                else
                {
                    curPlot.PlotSides++;
                    curPlot.Fenced |= FenceSides.Right;
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

            foreach (List<Point> plotset in PlotSets)
            {
                int area = plotset.Count;
                int pererimeter = 0;
                foreach (Point plotPoint in plotset)
                {
                    Plot plot = PlotMap[plotPoint.X, plotPoint.Y];
                    pererimeter += plot.PlotSides;
                }
                part1 += area * pererimeter;
            }

        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

            foreach (List<Point> plotset in PlotSets)
            {
                int area = plotset.Count;
                int sides = 0;
                foreach (FenceSides currentFence in Enum.GetValues(typeof(FenceSides)))
                {
                    foreach (Point curPoint in plotset)
                    {
                        Point deltaUp = new Point(0, -1);
                        Point deltaDown = new Point(0, 1);
                        Point deltaLeft = new Point(-1, 0);
                        Point deltaRight = new Point(1, 0);

                        Plot plot = PlotMap[curPoint.X, curPoint.Y];
                        if (plot.Fenced.HasFlag(currentFence))
                        {
                            sides++;
                            plot.Fenced &= ~currentFence; //Remove the side we just counted.

                            //Move along the left and right of this and remove if found. If not found stop.
                            if (currentFence == FenceSides.Left || currentFence == FenceSides.Right)
                            {
                                ClaimFence(plotset, currentFence, curPoint, deltaUp);
                                ClaimFence(plotset, currentFence, curPoint, deltaDown);
                            }
                            else
                            {
                                ClaimFence(plotset, currentFence, curPoint, deltaLeft);
                                ClaimFence(plotset, currentFence, curPoint, deltaRight);
                            }
                        }
                    }
                }

                part2 += area * sides;
            }

        });

        return part2;
    }

    private void ClaimFence(List<Point> plotset, FenceSides currentFence, Point pointCurrent, Point delta)
    {
        Point pNext = new Point(pointCurrent.X + delta.X, pointCurrent.Y + delta.Y);
        while (plotset.Contains(pNext))
        {
            Plot plotNext = PlotMap[pNext.X, pNext.Y];
            if (!plotNext.Fenced.HasFlag(currentFence))
            {
                break;
            }
            plotNext.Fenced &= ~currentFence; //Remove the side we just counted.
            pNext = new Point(pNext.X + delta.X, pNext.Y + delta.Y);
        }
    }
}
