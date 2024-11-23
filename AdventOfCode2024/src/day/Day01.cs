using AdventOfCode2024;

public class Day01 : BaseDay
{
    public Day01()
    {

    }

    public override ValueTask<string> Solve_1()
    {
        string t_inputFilePath = InputFilePath(1);
        if (!File.Exists(t_inputFilePath))
        {
            throw new FileNotFoundException(t_inputFilePath);
        }

        return new("Solution 1.1");
    }

    public override ValueTask<string> Solve_2()
    {
        string t_inputFilePath = InputFilePath(2);
        if (!File.Exists(t_inputFilePath))
        {
            throw new FileNotFoundException(t_inputFilePath);
        }

        return new("Solution 1.2");
    }
}