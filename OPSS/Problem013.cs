namespace OPSS
{
    /* Difficulty: 3/5
     * We're given an infinite tiled board. 
     * We fill rectangle consisting of m x n tiles with stones, each occupying a single tile.
     * A stone can jump over another stone horizontally or vertically, if stone to jump to is free.
     * A stone jumped over is removed from the board.
     * What is minimum number of stones remaining on the board?
     * 
Input
Numbers m and n separated by whitespace.

Output
Minimum number of stones remaining on the board.
     */
    public sealed class ProstaGra : ProblemBase
    {
        protected override string Input => "3 4";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
            //rządek 3xn się redukuje do 3x(n - 1), ale dla każdej nieparzystej 2xn zostaną dwie.
            //Dla parzystej n przy 2xn zostanie tylko jedna.
            if (a == 1 || b == 1)
                output.Add(((Math.Max(a, b) + 1) >> 1).ToString());
            else
                output.Add((a % 3 == 0 || b % 3 == 0 || ((a == 2 && b % 2 == 1) || (b == 2 && a % 2 == 1))) ? "2" : "1");
        }
    }
}
