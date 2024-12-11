using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day11>(input => new Day11(input));
        await Solver.Solve(factory, Solver.Parts.BOTH);
    }
}