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
