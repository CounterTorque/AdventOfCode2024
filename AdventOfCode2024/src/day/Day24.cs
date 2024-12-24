using System.Diagnostics;

using AdventOfCode2024;


public class Day24 : BaseDay
{
    enum LogicGate { AND, OR, XOR };

    struct WireLogic
    {
        public LogicGate Gate;
        public string Input1;
        public string Input2;
        public string Output;
    }

    Dictionary<string, bool> WireStates = new Dictionary<string, bool>();
    List<WireLogic> WireLogicSets = new List<WireLogic>();

    
    public Day24(string[]? inputLines = null) : base(inputLines)
    {
        bool IsParseInputs = true;

        foreach(string line in InputLines)
        {
            if (line == "")
            {
                IsParseInputs = false;
                continue;
            }

            if (IsParseInputs)
            {
                //Name will be 3 letters, : , then a number 1 or 0 for state
                string[] splitLine = line.Split(':');
                string name = splitLine[0];
                bool state = splitLine[1].Trim() == "1";
                WireStates.Add(name, state);
            }
            else
            {
                string[] splitLine = line.Split(' ');
                WireLogic logic = new WireLogic();
                logic.Input1 = splitLine[0];
                logic.Gate = (LogicGate)Enum.Parse(typeof(LogicGate), splitLine[1], true);
                logic.Input2 = splitLine[2];
                //Skip the -> in 3
                logic.Output = splitLine[4];
                WireLogicSets.Add(logic);
            }
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        ulong part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        LinkedList<WireLogic> RemainingLogic = new LinkedList<WireLogic>(WireLogicSets.Select(x => x));
        
        await Task.Run(() => {

            int nextIndex = 0;
            while (RemainingLogic.Count > 0)
            {
                WireLogic current = RemainingLogic.ElementAt(nextIndex);
                //Check that both the inputs are already set in the dictionary
                if (WireStates.ContainsKey(current.Input1) && WireStates.ContainsKey(current.Input2))
                {
                    bool input1 = WireStates[current.Input1];
                    bool input2 = WireStates[current.Input2];
                    bool result = false;
                    switch (current.Gate)
                    {
                        case LogicGate.AND:
                            result = input1 && input2;
                            break;
                        case LogicGate.OR:
                            result = input1 || input2;
                            break;
                        case LogicGate.XOR:
                            result = input1 ^ input2;
                            break;
                    }
                    WireStates[current.Output] = result;
                    RemainingLogic.Remove(current);
                    nextIndex = 0;
                    continue;
                }

                nextIndex = (nextIndex + 1) % WireLogicSets.Count;                
            }

            //Now we need to pull out all the dictionary values that start with z
            //Then sort them
            List<string> zKeys = WireStates.Keys.Where(x => x.StartsWith("z")).ToList();
            zKeys.Sort();

            int shift = 0;
            foreach (string key in zKeys)
            {
                ulong keyBit = WireStates[key] ? (ulong)1 : (ulong)0;
                part1 = part1 | (keyBit << shift);
                shift += 1;
            }

        });
        
        Console.WriteLine($"Part 1: {part1}");
        return 0;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {
            //Collect all x, y, and z wires into their own lists (the x and y are always defined by the input)
            //setting all values to 0
            //Walk through each pair of X and Y and Set them to 1, then check the Z.
            //If the Z is 0, then find what wire is set to 1 (if any....)
            //If you find one that's 1, then this is one to swap likely. 
            // SUB NOTE: This seems too easy. There are likely situations where there is a 0 because of the other logic involed.
            //           If so, you'd have to dig deeper into the logic to find what does become a 1. And you might not simply swap z's out. 
            //           And making a swap might destroy an already correct wiring.
            //Once you find a 1, then set it to 0 and try again.
            //Swap the outputs, (Mark them as already swapped) and try again.
            //Do this until all bits correctly are set from their x AND y = z
            //Should be 8 total (4 swaps)
            //Sort those names alphabetically, and return comma separated list
        });

        return part2;
    }
}
