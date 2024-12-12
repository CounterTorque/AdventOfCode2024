using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day12 : BaseDay
{
    class Plot
    {
        public char Type;
        public int FenceSides;
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
                PlotMap[x, y] = new Plot() { Type = plotType, FenceSides = 0 };
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
                    curPlot.FenceSides++;
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
                    curPlot.FenceSides++;
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
                    curPlot.FenceSides++;
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
                    curPlot.FenceSides++;
                }
            }  
        }
    }

    public override async ValueTask<int> Solve_1()
    {

        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            foreach (List<Point> plotset in PlotSets)
            {
                int area = plotset.Count;
                int pererimeter = 0;
                foreach (Point plotPoint in plotset)
                {
                    Plot plot = PlotMap[plotPoint.X, plotPoint.Y];
                    pererimeter += plot.FenceSides;
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
        await Task.Run(() => {

        });

        return part2;
    }
}
