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
        var Day01 = new Day01();
        await Day01.Solve_1();
        Assert.Pass();
    }
}
