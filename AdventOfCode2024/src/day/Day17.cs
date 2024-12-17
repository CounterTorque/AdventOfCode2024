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

    int rA = 47792830;
    int rB = 0;
    int rC = 0;

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
        for (int i = 0; i < line.Length; i=i+4)
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
        int instructionPointer = 0;

        await Task.Run(() =>
        {
            while (instructionPointer < Instructions.Count)
            {
                Instruction instruction = Instructions[instructionPointer];
                
                switch (instruction.Type)
                {
                    case InstructionType.Iadv:
                        {
                            int instVal = GetInstructionValue(instruction);
                            double val = rA / Math.Pow(2, instVal);
                            rA = (int)val;
                            instructionPointer++;
                        }
                        break;
                    case InstructionType.Ibxl:
                        {
                            int val = rB ^ instruction.Value;
                            rB = val;
                            instructionPointer++;
                        }
                        break;
                    case InstructionType.Ibst:
                        {
                            int instVal = GetInstructionValue(instruction);
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
                            int instVal = GetInstructionValue(instruction);
                            int val = instVal % 8;
                            Console.Write($"{val},");
                            instructionPointer++;
                        }
                        break;
                    case InstructionType.Ibdv:
                        {
                            int instVal = GetInstructionValue(instruction);
                            double val = rA / Math.Pow(2, instVal);
                            rB = (int)val;
                            instructionPointer++;
                        }
                        break;
                    case InstructionType.Icdv:
                        {
                            int instVal = GetInstructionValue(instruction);
                            double val = rA / Math.Pow(2, instVal);
                            rC = (int)val;
                            instructionPointer++;
                        }
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }

        });

        return part1;
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

        });

        return part2;
    }

    int GetInstructionValue(Instruction instruction)
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
