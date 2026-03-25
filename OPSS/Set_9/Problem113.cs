namespace OPSS
{
    /* Difficulty: 3/5
     * Find number of distinct products of two natural numbers from 1 to a given n.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 100).
     * Each line contains a single number n (1 ≤ n ≤ 100000).
     * 
     * Output
     * C lines, each containing number of distinct products of numbers a and b, where 1 ≤ a, b ≤ n.
     */
    public sealed class Iloczyny : ProblemBase
    {
        protected override string Input => "2\r\n5\r\n8";

        protected override string Output => "14\r\n30";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> totals = [1];
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                var n = int.Parse(input[i]);
                while (n > totals.Count)
                {
                    int j = totals.Count + 1;
                    int lim = (int)Math.Ceiling(Math.Sqrt(j));
                    int minDiv = 0;
                    int k2 = 2;
                    while (k2 <= lim && minDiv == 0)
                    {
                        if (j % k2 == 0)
                            minDiv = k2;
                        else
                            k2++;
                    }
                    totals.Add(totals[j - 2] + j - (k2 == 0 ? 0 : (j / k2) - 1));
                }
                output.Add(totals[n - 1].ToString());
            }
        }
    }
}
