namespace OPSS
{
    /* Difficulty: 4/5
     * Alice and Bob play a gambling game. Each of them writes down a pattern consisting of letters
     * O (heads) and R (tails). Then they alternate throwing a coin, each writing down result,
     * until either of them gets a match against a pattern they have.
     * Your task is to establish, given two arbitrary patterns, which pattern is more likely to win.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 200.
     * Each of C following lines contains two strings, Alice's and Bob's patterns respectively.
     * Patterns contain no more than 30 characters and consist only of letters "O" and "R".
     * 
     * Output
     * C lines, each containing an answer for each data set:
     * ● 1 - if Alice's pattern is more likely to win
     * ● 2 - if Bob's pattern is more likely to win
     * ● 0 - if both patterns are equally likely to win
     */
    public sealed class BinarneBingo : ProblemBase
    {
        protected override string Input => "6\r\nOORO OROO\r\nOROO ROOO\r\nOORO ROO\r\nROR ORO\r\nORO RO\r\nRO RORO";

        protected override string Output => "1\r\n1\r\n2\r\n0\r\n2\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                string[] tab = input[i].Split(' ');

                if (tab[0].Length != tab[1].Length)
                {
                    output.Add(tab[0].Length < tab[1].Length ? "1" : "2");
                }
                else
                {
                    int n1 = 0, n2 = 0;
                    List<int[]> lcs = [];
                    while (n1 < tab[0].Length && n2 < tab[1].Length)
                    {
                        if (tab[0][n1] != tab[1][n2])
                            n1++;
                        int k = 0;
                        while (n1 + k < tab[0].Length && n2 + k < tab[1].Length && tab[0][n1 + k] == tab[1][n2 + k])
                        {
                            k++;
                        }
                        if (k > 1)
                        {
                            lcs.Add([n1, n2, k]);
                        }
                        n1 += k;
                        n2 += k;
                    }
                    n1 = 0;
                    n2 = 0;
                    while (n1 < tab[0].Length && n2 < tab[1].Length)
                    {
                        if (tab[0][n1] != tab[1][n2])
                            n2++;
                        int k = 0;
                        while (n1 + k < tab[0].Length && n2 + k < tab[1].Length && tab[0][n1 + k] == tab[1][n2 + k])
                        {
                            k++;
                        }
                        if (k > 1)
                        {
                            lcs.Add([n1, n2, k]);
                        }
                        n1 += k;
                        n2 += k;
                    }
                    for (int k = 0; k < lcs.Count; k++)
                    {
                        n1 = lcs[k][0];
                        n2 = lcs[k][1];
                        while (n1 > 0 && n2 > 0 && tab[0][n1 - 1] == tab[1][n2 - 1])
                        {
                            n1--;
                            n2--;
                        }
                        int delta = lcs[k][0] - n1;
                        if (delta > 0)
                        {
                            lcs[k][0] -= delta;
                            lcs[k][1] -= delta;
                            lcs[k][2] += delta;
                        }
                    }
                    lcs.Sort((a, b) => -a[2].CompareTo(b[2]));
                    lcs.RemoveAll(x => x[2] < lcs[0][2]);
                    if (lcs.Count == 1)
                    {
                        int before1 = lcs[0][0], before2 = lcs[0][1],
                            after1 = tab[0].Length - (lcs[0][0] + lcs[0][2]), after2 = tab[1].Length - (lcs[0][1] + lcs[0][2]);
                        n1 = after1 + Math.Max(before1 - 1, 0) + tab[0].Length;
                        n2 = after2 + Math.Max(before2 - 1, 0) + tab[1].Length;
                        output.Add(n2 == n1 ? "0" : n1 < n2 ? "1" : "2");
                    }
                    else
                        output.Add("0");
                }
            }
        }
    }
}
