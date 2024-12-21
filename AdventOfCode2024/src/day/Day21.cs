using System.Diagnostics;

using AdventOfCode2024;


public class Day21 : BaseDay
{
    struct KeyButton
    {
        public char Key;
        public Dictionary<char, string> PathToButton;
    }

    Dictionary<char, KeyButton> KeyButtons = new Dictionary<char, KeyButton>
    {
       {'A', new KeyButton { Key = 'A', PathToButton = new Dictionary<char, string> {  {'A', "" },
                                                                                      {'0', "<" },
                                                                                      {'1', "<^<" },
                                                                                      {'2', "<^" },
                                                                                      {'3', "^" },
                                                                                      {'4', "<^<^" },
                                                                                      {'5', "<^^" },
                                                                                      {'6', "^^" },
                                                                                      {'7', "<^<^^" },
                                                                                      {'8', "<^^^" },
                                                                                      {'9', "^^^" }}}},
        {'0', new KeyButton { Key = '0', PathToButton = new Dictionary<char, string> {  {'A', ">" },
                                                                                      {'0', "" },
                                                                                      {'1', "^<" },
                                                                                      {'2', "^" },
                                                                                      {'3', ">^" },
                                                                                      {'4', "^<^" },
                                                                                      {'5', "^^" },
                                                                                      {'6', ">^^" },
                                                                                      {'7', "^^^<" },
                                                                                      {'8', "^^^" },
                                                                                      {'9', ">^^^" }}}},
        {'1', new KeyButton { Key = '1', PathToButton = new Dictionary<char, string> {  {'A', ">>v" },
                                                                                      {'0', ">v" },
                                                                                      {'1', "" },
                                                                                      {'2', ">" },
                                                                                      {'3', ">>" },
                                                                                      {'4', "^" },
                                                                                      {'5', ">^" },
                                                                                      {'6', ">>^" },
                                                                                      {'7', "^^" },
                                                                                      {'8', ">^^" },
                                                                                      {'9', ">>^^" }}}},
        {'2', new KeyButton { Key = '2', PathToButton = new Dictionary<char, string> {  {'A', ">v" },
                                                                                      {'0', "v" },
                                                                                      {'1', "<" },
                                                                                      {'2', "" },
                                                                                      {'3', ">" },
                                                                                      {'4', "<^" },
                                                                                      {'5', "^" },
                                                                                      {'6', ">^" },
                                                                                      {'7', "<^^" },
                                                                                      {'8', "^^" },
                                                                                      {'9', ">^^" }}}},
        {'3', new KeyButton { Key = '3', PathToButton = new Dictionary<char, string> {  {'A', "v" },
                                                                                      {'0', "v<" },
                                                                                      {'1', "<<" },
                                                                                      {'2', "<" },
                                                                                      {'3', "" },
                                                                                      {'4', "<<^" },
                                                                                      {'5', "<^" },
                                                                                      {'6', "^" },
                                                                                      {'7', "<<^^" },
                                                                                      {'8', "<^^" },
                                                                                      {'9', "^^" }}}},
        {'4', new KeyButton { Key = '4', PathToButton = new Dictionary<char, string> {  {'A', ">>vv" },
                                                                                      {'0', ">vv" },
                                                                                      {'1', "v" },
                                                                                      {'2', ">v" },
                                                                                      {'3', ">>v" },
                                                                                      {'4', "" },
                                                                                      {'5', ">" },
                                                                                      {'6', ">>" },
                                                                                      {'7', "^" },
                                                                                      {'8', "^>" },
                                                                                      {'9', "^>>" }}}},
        {'5', new KeyButton { Key = '5', PathToButton = new Dictionary<char, string> {  {'A', ">vv" },
                                                                                      {'0', "vv" },
                                                                                      {'1', "v<" },
                                                                                      {'2', "v" },
                                                                                      {'3', "v>" },
                                                                                      {'4', "<" },
                                                                                      {'5', "" },
                                                                                      {'6', ">" },
                                                                                      {'7', "<^" },
                                                                                      {'8', "^" },
                                                                                      {'9', ">^" }}}},
        {'6', new KeyButton { Key = '6', PathToButton = new Dictionary<char, string> {  {'A', "vv" },
                                                                                      {'0', "vv<" },
                                                                                      {'1', "v<<" },
                                                                                      {'2', "v<" },
                                                                                      {'3', "v" },
                                                                                      {'4', "<<" },
                                                                                      {'5', "<" },
                                                                                      {'6', "" },
                                                                                      {'7', "<<^" },
                                                                                      {'8', "<^" },
                                                                                      {'9', "^" }}}},
        {'7', new KeyButton { Key = '7', PathToButton = new Dictionary<char, string> {  {'A', ">>vvv" },
                                                                                      {'0', ">vvv" },
                                                                                      {'1', "vv" },
                                                                                      {'2', ">vv" },
                                                                                      {'3', ">>vv" },
                                                                                      {'4', "v" },
                                                                                      {'5', ">v" },
                                                                                      {'6', ">>v" },
                                                                                      {'7', "" },
                                                                                      {'8', ">" },
                                                                                      {'9', ">>" }}}},
        {'8', new KeyButton { Key = '8', PathToButton = new Dictionary<char, string> {  {'A', ">vvv" },
                                                                                      {'0', "vvv" },
                                                                                      {'1', "<vv" },
                                                                                      {'2', "vv" },
                                                                                      {'3', ">vv" },
                                                                                      {'4', "<v" },
                                                                                      {'5', "v" },
                                                                                      {'6', ">v" },
                                                                                      {'7', "<" },
                                                                                      {'8', "" },
                                                                                      {'9', ">" }}}},
        {'9', new KeyButton { Key = '9', PathToButton = new Dictionary<char, string> {  {'A', "vvv" },
                                                                                      {'0', "<vvv" },
                                                                                      {'1', "<<vv" },
                                                                                      {'2', "<vv" },
                                                                                      {'3', "vv" },
                                                                                      {'4', "<<v" },
                                                                                      {'5', "<v" },
                                                                                      {'6', "v" },
                                                                                      {'7', "<<" },
                                                                                      {'8', "<" },
                                                                                      {'9', "" }}}},
    };

    public Day21(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

        });

        return part1;
    }


    public override async ValueTask<int> Solve_2()
    {
        int part2 = 0;
        Debug.Assert(InputLines.Length != 0);
        await Task.Run(() =>
        {

        });

        return part2;
    }
}
