using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day15>(input => new Day15(input));
        await Solver.Solve(factory, Solver.Parts.Part1);
    }
}