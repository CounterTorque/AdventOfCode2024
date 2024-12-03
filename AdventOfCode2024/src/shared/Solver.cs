using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode2024
{
    public static class Solver
    {
        [Flags]
        public enum Parts
        {
            None = 0,
            Part1 = 1,
            Part2 = 2,
            BOTH = Part1 | Part2
        }

        private sealed record ElapsedTime(double Constructor, double Part1, double Part2);

        public static async Task Solve<TBaseDay>(IBaseDayFactory<TBaseDay> factory, Parts parts = Parts.BOTH)
            where TBaseDay : BaseDay
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                TBaseDay baseDay = factory.CreateInstance();
                sw.Stop();

                await SolveProblem(baseDay, parts, CalculateElapsedMilliseconds(sw));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private static async Task<ElapsedTime> SolveProblem(BaseDay problem, Parts parts, double constructorElapsedTime)
        {
            var problemIndex = problem.CalculateIndex();
            var problemTitle = problemIndex != default
                ? $"Day {problemIndex}"
                : $"{problem.GetType().Name}";


            Console.WriteLine($"Day {problemIndex}, {problemTitle}, {FormatTime(constructorElapsedTime)}");

            double elapsedMillisecondsPart1 = 0;
            double elapsedMillisecondsPart2 = 0;

            if (parts.HasFlag(Parts.Part1))
            {
                (string solution1, elapsedMillisecondsPart1) = await SolvePart(isPart1: true, problem);
                Console.WriteLine($"Part 1: {solution1} ({elapsedMillisecondsPart1} ms)");
            }

            if (parts.HasFlag(Parts.Part2))
            {
                (string solution2, elapsedMillisecondsPart2) = await SolvePart(isPart1: false, problem);
                Console.WriteLine($"Part 2: {solution2} ({elapsedMillisecondsPart2} ms)");
            }

            return new ElapsedTime(constructorElapsedTime, elapsedMillisecondsPart1, elapsedMillisecondsPart2);
        }

        private static async Task<(string solution, double elapsedTime)> SolvePart(bool isPart1, BaseDay problem)
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
}