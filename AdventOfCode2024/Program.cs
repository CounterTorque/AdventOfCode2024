using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day16>(input => new Day16(input));
        await Solver.Solve(factory, Solver.Parts.Part1);
    }
}