using System.Diagnostics;

using AdventOfCode2024;


public class Day17 : BaseDay
{
    enum InstructionType
    {
        Iadv = 0,
        Ibxl = 1,
        Ibst = 2,
        Ijnz = 3,
        Ibxc = 4,
        Iout = 5,
        Ibdv = 6,
        Icdv = 7
    }

    long rA = 47792830;
    long rB = 0;
    long rC = 0;

    struct Instruction
    {
        public InstructionType Type;
        public int Value;


    }
    List<Instruction> Instructions = new List<Instruction>();

    public Day17(string[]? inputLines = null) : base(inputLines)
    {
        Debug.Assert(InputLines.Length == 1);
        char[] line = InputLines[0].ToCharArray();
        for (int i = 0; i < line.Length; i = i + 4)
        {
            int pI = int.Parse(line[i].ToString());
            int pV = int.Parse(line[i + 2].ToString());

            Instruction instruction = new Instruction();
            instruction.Type = (InstructionType)pI;
            instruction.Value = pV;

            Instructions.Add(instruction);
        }
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        string output = "";

        await Task.Run(() =>
        {
            //SAMPLE 1
            output = ExecuteInstructions(2024, 0, 0);

            //PUZZLE INPUT
            //string output = ExecuteInstructions(47792830, 0, 0);

        });

        Console.WriteLine(output);
        return part1;
    }

    public override async ValueTask<int> Solve_2()
    {
        //string target = "0,3,5,4,3,0,"; //TEST VALUE
        string target = "2,4,1,5,7,5,1,6,4,3,5,5,0,3,3,0,"; //PUZZLE INPUT
        List<long> targetSeq = target.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x)).ToList();
            

        await Task.Run(() =>
        {
            long rACheck = 100001900000000; //Last Known Tested
          //long rACheck = 281474976710656; //Has to be less than this, right?? 2^48 (3bits*16insts)
            while (true)
            {
                string output = ExecuteInstructions(rACheck, 0, 0, targetSeq);
                if (rACheck % 10000000 == 0)
                {
                    Console.WriteLine($"{rACheck}: {output}");
                }
                
                if (output.Equals(target))
                {
                    Console.WriteLine($"FINAL: {rACheck}: {output}");
                    break;
                }
                
                rACheck++;                
            }
            

        });

        //100000000000000 TOLOW
        return 0;
    }

    private string ExecuteInstructions(long rASet, long rBSet, long rCSet, List<long> targetSeq = null)
    {
        string output = "";
        int outputIndex = 0;

        rA = rASet;
        rB = rBSet;
        rC = rCSet;
        int instructionPointer = 0;
        while (instructionPointer < Instructions.Count)
        {
            Instruction instruction = Instructions[instructionPointer];

            switch (instruction.Type)
            {
                case InstructionType.Iadv:
                    {
                        long instVal = GetInstructionValue(instruction);
                        double val = rA / Math.Pow(2, instVal);
                        rA = (long)val;
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Ibxl:
                    {
                        long val = rB ^ instruction.Value;
                        rB = val;
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Ibst:
                    {
                        long instVal = GetInstructionValue(instruction);
                        rB = instVal % 8;
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Ijnz:
                    {
                        if (rA != 0)
                        {
                            instructionPointer = instruction.Value;
                            //NOTE: This may cause a jump that mixes the values and ops. Check for this if it's not working.
                        }
                        else
                        {
                            instructionPointer++;
                        }
                    }
                    break;
                case InstructionType.Ibxc:
                    {
                        rB = rB ^ rC;
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Iout:
                    {
                        long instVal = GetInstructionValue(instruction);
                        long val = instVal % 8;
                        if (targetSeq != null)
                        {
                            if (val != targetSeq[outputIndex])
                            {
                                return output;
                            }
                            outputIndex++;
                        }
                        output += $"{val},";
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Ibdv:
                    {
                        long instVal = GetInstructionValue(instruction);
                        double val = rA / Math.Pow(2, instVal);
                        rB = (long)val;
                        instructionPointer++;
                    }
                    break;
                case InstructionType.Icdv:
                    {
                        long instVal = GetInstructionValue(instruction);
                        double val = rA / Math.Pow(2, instVal);
                        rC = (long)val;
                        instructionPointer++;
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        return output;
    }

    long GetInstructionValue(Instruction instruction)
    {
        if (instruction.Value <= 3)
            return instruction.Value;

        if (instruction.Value == 4)
            return rA;

        if (instruction.Value == 5)
            return rB;

        if (instruction.Value == 6)
            return rC;

        if (instruction.Value == 7)
            return -9999;

        Debug.Assert(false);
        return 0;
    }
}
