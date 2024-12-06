using System.Diagnostics;

using AdventOfCode2024;


public class Day06 : BaseDay
{
    [Flags]
    enum MapState
    {
        None = 0x00000,
        Object = 0x00001,
        VisitedUp = 0x00010,
        VisitedRight = 0x00100,
        VisitedDown = 0x01000,
        VisitedLeft = 0x10000,
        Visited = VisitedUp | VisitedRight | VisitedDown | VisitedLeft
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        Max
    }

    MapState[,] puzzleMap;
    int xMax;
    int yMax;

    (int, int) guardStart;

    public Day06(string[]? inputLines = null) : base(inputLines)
    {
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        puzzleMap = new MapState[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            char[] line = InputLines[y].ToCharArray();
            Debug.Assert(line.Length == xMax);
            for (int x = 0; x < xMax; x++)
            {
                switch (line[x])
                {
                    case '#':
                        puzzleMap[x, y] = MapState.Object;
                        break;
                    case '^':
                        puzzleMap[x, y] = MapState.VisitedUp;
                        guardStart.Item1 = x;
                        guardStart.Item2 = y;
                        break;
                    case '.':
                        puzzleMap[x, y] = MapState.None;
                        break;
                    default:
                        puzzleMap[x, y] = MapState.None;
                        break;
                }
            }
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        
        var guardPosition = (x: guardStart.Item1, y: guardStart.Item2);

        await Task.Run(() =>
        {
            MapState[,] currentPuzzleMap = (MapState[,])puzzleMap.Clone();
            bool exitsMap = WalkMap(guardPosition, ref currentPuzzleMap);
            Debug.Assert(exitsMap);

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    bool isVisted = (currentPuzzleMap[x, y] & MapState.Visited) != 0;
                    if (isVisted)
                    {
                        part1 += 1;
                    }
                }
            }

        });

        return part1;
    }

    private bool WalkMap((int x, int y) guardPosition, ref MapState[,]currentPuzzleMap)
    {
        Direction guardDirection = Direction.Up;

        while (true)
        {
            int nextX = guardPosition.x;
            int nextY = guardPosition.y;
            MapState visitType = MapState.None;
            switch (guardDirection)
            {
                case Direction.Up:
                    nextY -= 1;
                    visitType = MapState.VisitedUp;
                    break;
                case Direction.Down:
                    nextY += 1;
                    visitType = MapState.VisitedDown;
                    break;
                case Direction.Left:
                    nextX -= 1;
                    visitType = MapState.VisitedLeft;
                    break;
                case Direction.Right:
                    nextX += 1;
                    visitType = MapState.VisitedRight;
                    break;
            }

            if (nextX < 0 || nextX >= xMax || nextY < 0 || nextY >= yMax)
            {
                break;
            }

            if (currentPuzzleMap[nextX, nextY] == MapState.Object)
            {
                guardDirection++;
                if (guardDirection == Direction.Max)
                {
                    guardDirection = Direction.Up;
                }
                continue;
            }

            guardPosition.x = nextX;
            guardPosition.y = nextY;
            //If we find a tile that has already been visited in the same way that detects a loop.
            if ((currentPuzzleMap[guardPosition.x, guardPosition.y] & visitType) != 0)
            {
                return false;
            }
            currentPuzzleMap[guardPosition.x, guardPosition.y] |= visitType;
            
        }

        return true;
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            var guardPosition = (x: guardStart.Item1, y: guardStart.Item2);
            //Run it once originaly, then record all the visited tiles.
            MapState[,] visitedPuzzleMap = (MapState[,])puzzleMap.Clone();
            bool exitsMap = WalkMap(guardPosition, ref visitedPuzzleMap);
            Debug.Assert(exitsMap);

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    guardPosition = (x: guardStart.Item1, y: guardStart.Item2);
                    bool isVisted = (visitedPuzzleMap[x, y] & MapState.Visited) != 0;
                    if (isVisted) 
                    {
                        MapState[,] currentPuzzleMap = (MapState[,])puzzleMap.Clone();
                        currentPuzzleMap[x, y] = MapState.Object;
                        exitsMap = WalkMap(guardPosition, ref currentPuzzleMap);
                        if (!exitsMap)
                        {
                            part2 += 1;
                        }
                    }
                }
            }
        });

        return part2;
    }
}
