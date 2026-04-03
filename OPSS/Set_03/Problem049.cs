namespace OPSS
{
    /* Time limit: 1s, Memory limit: 2MB, Difficulty: 3/5
     * Your goal is to determine, whether number of ways you can select K out of N items is odd or even.
     * 
     * Input
     * First line contains number of data sets d, 1 ≤ d ≤ 1000.
     * Each data set is defined by a single line containing two numbers N and K, 
     * 0 ≤ N, K ≤ 1000000000, separated by a single whitespace.
     * 
     * Output
     * d lines, each containing an answer for respective data set - a single letter 'P' if number of ways is even,
     * 'N' otherwise
     */
    public sealed class CwanyLutek : ProblemBase
    {
        protected override string Input => "3\r\n100 2\r\n7 7\r\n19 9";

        protected override string Output => "P\r\nN\r\nP";

        static string Lutek(int N, int K)
        {
            while (true)
            {
                K = Math.Min(K, N - K);
                if (K == 0)
                    return "N";
                if (K == 1)
                    return N % 2 == 0 ? "P" : "N";
                int x = 1;
                while (x < N)
                    x <<= 1;
                if (N - K <= N - (x >> 1))
                    N -= (x >> 1);
                else
                    return "P";
            }

        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int d = int.Parse(input[0]);
            for (int i = 1; i <= d; i++)
            {
                var splits = input[i].Split(' ');
                int N = int.Parse(splits[0]), K = int.Parse(splits[1]);
                output.Add(Lutek(N, K));
            }
        }
    }
}
