using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode2024;

public static class Solver
{
    private static readonly bool IsInteractiveEnvironment = Environment.UserInteractive && !Console.IsOutputRedirected;

    private sealed record ElapsedTime(double Constructor, double Part1, double Part2);


    public static async Task SolveLast()
    {
        var lastProblem = LoadAllProblems().LastOrDefault();
        if (lastProblem is not null)
        {
            var sw = new Stopwatch();
            sw.Start();
            var potentialProblem = InstantiateProblem(lastProblem);
            sw.Stop();

            if (potentialProblem is BaseProblem problem)
            {
                await SolveProblem(problem, CalculateElapsedMilliseconds(sw));
            }

        }

    }


    public static async Task Solve<TProblem>()
        where TProblem : BaseProblem, new()
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            TProblem problem = new();
            sw.Stop();

            await SolveProblem(problem, CalculateElapsedMilliseconds(sw));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    internal static IEnumerable<Type> LoadAllProblems()
    {
        List<Assembly> assemblies = [Assembly.GetEntryAssembly()!];
        return assemblies.SelectMany(a => a.GetTypes())
            .Where(type => typeof(BaseProblem).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .OrderBy(t => t.FullName);
    }


    private static object? InstantiateProblem(Type problemType)
    {
        try
        {
            return Activator.CreateInstance(problemType);
        }
        catch (Exception e)
        {
            return e.InnerException?.Message + Environment.NewLine + e.InnerException?.StackTrace;
        }
    }

    private static async Task<ElapsedTime> SolveProblem(BaseProblem problem, double constructorElapsedTime)
    {
        var problemIndex = problem.CalculateIndex();
        var problemTitle = problemIndex != default
            ? $"Day {problemIndex}"
            : $"{problem.GetType().Name}";


        Console.WriteLine($"Day {problemIndex}, {problemTitle}, {FormatTime(constructorElapsedTime)}");

        (string solution1, double elapsedMillisecondsPart1) = await SolvePart(isPart1: true, problem);
        Console.WriteLine($"Part 1: {solution1} ({elapsedMillisecondsPart1} ms)");

        (string solution2, double elapsedMillisecondsPart2) = await SolvePart(isPart1: false, problem);
        Console.WriteLine($"Part 2: {solution2} ({elapsedMillisecondsPart2} ms)");


        return new ElapsedTime(constructorElapsedTime, elapsedMillisecondsPart1, elapsedMillisecondsPart2);
    }

    private static async Task<(string solution, double elapsedTime)> SolvePart(bool isPart1, BaseProblem problem)
    {
        Stopwatch stopwatch = new();
        var solution = string.Empty;

        try
        {
            Func<ValueTask<string>> solve = isPart1
                ? problem.Solve_1
                : problem.Solve_2;

            stopwatch.Start();
            solution = await solve();
        }
        catch (NotImplementedException)
        {
            solution = "[[Not implemented]]";
        }
        catch (Exception e)
        {
            solution = e.Message + Environment.NewLine + e.StackTrace;
        }
        finally
        {
            stopwatch.Stop();
        }

        var elapsedMilliseconds = CalculateElapsedMilliseconds(stopwatch);

        return (solution, elapsedMilliseconds);
    }

    private static double CalculateElapsedMilliseconds(Stopwatch stopwatch)
    {
        return 1000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
    }

    private static string FormatTime(double elapsedMilliseconds)
    {
        var message = elapsedMilliseconds switch
        {
            < 1 => $"{elapsedMilliseconds:F} ms",
            < 1_000 => $"{Math.Round(elapsedMilliseconds)} ms",
            < 60_000 => $"{0.001 * elapsedMilliseconds:F} s",
            _ => $"{Math.Floor(elapsedMilliseconds / 60_000)} min {Math.Round(0.001 * (elapsedMilliseconds % 60_000))} s",
        };

        return message;
    }

}