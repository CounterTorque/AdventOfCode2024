﻿using AdventOfCode2024;


internal class Program
{
    private static async Task Main(string[] args)
    {
        var factory = new BaseDayFactory<Day21>(input => new Day21(input));
        await Solver.Solve(factory, Solver.Parts.Part1);
    }
}