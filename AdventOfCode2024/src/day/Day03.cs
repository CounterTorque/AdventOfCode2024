using System.Diagnostics;
using System.Text.RegularExpressions;
using AdventOfCode2024;


public class Day03 : BaseDay
{

    public Day03(string[]? inputLines = null) : base(inputLines)
    {
    }

    /// </summary>
    /// <returns>The sum of all of the multiplication operations in the assembly code.</returns>
    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        string pattern = "mul\\([0-9]+,[0-9]+\\)";
        await Task.Run(() =>
        {
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, pattern);

                for (int i = 0; i < matches.Count; i++)
                {
                    string currentMul = matches[i].Value;
                    string[] nums = Regex.Matches(currentMul, @"[0-9]+").Select(x => x.Value).ToArray();
                    Debug.Assert(nums.Length == 2);
                    part1 += int.Parse(nums[0]) * int.Parse(nums[1]);
                }
            }
        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        string[] lines = InputLines;
        Debug.Assert(lines.Length != 0);
        string mulPattern = "mul\\([0-9]+,[0-9]+\\)";
        string doPattern = "do\\(\\)";
        string dontPattern = "don't\\(\\)";
        await Task.Run(() =>
        {

            string totalLine = string.Concat(lines);

            MatchCollection mulMatches = Regex.Matches(totalLine, mulPattern);
            MatchCollection doMatches = Regex.Matches(totalLine, doPattern);
            MatchCollection dontMatches = Regex.Matches(totalLine, dontPattern);

            foreach (Match mulMatch in mulMatches)
            {
                int closestDo = doMatches
                                .Where(x => x.Index < mulMatch.Index)
                                .Select(x => x.Index)
                                .DefaultIfEmpty(0)
                                .Max();

                int closestDont = dontMatches
                                .Where(x => x.Index < mulMatch.Index)
                                .Select(x => x.Index)
                                .DefaultIfEmpty(int.MinValue)
                                .Max();

                if (closestDo > closestDont)
                {
                    string currentMul = mulMatch.Value;
                    string[] nums = Regex.Matches(currentMul, @"[0-9]+").Select(x => x.Value).ToArray();
                    Debug.Assert(nums.Length == 2);
                    part2 += int.Parse(nums[0]) * int.Parse(nums[1]);
                }
            }
        });

        return part2;
    }
}
