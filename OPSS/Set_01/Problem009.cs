namespace OPSS
{
    /* Difficulty: 1/5
     * Find sum of all numbers from 1 to N.
     * 
     * Input
     * First line contains number of data sets n, 1 ≤ n ≤ 200000.
     * Following n lines each contain a single integer N.
     * 
     * Output
     * n lines, where i-th line contains a sum from 1 to N for i-th data set. 
     * Absolute value of this sum is no greater than 2^31.
     */
    public sealed class Suma : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n2\r\n5\r\n7";

        protected override string Output => "6\r\n3\r\n15\r\n28";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            for (int i = 1; i <= n; i++)
            {
                uint N = uint.Parse(input[i]);
                output.Add((N > 0 ? ((N * (N + 1)) >> 1) : (1 - (N * (N - 1) >> 1))).ToString());
            }
        }
    }
}
