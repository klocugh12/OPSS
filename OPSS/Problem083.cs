namespace OPSS
{
    /* Difficulty: 3/5
     * Alice and Bob are playing poker and using matches to bet. Alice has only blue matches and Bob
     * only green ones. Before the betting phase there were a blue matches and b green ones on the table.
     * Alice and Bob take turns, starting with one, who has less matches on the table.
     * In each turn player adds as many matches as there are opponent's matches on the table at the moment.
     * Find number of matches at the table after n turns. Write it as remainder of division of said number 
     * by specified integer m.
     * 
     * Input
     * First line contains number of data sets  C (1 ≤ C ≤ 1000).
     * Each data set consists of a single line containing four integers a, b, n, m
     * separated by a whitespace each (0 < a, b < 2^31, 0 ≤ n < 2^31, 2 ≤ m < 2^31).
     * They are, respectively, initial numbers of blue and green matches on the table,
     * total number of turns in betting phase, and a value to divide by to get a remainder.
     * 
     * Output
     * C lines, each containing a remainder of dividing number of matches on the table after n turns by m.
     */
    public sealed class Licytacja : ProblemBase
    {
        protected override string Input => "3\r\n1 1 4 1000\r\n5 6 7 1000\r\n4 101 23 999";

        protected override string Output => "13\r\n309\r\n767";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]), n = int.Parse(splits[2]), m = int.Parse(splits[3]);
                int steps = n;
                int[][] tab = [[1, 1], [1, 0]];
                int[][] res = [[1, 1], [1, 0]];
                while (steps > 0)
                {
                    if (steps % 2 == 0)
                    {
                        tab = Mul(tab, tab, m);
                        steps >>= 1;
                    }
                    else
                    {
                        res = Mul(res, tab, m);
                        steps--;
                    }
                }
                output.Add($"{(n % 2 == 0 ? ((res[0][0] * a + res[0][1] * b) % m) : ((res[0][0] * b + res[0][1] * a) % m))}");
            }
        }

        static int[][] Mul(int[][] tab1, int[][] tab2, int m)
        {
            return [[(tab1[0][0] * tab2[0][0] + tab1[1][0] * tab2[0][1]) % m, (tab1[0][1] * tab2[0][0] + tab1[1][1] * tab2[0][1]) % m],
                [(tab1[0][0] * tab2[1][0] + tab1[1][0] * tab2[1][1]) % m, (tab1[1][1] * tab2[1][1] + tab1[0][1] * tab2[1][0]) % m]];
        }
    }
}
