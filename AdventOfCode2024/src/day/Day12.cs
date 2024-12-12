using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day12 : BaseDay
{
    struct Plot
    {
        public char Type;
        public int FenceSides;
    }

    int xMax;
    int yMax;

    Plot[,] PlotMap;
    List<List<Plot>> PlotSets = new List<List<Plot>>();
    
    public Day12(string[]? inputLines = null) : base(inputLines)
    {
        Queue<Point> plotQueue = new Queue<Point>();
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        PlotMap = new Plot[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                plotQueue.Enqueue(new Point(x, y));
                char plotType = InputLines[y][x];
                PlotMap[x, y] = new Plot() { Type = plotType, FenceSides = 0 };
            }
        }

        while (plotQueue.Count > 0)
        {
            Point curPoint = plotQueue.Dequeue();
            
        }

        //TODO: Instead of walking the queue, we need to look at all the neighbors of the current point. 
        // If they are not visited, add those to a seperate queue for this plot TYPE. 
        // We need to store the Plots in a dictionary based on the plot type. 
        //However, we need more than just the type, because we can have duplicate, but unique plot types that are disconnected. 



    }

    public override async ValueTask<int> Solve_1()
    {

        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

            foreach (List<Plot> plotset in PlotSets)
            {
                int area = plotset.Count;
                int pererimeter = 0;
                foreach (Plot plot in plotset)
                {
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
