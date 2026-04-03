namespace OPSS
{
    /* Time limit: 1s, Memory limit: 1.5MB, Difficulty: 2/5
     * Compute value of M = 11^n.
     * Hint:
     *           1 + 1 = 2
     *         1 + 2 + 1 = 4
     *       1 + 3 + 3 + 1 = 8
     *     1 + 4 + 6 + 4 + 1 = 16
     *     
     * Input
     * First line contains number of data sets i, (0 < i ≤ 500).
     * Following i lines each contain a single number n.
     * 
     * Output
     * n lines, where i-th line contains value of M = 11^n for i-th data set.
     * M has no more than 256 digits.
     */
    public sealed class DziwneWlasnosciJedenastu : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n10";

        protected override string Output => "1331\r\n25937424601";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int i = int.Parse(input[0]);
            for (int i2 = 1; i2 <= i; i2++)
            {
                int a = int.Parse(input[i2]);
                byte[] result = new byte[a + 1];
                result[^1] = 1;
                for (int j = 0; j < a; j++)
                {
                    for (int k = 0; k <= j; k++)
                    {
                        result[^((j + 2) - k)] += result[^((j + 1) - k)];
                        int l = result.Length - (j + 2) + k;
                        while (result[l] > 9)
                        {
                            result[l - 1]++;
                            result[l] %= 10;
                            l--;
                        }
                    }
                }
                output.Add(string.Join("", result));
            }
        }
    }
}
