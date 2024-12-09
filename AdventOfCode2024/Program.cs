using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day09>(input => new Day09(input));
        await Solver.Solve(factory, Solver.Parts.BOTH);
    }
}