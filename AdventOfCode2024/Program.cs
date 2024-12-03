using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day03>(input => new Day03(input));
        await Solver.Solve(factory, Solver.Parts.BOTH);
    }
}