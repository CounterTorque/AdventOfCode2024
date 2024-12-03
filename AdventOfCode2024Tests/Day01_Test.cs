using NUnit.Framework;
using AdventOfCode2024;

namespace AdventOfCode2024Tests;

[TestFixture]
public class Day01_Test
{
    

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        string[] inputLines = {"3   4","4   5" };
        const int expected = 2;

        var Day01 = new Day01(inputLines);
        var result = await Day01.Solve_1();
        Assert.That(result, Is.EqualTo(expected));
    }
}
