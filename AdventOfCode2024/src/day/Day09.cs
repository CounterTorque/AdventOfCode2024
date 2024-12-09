using System.Diagnostics;

using AdventOfCode2024;


public class Day09 : BaseDay
{

    public Day09(string[]? inputLines = null) : base(inputLines)
    {
       
    }

    public override async ValueTask<int> Solve_1()
    {
        List<int> unpackedDisk = new List<int>();
        Debug.Assert(InputLines.Length == 1);
        bool isBlock = true;
        int blockID = 0;
        foreach(char c in InputLines[0])
        {
            int blockSize = int.Parse(c.ToString());
            for(long i = 0; i < blockSize; i++)
            {
                if (isBlock)
                {
                    unpackedDisk.Add(blockID);
                }
                else
                {
                    unpackedDisk.Add(int.MinValue);
                }                
            }
            if (isBlock)
            {
                blockID++;
            }
            isBlock = !isBlock;
        }

        long part1 = 0;
        await Task.Run(() =>
        {

            int endIndex = unpackedDisk.Count - 1;
            int nextEmptyIndex = unpackedDisk.FindIndex(x => x == int.MinValue);

            while (nextEmptyIndex < endIndex)
            {
                int endValue = unpackedDisk[endIndex];
                if (endValue == int.MinValue)
                {
                    endIndex--;
                    continue;
                }

                unpackedDisk[nextEmptyIndex] = endValue;
                unpackedDisk[endIndex] = int.MinValue;
                nextEmptyIndex = unpackedDisk.FindIndex(nextEmptyIndex + 1, x => x == int.MinValue);
            }

        });
        part1 = CheckSum(unpackedDisk);

        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }

    private long CheckSum(List<int> unpackedDisk)
    {
        long checkSum = 0;
        //Calculate "checksum"
        for (int i = 0; i < unpackedDisk.Count; i++)
        {
            long value = unpackedDisk[i];
            if (value == int.MinValue)
            {
                continue;
            }

            checkSum += (value * i);
        }

        return checkSum;
    }

    public override async ValueTask<int> Solve_2()
    {
        List<int> unpackedDisk = new List<int>();
        Debug.Assert(InputLines.Length == 1);
        bool isBlock = true;
        int blockID = 0;
        foreach(char c in InputLines[0])
        {
            int blockSize = int.Parse(c.ToString());
            for(long i = 0; i < blockSize; i++)
            {
                if (isBlock)
                {
                    unpackedDisk.Add(blockID);
                }
                else
                {
                    unpackedDisk.Add(int.MinValue);
                }                
            }
            if (isBlock)
            {
                blockID++;
            }
            isBlock = !isBlock;
        }


        long part2 = 0;
        await Task.Run(() => {

            int endIndex = unpackedDisk.Count - 1;
            int FirstEmptyIndex = unpackedDisk.FindIndex(x => x == int.MinValue);
            
            while (FirstEmptyIndex < endIndex)
            {
                int endValue = unpackedDisk[endIndex];
                if (endValue == int.MinValue)
                {
                    endIndex--;
                    continue;
                }
                int endLength = 0;
                for (int i = endIndex; i >= FirstEmptyIndex; i--)
                {
                    if (unpackedDisk[i] == endValue)
                    {
                        endLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                //Find the next empty that is large enough.
                int nextEmptyIndex = FirstEmptyIndex;
                while (nextEmptyIndex < endIndex)
                {
                    int nextPlusEmptyIndex = unpackedDisk.FindIndex(nextEmptyIndex, x => x != int.MinValue);
                    int emptyLength = nextPlusEmptyIndex - nextEmptyIndex;
                    if (endLength <= emptyLength)
                    {
                        for (int eli = 0; eli < endLength; eli++)
                        {
                            unpackedDisk[nextEmptyIndex + eli] = endValue;
                            unpackedDisk[endIndex - eli] = int.MinValue;
                        }
                        break;
                    }
                    nextEmptyIndex = unpackedDisk.FindIndex(nextEmptyIndex + emptyLength, x => x == int.MinValue);
                }

                endIndex -= endLength;
                FirstEmptyIndex = unpackedDisk.FindIndex(x => x == int.MinValue);
            }

            part2 = CheckSum(unpackedDisk);
        });

        Console.WriteLine($"Part 2: {part2}");
        return 0;
    }
}
