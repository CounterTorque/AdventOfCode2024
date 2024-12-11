using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day11 : BaseDay
{
    List<long> stones = new List<long>();

    public Day11(string[]? inputLines = null) : base(inputLines)
    {
        Debug.Assert(InputLines.Length == 1);
        string[] nums = Regex.Split(InputLines[0], @"\s+");
        foreach (string num in nums)
        {
            stones.Add(int.Parse(num));
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int iterations = 0;
        int part1 = 0;

        await Task.Run(() =>
        {

            while (true)
            {
                List<long> nextStones = new List<long>();
                for (int i = 0; i < stones.Count; i++)
                {
                    long stone = stones[i];
                    if (stone == 0)
                    {
                        nextStones.Add(1);
                        continue;
                    }
                    string numStr = stone.ToString();
                    int digits = numStr.Length;
                    bool isEven = digits % 2 == 0;
                    if (isEven)
                    {
                        string lefthalf = numStr.Substring(0, digits / 2);
                        string righthalf = numStr.Substring(digits / 2, digits / 2);
                        long left = long.Parse(lefthalf);
                        long right = long.Parse(righthalf);
                        nextStones.Add(left);
                        nextStones.Add(right);
                        continue;
                    }

                    long newNum = stone * 2024;
                    nextStones.Add(newNum);
                }
                stones = nextStones;
                iterations++;
                if (iterations == 25)
                {
                    break;
                }
            }

            part1 = stones.Count;


        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int iterations = 0;
        int part2 = 0;
        const int chunkSize = 100000;

        await Task.Run(() =>
        {
            Queue<List<long>> chunks = new Queue<List<long>>();
            chunks.Enqueue(stones);

            while (iterations < 75)
            {
                int currentChunkCount = chunks.Count;

                for (int chunkIndex = 0; chunkIndex < currentChunkCount; chunkIndex++)
                {
                    List<long> currentChunk = chunks.Dequeue();
                    List<long> nextChunk = new List<long>(chunkSize);

                    foreach (var stone in currentChunk)
                    {
                        if (stone == 0)
                        {
                            nextChunk.Add(1);
                            continue;
                        }

                        string numStr = stone.ToString();
                        int digits = numStr.Length;
                        bool isEven = digits % 2 == 0;

                        if (isEven)
                        {
                            string lefthalf = numStr.Substring(0, digits / 2);
                            string righthalf = numStr.Substring(digits / 2, digits / 2);
                            long left = long.Parse(lefthalf);
                            long right = long.Parse(righthalf);
                            nextChunk.Add(left);
                            nextChunk.Add(right);
                        }
                        else
                        {
                            long newNum = stone * 2024;
                            nextChunk.Add(newNum);
                        }

                        // When a chunk reaches its size limit, enqueue it and start a new one
                        if (nextChunk.Count >= chunkSize)
                        {
                            chunks.Enqueue(nextChunk);
                            nextChunk = new List<long>(chunkSize);
                        }
                    }

                    // Enqueue the remaining items in the current chunk
                    if (nextChunk.Count > 0)
                    {
                        chunks.Enqueue(nextChunk);
                    }
                }

                iterations++;
                Console.WriteLine($"Iteration {iterations}: Total Chunks: {chunks.Count}");
            }

            // Count all elements across all chunks
            part2 = chunks.Sum(chunk => chunk.Count);
        });

        return part2;
    }
}
