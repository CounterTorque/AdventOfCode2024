using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day18 : BaseDay
{
    class AStar
    {
        public enum TileType
        {
            Empty,
            Start,
            Wall,
            End
        }

        public enum EDirection
        {
            North,
            South,
            West,
            East
        }

        public class Tile(TileType type, int x, int y)
        {
            public TileType Type { get; set; } = type;
            public int X { get; set; } = x;
            public int Y { get; set; } = y;
            public int G { get; set; } // cost to move from start to this tile
            public int H { get; set; } // heuristic cost to move from this tile to end
            public int F { get; set; } // total cost
            public Tile Parent { get; set; }

        }

        private List<Tile> _openList;
        private List<Tile> _closedList;
        private Tile[,] _map;
        private Tile _start;
        private Tile _end;
        private int xMax;
        private int yMax;

        public AStar(Tile[,] map, Tile start, Tile end)
        {
            _map = map;
            _start = start;
            _end = end;
            _openList = new List<Tile>();
            _closedList = new List<Tile>();

            xMax = map.GetLength(0);
            yMax = map.GetLength(1);
        }

        public List<List<Tile>> FindAllBestPaths()
        {
            return null;
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

                    int tentativeG = currentTile.G + 1;

                    if (_openList.Contains(neighbor))
                    {
                        if (tentativeG < neighbor.G)
                        {
                            neighbor.G = tentativeG;
                            neighbor.Parent = currentTile;
                        }
                    }
                    else
                    {
                        neighbor.G = tentativeG;
                        neighbor.H = CalculateHeuristic(neighbor, _end);
                        neighbor.F = neighbor.G + neighbor.H;
                        neighbor.Parent = currentTile;
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

        private int CalculateHeuristic(Tile tile, Tile end)
        {
            return Math.Abs(tile.X - end.X) + Math.Abs(tile.Y - end.Y);
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
                            }
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }

    const int xMax = 70 + 1;
    const int yMax = 70 + 1;
    AStar.Tile[,] map = new AStar.Tile[xMax, yMax];
    Point PlayerPos = new Point(0, 0);
    Point EndPos = new Point(xMax - 1, yMax - 1);

    struct BytePos
    {
        public int X;
        public int Y;
    }

    List<BytePos> fallingBytes = new List<BytePos>();

    public Day18(string[]? inputLines = null) : base(inputLines)
    {
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                map[x, y] = new AStar.Tile(AStar.TileType.Empty, x, y);
            }
        }

        foreach (string line in InputLines)
        {
            string[] nums = Regex.Split(line, ",");
            Debug.Assert(nums.Length == 2, "Invalid line: " + line);

            int x = int.Parse(nums[0]);
            int y = int.Parse(nums[1]);
            fallingBytes.Add(new BytePos { X = x, Y = y });
        }

    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        int MaxFalling = 1024;

        for (int i = 0; i < fallingBytes.Count; i++)
        {
            if (i >= MaxFalling) break;

            BytePos bp = fallingBytes[i];
            map[bp.X, bp.Y].Type = AStar.TileType.Wall;
        }


        await Task.Run(() =>
        {
            AStar aStar = new AStar(map, map[PlayerPos.X, PlayerPos.Y], map[EndPos.X, EndPos.Y]);
            List<AStar.Tile> path = aStar.FindPath();
            aStar.PrintMap(path);
            int pathCost = path.Last().G;
            part1 = pathCost;
        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            int MaxFalling = 1024;
            while (MaxFalling < fallingBytes.Count)
            {
                Console.WriteLine($"Trying {MaxFalling} falling bytes");
                for (int y = 0; y < yMax; y++)
                {
                    for (int x = 0; x < xMax; x++)
                    {
                        map[x, y] = new AStar.Tile(AStar.TileType.Empty, x, y);
                    }
                }

                for (int i = 0; i < MaxFalling; i++)
                {
                    BytePos bp = fallingBytes[i];
                    map[bp.X, bp.Y].Type = AStar.TileType.Wall;
                }

                AStar aStar = new AStar(map, map[PlayerPos.X, PlayerPos.Y], map[EndPos.X, EndPos.Y]);
                List<AStar.Tile> path = aStar.FindPath();
                if (path == null)
                {
                    break;
                }


                MaxFalling++;
            }

            BytePos lastChanceByte = fallingBytes[MaxFalling-1];           
            Console.WriteLine($"Last chance byte: {lastChanceByte.X}, {lastChanceByte.Y} @ {MaxFalling-1}");
            

        });

        return 0;
    }
}
