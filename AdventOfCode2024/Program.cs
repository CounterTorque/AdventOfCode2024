using AdventOfCode2024;

var factory = new BaseDayFactory<Day02>(input => new Day02(input));
await Solver.Solve<Day02>(factory);
