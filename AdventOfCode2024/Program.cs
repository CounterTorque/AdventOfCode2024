using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day13>(input => new Day13(input));
        await Solver.Solve(factory, Solver.Parts.BOTH);
    }
}