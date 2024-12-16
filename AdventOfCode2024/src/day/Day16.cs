using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


enum TileType
{
    Empty,
    Start,
    Wall,
    End
}

enum EDirection
{
    North,
    South,
    West,
    East
}

class Tile(TileType type, int x, int y)
{
    public TileType Type { get; set; } = type;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public EDirection Facing { get; set; }
    public int G { get; set; } // cost to move from start to this tile
    public int H { get; set; } // heuristic cost to move from this tile to end
    public int F { get; set; } // total cost
    public Tile Parent { get; set; }
    
    public EDirection ParentFacing { get; set; }
}


class AStar
{
    private List<Tile> _openList;
    private List<Tile> _closedList;
    private Tile[,] _map;
    private Tile _start;
    private Tile _end;
    private EDirection _facingDir;
    private int xMax;
    private int yMax;

    public AStar(Tile[,] map, Tile start, Tile end, EDirection facingDir)
    {
        _map = map;
        _start = start;
        _end = end;
        _openList = new List<Tile>();
        _closedList = new List<Tile>();
        _start.Facing = facingDir;

        _facingDir = facingDir;

        xMax = map.GetLength(0);
        yMax = map.GetLength(1);
    }

    public List<Tile> FindPath()
    {
        // initialize start tile
        _start.G = 0;
        _start.H = CalculateHeuristic(_start, _end);
        _start.F = _start.G + _start.H;
        _openList.Add(_start);

        while (_openList.Count > 0)
        {
            // get tile with lowest F cost
            Tile currentTile = _openList[0];
            _openList.RemoveAt(0);
            _closedList.Add(currentTile);

           // check if we've reached the end
            if (currentTile.X == _end.X && currentTile.Y == _end.Y)
            {
                return ReconstructPath(currentTile);
            }

            // explore neighbors
            foreach (EDirection direction in Enum.GetValues(typeof(EDirection)))
            {
                int newX = currentTile.X;
                int newY = currentTile.Y;

                switch (direction)
                {
                    case EDirection.North:
                        newY--;
                        break;
                    case EDirection.South:
                        newY++;
                        break;
                    case EDirection.East:
                        newX++;
                        break;
                    case EDirection.West:
                        newX--;
                        break;
                }

                if (newX < 0 || newX >= xMax || newY < 0 || newY >= yMax)
                {
                    continue;
                }

                Tile neighbor = _map[newX, newY];
                if (neighbor.Type == TileType.Wall || _closedList.Contains(neighbor))
                {
                    continue;
                }

                int turningCost = CalculateTurningCost(currentTile.Facing, direction);
                int tentativeG = currentTile.G + 1 + turningCost;

                if (_openList.Contains(neighbor))
                {
                    if (tentativeG < neighbor.G)
                    {
                        neighbor.G = tentativeG;
                        neighbor.Parent = currentTile;
                        neighbor.ParentFacing = currentTile.Facing;
                    }
                }
                else
                {
                    neighbor.G = tentativeG;
                    neighbor.H = CalculateHeuristic(neighbor, _end);
                    neighbor.F = neighbor.G + neighbor.H;
                    neighbor.Parent = currentTile;
                    neighbor.ParentFacing = currentTile.Facing;
                    neighbor.Facing = direction;
                    _openList.Add(neighbor);
                }
            }

            // sort open list by F cost
            _openList.Sort((a, b) => a.F.CompareTo(b.F));
        }

        // no path found
        return null;
    }

    private List<Tile> ReconstructPath(Tile currentTile)
    {
        List<Tile> path = new List<Tile>();
        while (currentTile != null)
        {
            path.Add(currentTile);
            currentTile = currentTile.Parent;
        }
        path.Reverse();
        return path;
    }

    private Tile[] GetNeighbors(Tile tile)
    {
        Tile[] neighbors = new Tile[4];
        int x = tile.X;
        int y = tile.Y;

        // up
        if (y > 0 && _map[x, y - 1].Type != TileType.Wall)
        {
            neighbors[(int)EDirection.North] = _map[x, y - 1];
        }

        // down
        if (y < yMax && _map[x, y + 1].Type != TileType.Wall)
        {
            neighbors[(int)EDirection.South] = _map[x, y + 1];
        }

        // left
        if (x > 0 && _map[x - 1, y].Type != TileType.Wall)
        {
            neighbors[(int)EDirection.West] = _map[x - 1, y];
        }

        // right
        if (x < xMax && _map[x + 1, y].Type != TileType.Wall)
        {
            neighbors[(int)EDirection.East] = _map[x + 1, y];
        }

        return neighbors;
    }

    private int CalculateHeuristic(Tile tile, Tile end)
    {
        return Math.Abs(tile.X - end.X) + Math.Abs(tile.Y - end.Y);
    }
    private int CalculateTurningCost(EDirection currentDirection, EDirection newDirection)
    {
        if (currentDirection == newDirection)
        {
            return 0;
        }

        int turningCost = 1000;

        // handle special cases where turning cost is double (e.g., turning from north to south)
        if ((currentDirection == EDirection.North && newDirection == EDirection.South) ||
            (currentDirection == EDirection.South && newDirection == EDirection.North) ||
            (currentDirection == EDirection.East && newDirection == EDirection.West) ||
            (currentDirection == EDirection.West && newDirection == EDirection.East))
        {
            turningCost = 2000;
        }        

        return turningCost;
    }

    public void PrintMap(List<Tile> path)
    {
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {                
                switch (_map[x, y].Type)
                {
                    case TileType.Wall:
                        Console.Write("#");
                        break;
                    case TileType.Start:
                        Console.Write("S");
                        break;
                    case TileType.End:
                        Console.Write("E");
                        break;
                    case TileType.Empty:
                    {
                        if (path.Contains(_map[x, y]))
                        {
                            Console.Write("@");
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    } break;
                }
            }
            Console.WriteLine();
        }
    }
}

public class Day16 : BaseDay
{    
    Tile[,] map;

    int xMax = 0;
    int yMax = 0;
    
    Point PlayerPos = new Point(0, 0);
    Point EndPos = new Point(0, 0);
    EDirection PlayerDir = EDirection.South;

    public Day16(string[]? inputLines = null) : base(inputLines)
    {
        int yCur = 0;
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        map = new Tile[xMax, yMax];

        foreach (string line in InputLines)
        {
            char[] lineChars = line.ToCharArray();
            
            for (int x = 0; x < lineChars.Length; x++)
            {
                switch (lineChars[x])
                {
                    case '#':
                        map[x, yCur] = new Tile(TileType.Wall, x, yCur);
                        break;
                    case '.':
                        map[x, yCur] = new Tile(TileType.Empty, x, yCur);
                        break;
                    case 'S':
                        // map[x, yCur] = new Tile(TileType.Start, x, yCur);
                        // PlayerPos = new Point(x, yCur);
                        map[x, yCur] = new Tile(TileType.End, x, yCur);
                        EndPos = new Point(x, yCur);
                        break;
                    case 'E':
                        // map[x, yCur] = new Tile(TileType.End, x, yCur);
                        // EndPos = new Point(x, yCur);
                        map[x, yCur] = new Tile(TileType.Start, x, yCur);
                        PlayerPos = new Point(x, yCur);
                        break;
                }
            }
            yCur++;
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {
            AStar aStar = new AStar(map, map[PlayerPos.X, PlayerPos.Y], map[EndPos.X, EndPos.Y], PlayerDir);
            List<Tile> path = aStar.FindPath();
            aStar.PrintMap(path);
            int pathCost = path.Last().G;
            part1 = pathCost;
        });
        
        //101496 TO HIGH
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
