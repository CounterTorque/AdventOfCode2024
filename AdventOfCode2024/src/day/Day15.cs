using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day15 : BaseDay
{
    [Flags]
    enum EMapState
    {
        Empty = 1,        
        Robot = 2,
        Wall = 4,
        BoxLeft = 8,
        BoxRight = 16,
        Box = BoxLeft & BoxRight,
    }

    enum EDirection
    {
        North,
        South,
        West,
        East
    }

    EMapState[,] map;
    int xMax = 0;
    int yMax = 0;

    List<EDirection> directions = new List<EDirection>();
    Point robotPos = new Point(0, 0);
    
    public Day15(string[]? inputLines = null) : base(inputLines)
    {
        
    }

    private bool CanMoveInto(Point objectPos, Point delta)
    {
        Point newPos = new Point(objectPos.X + delta.X, objectPos.Y + delta.Y);
        if ((newPos.X < 0) || (newPos.Y < 0) || (newPos.X >= xMax) || (newPos.Y >= yMax))
        {
            return false;
        }

        switch(map[newPos.X, newPos.Y])
        {
            case EMapState.Wall:
                return false;
            case EMapState.Box:
            {
                if (CanMoveInto(newPos, delta))
                {
                    Point nextPos = new Point(newPos.X + delta.X, newPos.Y + delta.Y);
                    map[nextPos.X, nextPos.Y] = EMapState.Box;
                    map[newPos.X, newPos.Y] = EMapState.Empty;
                    return true;
                }
                return false;
            }
            case EMapState.BoxLeft:
            case EMapState.BoxRight:
                //TODO
                return false;
            case EMapState.Robot:
                return true;
            case EMapState.Empty:
                return true;
        }

        return false;
    }

    public override async ValueTask<int> Solve_1()
    {
         bool isMapParse = true;
        int yCur = 0;
        foreach (string line in InputLines)
        {
            if (line == "")
            {
                yMax = yCur;
                break;
            }
            xMax = line.Length;
            yCur++;
        }

        yCur = 0;
        map = new EMapState[xMax, yMax];

        foreach (string line in InputLines)
        {
            char[] lineChars = line.ToCharArray();

            if (isMapParse)    
            {
                if (line == "")
                {
                    isMapParse = false;
                    continue;
                }
               
                for (int x = 0; x < lineChars.Length; x++)
                {
                    switch (lineChars[x])
                    {
                        case '#':
                            map[x, yCur] = EMapState.Wall;
                            break;
                        case '.':
                            map[x, yCur] = EMapState.Empty;
                            break;
                        case 'O':
                            map[x, yCur] = EMapState.Box;
                            break;
                        case '@':
                            map[x, yCur] = EMapState.Robot;
                            //map[x, yCur] = EMapState.Empty;
                            robotPos = new Point(x, yCur);
                            break;
                    }
                }
                yCur++;
                continue;
            }

            // Parse Directions
            foreach(char c in lineChars)
            {
                switch (c)
                {
                    case '^':
                        directions.Add(EDirection.North);
                        break;
                    case 'v':
                        directions.Add(EDirection.South);
                        break;
                    case '<':
                        directions.Add(EDirection.West);
                        break;
                    case '>':
                        directions.Add(EDirection.East);
                        break;
                }    
            }
        }

        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {
            
            for (int i = 0; i < directions.Count; i++)
            {
                Point delta = new Point(0, 0);
                switch (directions[i])
                {
                    case EDirection.North:
                        delta = new Point(0, -1);
                        break;
                    case EDirection.South:
                        delta = new Point(0, 1);
                        break;
                    case EDirection.West:
                        delta = new Point(-1, 0);
                        break;
                    case EDirection.East:
                        delta = new Point(1, 0);
                        break;
                }

                bool canMove = CanMoveInto(robotPos, delta);
                if (canMove)
                {
                    map[robotPos.X, robotPos.Y] = EMapState.Empty;
                    robotPos.X += delta.X;
                    robotPos.Y += delta.Y;
                    map[robotPos.X, robotPos.Y] = EMapState.Robot;                    
                }
                //PrintMap();
            }


            //Calc GPS For Boxes
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (map[x, y] == EMapState.Box)
                    {
                        int boxGPS = (y * 100) + x;
                        part1 += boxGPS;
                    }
                }
            }
            
        });
        
        //1430439 
        return part1;
    }

    private void PrintMap()
    {
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                switch (map[x, y])
                {
                    case EMapState.Wall:
                        Console.Write("#");
                        break;
                    case EMapState.Box:
                        Console.Write("O");
                        break;
                    case EMapState.Robot:
                        Console.Write("@");
                        break;
                    case EMapState.Empty:
                        Console.Write(".");
                        break;
                }
            }
            Console.WriteLine();
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
