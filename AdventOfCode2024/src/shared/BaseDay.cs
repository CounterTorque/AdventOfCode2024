namespace AdventOfCode2024
{

    public abstract class BaseDay : BaseProblem
    {
        protected BaseDay(string[]? inputLines = null)
        {
            InputLines = inputLines;
            if (InputLines == null)
            {
                string inputFilePath = InputFilePath();
                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException(inputFilePath);
                }
                InputLines = File.ReadAllLines(inputFilePath);
            }
        }

        protected string[] InputLines { get; }
        protected override string ClassPrefix { get; } = "Day";
    }
}