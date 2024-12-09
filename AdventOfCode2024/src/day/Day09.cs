using System.Diagnostics;

using AdventOfCode2024;


public class Day09 : BaseDay
{

    List<long> unpackedDisk = new List<long>();
    
    public Day09(string[]? inputLines = null) : base(inputLines)
    {
        Debug.Assert(InputLines.Length == 1);
        bool isBlock = true;
        long blockID = 0;
        foreach(char c in InputLines[0])
        {
            long blockSize = long.Parse(c.ToString());
            for(long i = 0; i < blockSize; i++)
            {
                if (isBlock)
                {
                    unpackedDisk.Add(blockID);
                }
                else
                {
                    unpackedDisk.Add(-blockSize);
                }                
            }
            if (isBlock)
            {
                blockID++;
            }
            isBlock = !isBlock;
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        long part1 = 0;
        await Task.Run(() => {

            int endIndex = unpackedDisk.Count - 1;
            int nextEmptyIndex = unpackedDisk.FindIndex(x => x < 0);
            
            while (nextEmptyIndex < endIndex)
            {
                long endValue = unpackedDisk[endIndex];
                if (endValue < 0)
                {
                    endIndex--;
                    continue;
                }

                unpackedDisk[nextEmptyIndex] = endValue;
                unpackedDisk[endIndex] = long.MinValue;
                nextEmptyIndex = unpackedDisk.FindIndex(nextEmptyIndex + 1, x => x < 0);
            }

        });

        //Calculate "checksum"
        for (int i = 0; i < unpackedDisk.Count; i++)
        {
            long value = unpackedDisk[i];
            if (value < 0)
            {
                continue;
            }

            part1 += (value * i);
        }
        
        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }


    public override async ValueTask<int> Solve_2()
    {
        long part2 = 0;
        await Task.Run(() => {

        //TODO: Unpack only if it fits, walking right to left for each file ID


        //Calculate "checksum"
        for (int i = 0; i < unpackedDisk.Count; i++)
        {
            long value = unpackedDisk[i];
            if (value < 0)
            {
                continue;
            }

            part2 += (value * i);
        }
        });

        Console.WriteLine($"Part 2: {part2}");
        return 0;
    }
}
