using System.Diagnostics;

using AdventOfCode2024;


public class Day04 : BaseDay
{
    readonly char[] matchKey = ['X', 'M', 'A', 'S'];
    
    public Day04(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        
        int rowSize = InputLines[0].Length;
        int colSize = InputLines.Length;
        char[][] puzzleBody = InputLines.Select(x => x.ToCharArray()).ToArray();

        await Task.Run(async () => {
            
            for (int y = 0; y < colSize; y++)
            {
                for (int x = 0; x < rowSize; x++)
                {
                    if (puzzleBody[x][y] != matchKey[0])
                    {
                        continue;
                    } 

                    int numMatches = 0;
                    numMatches += await CheckDirection(puzzleBody, x, y, -1, -1, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y,  0, -1, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y,  1, -1, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y, -1,  0, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y,  1,  0, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y, -1,  1, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y,  0,  1, 1);
                    numMatches += await CheckDirection(puzzleBody, x, y,  1,  1, 1);

                    part1 += numMatches;
                }
            }

        });
        
        return part1;
    }

    private async Task<int> CheckDirection(char[][] puzzleBody, int x, int y, int dx, int dy, int dKey = 1)
    {
        int tx = x + dx;
        int ty = y + dy;
        if (tx < 0 || ty < 0 || tx >= puzzleBody[0].Length || ty >= puzzleBody.Length)
        {
            return 0;
        }

        if (puzzleBody[tx][ty] != matchKey[dKey])
        {
            return 0;
        }

        if (dKey == matchKey.Length - 1)
        {
            return 1;
        }

        int numMatches = await CheckDirection(puzzleBody, tx, ty, dx, dy, dKey + 1);
        return numMatches;        
    }

    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() => {

        });

        return part2;
    }
}
