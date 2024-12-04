using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day04>(input => new Day04(input));
        await Solver.Solve(factory, Solver.Parts.BOTH);
    }
}