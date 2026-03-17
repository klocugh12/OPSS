namespace OPSS
{
    /* Difficulty: 1/5
     * Fibonacci numbers are defined as follows:
     * F(0)=1,
     * F(1)=1
     * F(n)=F(n-1)+F(n-2), n>1.
     * Find least significant digit of n-th Fibonacci's number in binary system.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 100).
     * Each data set consists of a single line containing a 1 to 255-bit binary number b.
     * Most significant bit of each number is always 1.
     * 
     * Output
     * C lines, each containing least significant bit of fib(b).
     */
    public sealed class StarozytnaMaszyna : ProblemBase
    {
        protected override string Input => "2\r\n111\r\n1001";

        protected override string Output => "1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                bool odd = true;
                int mod = 0;
                for(int j = input[i].Length - 1; j >= 0; j--)
                {
                    if (input[i][j] == '1')
                    {
                        mod = (mod + (odd ? 1 : 2)) % 3;
                    }
                    odd = !odd;
                }
                output.Add(mod == 0 ? "0" : "1");
            }
        }
    }
}
