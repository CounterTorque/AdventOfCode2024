using System.Diagnostics;

using AdventOfCode2024;


public class Day06 : BaseDay
{
    enum MapState
    {
        None,
        Visited,
        Object
    }

    enum Direction {
        Up,
        Right,
        Down,
        Left,
        Max
    }

    
    public Day06(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        Direction guardDirection = Direction.Up;
        var guardPosition = (x: 0, y: 0);

        int xMax = InputLines[0].Length;
        int yMax = InputLines.Length;

        MapState[,] puzzleMap = new MapState[xMax, yMax];
        for (int y = 0; y < yMax; y++)
        {
            char[] line = InputLines[y].ToCharArray();
            Debug.Assert(line.Length == xMax);
            for (int x = 0; x < xMax; x++)
            {
                switch (line[x])
                {
                    case '#':
                        puzzleMap[x,y] = MapState.Object;
                        break;
                    case '^':
                        puzzleMap[x,y] = MapState.Visited;
                        guardPosition.x = x;
                        guardPosition.y = y;
                        break;
                    case '.':
                        puzzleMap[x,y] = MapState.None;
                        break;
                    default:
                        puzzleMap[x,y] = MapState.None;
                        break;
                }
            }
        }

        

        await Task.Run(() => {

            while (true)
            {
                int nextX = guardPosition.x;
                int nextY = guardPosition.y;
                switch (guardDirection)
                {
                    case Direction.Up:
                        nextY -= 1;
                        break;
                    case Direction.Down:
                        nextY += 1;
                        break;
                    case Direction.Left:
                        nextX -= 1;
                        break;
                    case Direction.Right:
                        nextX += 1;
                        break;
                }

                if (nextX < 0 || nextX >= xMax || nextY < 0 || nextY >= yMax)
                {
                    break;
                }

                if (puzzleMap[nextX, nextY] == MapState.Object)
                {
                    //Blocked by an object, turn right
                    guardDirection = guardDirection + 1;
                    if (guardDirection == Direction.Max)
                    {
                        guardDirection = Direction.Up;
                    }
                    continue;
                }

                guardPosition.x = nextX;
                guardPosition.y = nextY;
                puzzleMap[guardPosition.x, guardPosition.y] = MapState.Visited;
            }

            //If we get here, the guard is outside the map
            //And we can count the visited spaces.
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (puzzleMap[x,y] == MapState.Visited)
                    {
                        part1 += 1;
                    }
                }
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
