namespace OPSS
{
    /* Difficulty: 3/5
     * Your task is to determine, whether number of ways you can select K out of N items is odd or even.
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

        static string Lutek(int a, int b)
        {
            while (true)
            {
                b = Math.Min(b, a - b);
                if (b == 0)
                    return "N";
                if (b == 1)
                    return a % 2 == 0 ? "P" : "N";
                int x = 1;
                while (x < a)
                    x <<= 1;
                if (a - b <= a - (x >> 1))
                    a -= (x >> 1);
                else
                    return "P";
            } 

        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                output.Add(Lutek(a, b));
            }
        }
    }
}
