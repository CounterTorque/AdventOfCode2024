using System.Diagnostics;
using System.Text;
using AdventOfCode2024;


public class Day21 : BaseDay
{
    struct KeyButton
    {
        public char Key;
        public Dictionary<char, string> PathToButton;
    }

    Dictionary<char, KeyButton> PadButtons = new Dictionary<char, KeyButton>
    {
       {'A', new KeyButton { Key = 'A', PathToButton = new Dictionary<char, string> {  {'A', "" },
                                                                                      {'0', "<" },
                                                                                      {'1', "^<<" },
                                                                                      {'2', "^<" },
                                                                                      {'3', "^" },
                                                                                      {'4', "^^<<" },
                                                                                      {'5', "^^<" },
                                                                                      {'6', "^^" },
                                                                                      {'7', "^^^<<" },
                                                                                      {'8', "^^^<" },
                                                                                      {'9', "^^^" }}}},
        {'0', new KeyButton { Key = '0', PathToButton = new Dictionary<char, string> {  {'A', ">" },
                                                                                      {'0', "" },
                                                                                      {'1', "^<" },
                                                                                      {'2', "^" },
                                                                                      {'3', ">^" },
                                                                                      {'4', "^^<" },
                                                                                      {'5', "^^" },
                                                                                      {'6', "^^>" },
                                                                                      {'7', "^^^<" },
                                                                                      {'8', "^^^" },
                                                                                      {'9', "^^^>" }}}},
        {'1', new KeyButton { Key = '1', PathToButton = new Dictionary<char, string> {  {'A', ">>v" },
                                                                                      {'0', ">v" },
                                                                                      {'1', "" },
                                                                                      {'2', ">" },
                                                                                      {'3', ">>" },
                                                                                      {'4', "^" },
                                                                                      {'5', "^>" },
                                                                                      {'6', "^>>" },
                                                                                      {'7', "^^" },
                                                                                      {'8', "^^>" },
                                                                                      {'9', "^^>>" }}}},
        {'2', new KeyButton { Key = '2', PathToButton = new Dictionary<char, string> {  {'A', ">v" },
                                                                                      {'0', "v" },
                                                                                      {'1', "<" },
                                                                                      {'2', "" },
                                                                                      {'3', ">" },
                                                                                      {'4', "^<" },
                                                                                      {'5', "^" },
                                                                                      {'6', "^>" },
                                                                                      {'7', "^^<" },
                                                                                      {'8', "^^" },
                                                                                      {'9', ">^^" }}}},
        {'3', new KeyButton { Key = '3', PathToButton = new Dictionary<char, string> {  {'A', "v" },
                                                                                      {'0', "v<" },
                                                                                      {'1', "<<" },
                                                                                      {'2', "<" },
                                                                                      {'3', "" },
                                                                                      {'4', "^<<" },
                                                                                      {'5', "^<" },
                                                                                      {'6', "^" },
                                                                                      {'7', "^^<<" },
                                                                                      {'8', "^^<^" },
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
                                                                                      {'3', ">v" },
                                                                                      {'4', "<" },
                                                                                      {'5', "" },
                                                                                      {'6', ">" },
                                                                                      {'7', "^<" },
                                                                                      {'8', "^" },
                                                                                      {'9', "^>" }}}},
        {'6', new KeyButton { Key = '6', PathToButton = new Dictionary<char, string> {  {'A', "vv" },
                                                                                      {'0', "vv<" },
                                                                                      {'1', "v<<" },
                                                                                      {'2', "v<" },
                                                                                      {'3', "v" },
                                                                                      {'4', "<<" },
                                                                                      {'5', "<" },
                                                                                      {'6', "" },
                                                                                      {'7', "^<<" },
                                                                                      {'8', "^<" },
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
                                                                                      {'1', "vv<" },
                                                                                      {'2', "vv" },
                                                                                      {'3', ">vv" },
                                                                                      {'4', "v<" },
                                                                                      {'5', "v" },
                                                                                      {'6', ">v" },
                                                                                      {'7', "<" },
                                                                                      {'8', "" },
                                                                                      {'9', ">" }}}},
        {'9', new KeyButton { Key = '9', PathToButton = new Dictionary<char, string> {  {'A', "vvv" },
                                                                                      {'0', "vvv<" },
                                                                                      {'1', "vv<<" },
                                                                                      {'2', "vv<" },
                                                                                      {'3', "vv" },
                                                                                      {'4', "v<<" },
                                                                                      {'5', "v<" },
                                                                                      {'6', "v" },
                                                                                      {'7', "<<" },
                                                                                      {'8', "<" },
                                                                                      {'9', "" }}}},
    };

    private Dictionary<char, KeyButton> DirButtons1 = new Dictionary<char, KeyButton>
    {
        {'A', new KeyButton { Key = 'A', PathToButton = new Dictionary<char, string> {  {'A', "" },
                                                                                      {'^', "<" },
                                                                                      {'>', "v" },
                                                                                      {'v', "v<" },
                                                                                      {'<', "v<<" }}}},//
        {'^', new KeyButton { Key = '^', PathToButton = new Dictionary<char, string> {  {'A', ">" },
                                                                                      {'^', "" },
                                                                                      {'>', "v>" },
                                                                                      {'v', "v" },
                                                                                      {'<', "<v" }}}},
        {'>', new KeyButton { Key = '>', PathToButton = new Dictionary<char, string> {  {'A', "^" },
                                                                                      {'^', "<^" },//
                                                                                      {'>', "" },
                                                                                      {'v', "<" },
                                                                                      {'<', "<<" }}}},
        {'v', new KeyButton { Key = 'v', PathToButton = new Dictionary<char, string> {  {'A', ">^" },
                                                                                      {'^', "^" },
                                                                                      {'>', ">" },
                                                                                      {'v', "" },
                                                                                      {'<', "<" }}}},
        {'<', new KeyButton { Key = '<', PathToButton = new Dictionary<char, string> {  {'A', ">>^" },
                                                                                      {'^', ">^" },
                                                                                      {'>', ">>" },
                                                                                      {'v', ">" },
                                                                                      {'<', "" }}}},
    };




    public Day21(string[]? inputLines = null) : base(inputLines)
    {
    }

    public override async ValueTask<int> Solve_1()
    {
        int part1 = 0;
     
        await Task.Run(() =>
        {
            foreach (string sequence in InputLines)
            {                
                char curKey = 'A';
                StringBuilder sbPad = new StringBuilder();

                for (int i = 0; i < sequence.Length; i++)
                {
                    char key = sequence[i];
                    KeyButton pad = PadButtons[curKey];
                    sbPad.Append(pad.PathToButton[key]);
                    sbPad.Append("A");
                    curKey = key;
                }

                string padString = sbPad.ToString();
                Console.WriteLine(padString);

                string robot1Dir = FindDirMap(padString, DirButtons1);            
                Console.WriteLine(robot1Dir);

                string myKeypad = FindDirMap(robot1Dir, DirButtons1);            
                Console.WriteLine(myKeypad);
                
                int numericCode = int.Parse(sequence.Substring(0, 3));
                Console.WriteLine($"{myKeypad.Length} * {numericCode}");
                part1 += (myKeypad.Length * numericCode);
            }

            //v<<A>>^AvA^Av<<A>>^AAv<<A>A>^AAvAA<^A>Av<A>^AA<A>Av<A<A>>^AAAvA<^A>A   MINE
            //<v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A       SAMPLE
            
        });


        //138560 TO HIGH
        return part1;
    }

    private string FindDirMap(string sequence, Dictionary<char, KeyButton> DirButtons)
    {
        StringBuilder sbDir1 = new StringBuilder();
        char curKey = 'A';
        for (int i = 0; i < sequence.Length; i++)
        {
            char key = sequence[i];
            KeyButton dir = DirButtons[curKey];
            sbDir1.Append(dir.PathToButton[key]);
            sbDir1.Append("A");
            curKey = key;
        }

        return sbDir1.ToString();
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
