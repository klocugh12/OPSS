namespace OPSS
{
    /* Time limit: 1s, Memory limit: 1MB, Difficulty: 3/5
     * We're given an infinite tiled board. 
     * We fill rectangle consisting of m x n tiles with pebbles, each occupying a single tile.
     * A pebble can jump over another stone horizontally or vertically, if pebbles to jump to is free.
     * A pebble jumped over is removed from the board.
     * What is minimum number of pebbles remaining on the board?
     * 
     * Input
     * Numbers m and n separated by whitespace.
     * 
     * Output
     * Minimum number of pebbles remaining on the board.
     */
    public sealed class ProstaGra : ProblemBase
    {
        protected override string Input => "3 4";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int m = int.Parse(splits[0]), n = int.Parse(splits[1]);
            //3 x n is reduced to 3 x (n - 1), 2 x n is reduced to 2 for odd n, 1 for even n.
            if (m == 1 || n == 1)
                output.Add(((Math.Max(m, n) + 1) >> 1).ToString());
            else
                output.Add((m % 3 == 0 || n % 3 == 0 || ((m == 2 && n % 2 == 1) || (n == 2 && m % 2 == 1))) ? "2" : "1");
        }
    }
}
