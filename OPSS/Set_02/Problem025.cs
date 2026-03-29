namespace OPSS
{
    /* Difficulty: 4/5
     * One day Bob was bored and started writing down a sequence. It went as follows: 1121231234123451234561234567... 
     * Find n-th digit of that sequence
     * 
     * Input
     * First line contain number of data sets C, 1 ≤ C ≤ 100.
     * Next C lines each contain a single number N, 1 ≤ N < 2^31, position of digit to find.
     * 
     * Output
     * C lines each containing a digit at N-th position.
     */
    public sealed class ZagadkowyCiag : ProblemBase
    {
        protected override string Input => "3\r\n3\r\n4\r\n5";

        protected override string Output => "2\r\n1\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> ranges = [0];
            int a1 = 1, n = 9;
            for (int i = 1; i <= 4; i++)
            {
                ranges.Add(ranges[ranges.Count - 1] + (n * ((a1 << 1) + (n - 1) * i) >> 1));
                a1 = (int)Math.Pow(10, i) + 1;
                n *= 10;
            }
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[i]);
                int j = 1;
                while (j < ranges.Count && ranges[j] < N)
                    j++;
                a1 = (int)(Math.Pow(10, j - 1));
                N -= ranges[j - 1];
                double a = 0.5 * j;
                double b = (a + a1 - 1);
                int sqrt = (int)((Math.Sqrt(b * b + a * (N << 2)) - b) / (2.0 * a));
                N -= sqrt * (a1 - 1) + j * ((sqrt * (sqrt + 1)) >> 1);
                a1 = 9;
                j = 1;
                while (N > a1)
                {
                    N -= a1;
                    j++;
                    a1 *= 10;
                }
                a1 = (int)Math.Ceiling((double)N / j);
                output.Add(N == 0 ? (sqrt % 10).ToString() : a1.ToString()[(N + j - 1) % j].ToString());
            }
        }
    }
}
