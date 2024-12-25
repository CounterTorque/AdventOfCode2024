using System.Diagnostics;

using AdventOfCode2024;


public class Day25 : BaseDay
{
    struct ChannelHeight
    {
        public int[] Heights;

        public ChannelHeight()
        {
            Heights = new int[5];
        }
    }

    List<ChannelHeight> KeyLayouts = new List<ChannelHeight>();
    List<ChannelHeight> LockLayouts = new List<ChannelHeight>();

    const int MaxHeight = 6;
    
    public Day25(string[]? inputLines = null) : base(inputLines)
    {
        const string keyStart = ".....";

        for (int i = 0; i < InputLines.Length; i++)
        {            
            string line = InputLines[i];
            bool isKey = line == keyStart;
        
            ChannelHeight heightParse = new ChannelHeight();
            i++;
            for (int j = i; j < MaxHeight; j++)
            {
                line = InputLines[j];
                for (int k = 0; k < line.Length; k++)
                {
                    heightParse.Heights[k] += line[k] == '#' ? 1 : 0;
                }
            }
            i += MaxHeight;

            if (isKey)
            {
                KeyLayouts.Add(heightParse);
            }
            else
            {
                LockLayouts.Add(heightParse);
            }         

        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

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
