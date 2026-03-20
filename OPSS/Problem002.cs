namespace OPSS
{
    /* Difficulty: 1/5
     * Compute value of a^b
     * 
     * Input
     * First line contains number of data sets n, 1 ≤ n ≤ 200000.
     * Following n lines contain one data set each. Each data set consists of two numbers a, b,
     * 1 ≤ a ≤ 5, 1 ≤ b ≤ 10, separated with a single whitespace.
     * 
     * Output
     * n lines, where i-th line contains value of a^b, where a and b are values from i-th data set.
     */
    public sealed class WenusjanskieDzialki : ProblemBase
    {
        protected override string Input => "3\r\n2 6\r\n3 3\r\n1 10";

        protected override string Output => "64\r\n27\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            for(int i = 1; i <= n; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = 1;
                while(b > 0)
                {
                    if (b % 2 == 1)
                    {
                        c *= a;
                        b--;
                    }
                    else
                    {
                        a *= a;
                        b >>= 1;
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
