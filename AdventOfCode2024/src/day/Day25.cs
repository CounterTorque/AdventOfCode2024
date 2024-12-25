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
            for (int j = 0; j < MaxHeight-1; j++)
            {
                line = InputLines[j+i];
                for (int k = 0; k < line.Length; k++)
                {
                    char c = line[k];
                    heightParse.Heights[k] += c == '#' ? 1 : 0;
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
            //I think we can convert these to bits. Then if we AND them together, they should equal all 1's or something.....
            //Run Brute force for now.

            for (int l = 0; l < LockLayouts.Count; l++)
            {
                ChannelHeight lockLayout = LockLayouts[l];

                for (int k = 0; k < KeyLayouts.Count; k++)    
                {
                    ChannelHeight keyLayout = KeyLayouts[k];                
             
                    bool isMatch = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (keyLayout.Heights[i] + lockLayout.Heights[i] >= MaxHeight)
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        part1 += 1;
                        //break;
                    }
                }
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
