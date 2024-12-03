using AdventOfCode2024;

var factory = new BaseDayFactory<Day03>(input => new Day03(input));
await Solver.Solve<Day03>(factory);
