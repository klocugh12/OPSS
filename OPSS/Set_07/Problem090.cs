namespace OPSS
{
    /* Time limit: 1.5s, Memory limit: 64MB, Difficulty: 3/5
     * A Loner is a single player game with simple rules: you're given a rectangular board made of square tiles.
     * Some number of pieces is distributed across the board. Each tile can contain no more than 1 piece.
     * In each turn you remove all pieces from a single row, or a single column.
     * In particular it is possible to remove a single piece that way.
     * Find least number of moves needed to remove all pieces from the board.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 5).
     * First line of each data set contains number of pieces on the board N (1 ≤ N ≤ 100000).
     * Following N lines each contain two numbers x and y separated by a whitespace.
     * They represent coordinates of each piece (1 ≤ x ≤ 2*10^9, 1 ≤ y ≤ 2*10^9).
     * 
     * Output
     * C lines, each containing least number of moves needed to remove all pieces from the board.
     */
    public sealed class Samotnik : ProblemBase
    {
        protected override string Input => "1\r\n7\r\n5 12\r\n10 55\r\n10 30\r\n25 12\r\n44 25\r\n5 25\r\n10 1";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[j]);
                j++;
                Dictionary<int, List<int>> horiz = [];
                Dictionary<int, List<int>> vert = [];
                for (int k = 0; k < N; k++)
                {
                    var s = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (!vert.ContainsKey(s[0]))
                        vert.Add(s[0], [s[1]]);
                    else
                        vert[s[0]].Add(s[1]);
                    if (!horiz.ContainsKey(s[1]))
                        horiz.Add(s[1], [s[0]]);
                    else
                        horiz[s[1]].Add(s[0]);
                    j++;
                }
                int moves = 0;
                while (horiz.Any())
                {
                    var keyH = horiz.Keys.OrderByDescending(k => horiz[k].Count).First();
                    var keyV = vert.Keys.OrderByDescending(k => vert[k].Count).First();
                    bool useHoriz = (horiz[keyH].Count > vert[keyV].Count) || (horiz[keyH].Count == vert[keyV].Count && horiz.Count < vert.Count);
                    if (useHoriz)
                    {
                        foreach (var v in horiz[keyH])
                        {
                            vert[v].Remove(keyH);
                            if (vert[v].Count == 0)
                                vert.Remove(v);
                        }
                        horiz.Remove(keyH);
                    }
                    else
                    {
                        foreach (var v in vert[keyV])
                        {
                            horiz[v].Remove(keyV);
                            if (horiz[v].Count == 0)
                                horiz.Remove(v);
                        }
                        vert.Remove(keyV);
                    }
                    moves++;
                }
                output.Add(moves.ToString());
            }
        }
    }
}
