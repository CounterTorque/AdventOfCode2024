using System.Diagnostics;
using System.Drawing;
using AdventOfCode2024;


public class Day20 : BaseDay
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

    int xMax = 0;
    int yMax = 0;
    AStar.Tile[,] map;
    Point PlayerPos;
    Point EndPos;

    
    public Day20(string[]? inputLines = null) : base(inputLines)
    {
        int yCur = 0;
        xMax = InputLines[0].Length;
        yMax = InputLines.Length;
        map = new AStar.Tile[xMax, yMax];

        foreach (string line in InputLines)
        {
            char[] lineChars = line.ToCharArray();
            
            for (int x = 0; x < lineChars.Length; x++)
            {
                switch (lineChars[x])
                {
                    case '#':
                        map[x, yCur] = new AStar.Tile(AStar.TileType.Wall, x, yCur);
                        break;
                    case '.':
                        map[x, yCur] = new AStar.Tile(AStar.TileType.Empty, x, yCur);
                        break;
                    case 'S':
                        map[x, yCur] = new AStar.Tile(AStar.TileType.Start, x, yCur);
                        PlayerPos = new Point(x, yCur);
                        break;
                    case 'E':
                        map[x, yCur] = new AStar.Tile(AStar.TileType.End, x, yCur);
                        EndPos = new Point(x, yCur);
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
            //Run the AStar once. 
            //Then look at all the neighbors along the path and collect any that are walls. 
            //Then for each wall, run the AStar again. Collecting the new paths. 
            //For all of those paths, if they are short by at least [100] units, add one to Part1;

            AStar aStar = new AStar(map, map[PlayerPos.X, PlayerPos.Y], map[EndPos.X, EndPos.Y]);
            List<AStar.Tile> pathOriginal = aStar.FindPath();

            HashSet<Point> wallPoints = new HashSet<Point>();

            foreach (AStar.Tile tile in pathOriginal)
            {
                int xTile = tile.X;
                int yTile = tile.Y;

                if (xTile > 1 && map[xTile - 1, yTile].Type == AStar.TileType.Wall)
                {
                    wallPoints.Add(new Point(xTile - 1, yTile));
                }

                if (xTile < xMax - 2 && map[xTile + 1, yTile].Type == AStar.TileType.Wall)
                {
                    wallPoints.Add(new Point(xTile + 1, yTile));
                }

                if (yTile > 1 && map[xTile, yTile - 1].Type == AStar.TileType.Wall)
                {
                    wallPoints.Add(new Point(xTile, yTile - 1));
                }

                if (yTile < yMax - 2 && map[xTile, yTile + 1].Type == AStar.TileType.Wall)
                {
                    wallPoints.Add(new Point(xTile, yTile + 1));
                }
            }
            //NOTE: We could probably trim this list down as some of those walls will not lead to paths on the other side. 


            for (int i = 0; i < wallPoints.Count; i++)
            {
                Point wallPoint = wallPoints.ElementAt(i);
                Console.WriteLine($"Removeing Wall Number {i} / {wallPoints.Count} at {wallPoint.X}, {wallPoint.Y}");
                AStar.Tile wallTile = map[wallPoint.X, wallPoint.Y];
                Debug.Assert(wallTile.Type == AStar.TileType.Wall);
                wallTile.Type = AStar.TileType.Empty;

                AStar aStarCheat = new AStar(map, map[PlayerPos.X, PlayerPos.Y], map[EndPos.X, EndPos.Y]);
                List<AStar.Tile> cheatPath = aStarCheat.FindPath();

                int cheatSavings = pathOriginal.Count - cheatPath.Count;
                if (cheatSavings >= 100)
                {
                    part1++;
                }

                wallTile.Type = AStar.TileType.Wall;
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
