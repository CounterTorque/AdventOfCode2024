using System.Diagnostics;

using AdventOfCode2024;


public class Day09 : BaseDay
{

    List<int> unpackedDisk = new List<int>();
    
    public Day09(string[]? inputLines = null) : base(inputLines)
    {
        Debug.Assert(InputLines.Length == 1);
        bool isBlock = true;
        int blockID = 0;
        foreach(char c in InputLines[0])
        {
            int blockSize = int.Parse(c.ToString());
            for(int i = 0; i < blockSize; i++)
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
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        await Task.Run(() => {
            //Taking an index from the end
            //Taking an index from the left most int.MinValue
            //Work both towards the middle
            //Replacing the int.MinValue with the value of the index, if it is not int.MinValue

            int endIndex = unpackedDisk.Count - 1;
            int nextEmptyIndex = unpackedDisk.IndexOf(int.MinValue);
            
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
                nextEmptyIndex = unpackedDisk.IndexOf(int.MinValue, nextEmptyIndex);
            }

        });

        //Calculate "checksum"
        for (int i = 0; i < unpackedDisk.Count; i++)
        {
            int value = unpackedDisk[i];
            if (value == int.MinValue)
            {
                break;
            }

            part1 += (value * i);
        }
        
        // 554328789 TO LOW
        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        await Task.Run(() => {

        });

        return part2;
    }
}
