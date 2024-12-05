using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day05>(input => new Day05(input));
        await Solver.Solve(factory, Solver.Parts.Part2);
    }
}