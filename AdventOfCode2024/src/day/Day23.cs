using System.Diagnostics;
using QuikGraph;

using AdventOfCode2024;


public class Day23 : BaseDay
{

    Dictionary<string, List<string>> ComputerConnections = new Dictionary<string, List<string>>();

    public Day23(string[]? inputLines = null) : base(inputLines)
    {
        foreach (string line in InputLines)
        {
            string[] pcNames = line.Split("-");
            Debug.Assert(pcNames.Length == 2);

            if (!ComputerConnections.ContainsKey(pcNames[0]))
            {
                ComputerConnections.Add(pcNames[0], new List<string>());
            }
            ComputerConnections[pcNames[0]].Add(pcNames[1]);

            if (!ComputerConnections.ContainsKey(pcNames[1]))
            {
                ComputerConnections.Add(pcNames[1], new List<string>());
            }

            ComputerConnections[pcNames[1]].Add(pcNames[0]);

        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {
            var graph = new AdjacencyGraph<string, Edge<string>>();
            graph.AddVertexRange(ComputerConnections.Keys);
            graph.AddEdgeRange(ComputerConnections.SelectMany(x => x.Value.Select(y => new Edge<string>(x.Key, y))));


            HashSet<(string, string, string)> triangles = new HashSet<(string, string, string)>();

            foreach (string vertex in graph.Vertices)
            {

                var outEdges = graph.OutEdges(vertex).ToList();
                for (int i = 0; i < outEdges.Count; i++)
                {
                    for (int j = i + 1; j < outEdges.Count; j++)
                    {
                        Edge<string> edge1 = outEdges[i];
                        Edge<string> edge2 = outEdges[j];

                        var outEdges1 = graph.OutEdges(edge1.Target).Select(x => x.Target).ToList();
                        if (outEdges1.Contains(edge2.Target))
                        {
                            List<string> triVerts = new List<string> { vertex, edge1.Target, edge2.Target };
                            triVerts.Sort();
                            triangles.Add((triVerts[0], triVerts[1], triVerts[2]));
                        }

                    }
                }
            }

            foreach ((string v1, string v2, string v3) in triangles)
            {
                if (v1.StartsWith('t') || v2.StartsWith('t') || v3.StartsWith('t'))
                {
                    part1++;
                }
            }

        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);

        var adjgraph = new AdjacencyGraph<string, Edge<string>>();
        adjgraph.AddVertexRange(ComputerConnections.Keys);
        adjgraph.AddEdgeRange(ComputerConnections.SelectMany(x => x.Value.Select(y => new Edge<string>(x.Key, y))));


        var graph = new UndirectedGraph<string, UndirectedEdge<string>>();
        graph.AddVertexRange(adjgraph.Vertices);
        var seenEdges = new HashSet<UndirectedEdge<string>>();
        foreach (var edge in adjgraph.Edges)
        {
            // Sort vertices to ensure consistent undirected edge representation
            var source = string.Compare(edge.Source, edge.Target, StringComparison.Ordinal) <= 0
                ? edge.Source
                : edge.Target;

            var target = string.Compare(edge.Source, edge.Target, StringComparison.Ordinal) <= 0
                ? edge.Target
                : edge.Source;

            var undirectedEdge = new UndirectedEdge<string>(source, target);

            if (seenEdges.Add(undirectedEdge)) // Add only if it’s not a duplicate
            {
                graph.AddEdge(undirectedEdge);
            }
        }
        
        

        await Task.Run(() =>
        {
            var largestClique = new List<string>();
            BronKerbosch(new List<string>(), new HashSet<string>(graph.Vertices), new HashSet<string>(), graph, ref largestClique);
        
            largestClique.Sort();
            foreach(string vertex in largestClique)
            {
                Console.Write($"{vertex},");
            }
            Console.WriteLine();
            

        });

        return part2;
    }

    static void BronKerbosch(List<string> R, 
                            HashSet<string> P,
                            HashSet<string> X,
                            UndirectedGraph<string, UndirectedEdge<string>> graph,
                             ref List<string> largestClique)
    {
        if (P.Count == 0 && X.Count == 0)
        {
            // Found a maximal clique
            if (R.Count > largestClique.Count)
            {
                largestClique = new List<string>(R);
            }
            return;
        }

        var pivot = ChoosePivot(P, X, graph);
        var neighborsOfPivot = new HashSet<string>(graph.AdjacentVertices(pivot));

        foreach (var v in new HashSet<string>(P.Except(neighborsOfPivot)))
        {
            R.Add(v);
            BronKerbosch(R, new HashSet<string>(P.Intersect(graph.AdjacentVertices(v))),
                         new HashSet<string>(X.Intersect(graph.AdjacentVertices(v))),
                         graph, ref largestClique);
            R.Remove(v);
            P.Remove(v);
            X.Add(v);
        }
    }

    static string ChoosePivot(HashSet<string> P, HashSet<string> X, UndirectedGraph<string, UndirectedEdge<string>> graph)
    {
        // Simplistic pivot choice: return the first vertex from P ∪ X
        foreach (var vertex in P)
            return vertex;

        foreach (var vertex in X)
            return vertex;

        throw new InvalidOperationException("Pivot selection failed.");
    }
}
