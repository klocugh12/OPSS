namespace OPSS
{
    /* Difficulty: 3/5
     * 
     * You have unlimited amount of 2x1 domino tiles of size.
     * Find number of ways you can tile a 3xN rectangle using those tiles.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 2000.
     * Each data set consists of a single number N, 1 ≤ N ≤ 100000.
     * 
     * Output
     * C lines, each containing P mod 10^6, where P is number of ways to tile a 3xN rectangle.
     */
    public sealed class Domino : ProblemBase
    {
        protected override string Input => "2\r\n2\r\n4";

        protected override string Output => "3\r\n11";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            List<int> result = [3];
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[i]);
                if (N % 2 == 1)
                {
                    output.Add("0");
                    continue;
                }
                N >>= 1;
                for (int j = result.Count; j < N; j++)
                {
                    result.Add(0);
                    for (int k = 0; k < j; k++)
                    {
                        result[j] += (k == 0 ? 3 : 2) * result[j - k - 1];
                    }
                    result[j] += 2;
                    result[j] %= 1_000_000;
                }
                output.Add(result[N - 1].ToString());
            }
        }
    }
}
