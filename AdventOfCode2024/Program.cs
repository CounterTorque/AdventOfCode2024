using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day23>(input => new Day23(input));
        await Solver.Solve(factory, Solver.Parts.Part1);
    }
}