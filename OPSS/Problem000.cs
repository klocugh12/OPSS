namespace OPSS
{
    /* Problemset: 0, Difficulty: 1/5
     * Fibonacci numbers are defined as follows:
     * F(0)=1,
     * F(1)=1,
     * F(n)=F(n-1)+F(n-2), n>1.
     * Compute value of F(n)
     * 
     *  Input
     *  First line contains number of data sets d 0 ≤ d ≤ 1000. 
     *  Following d lines contain numbers from 0 to 45.
     *  
     *  Output
     *  d lines, where i-th line contains F(n), where n is value from i-th data set.
     */
    public sealed class TreningLiczbyFibonacciego : ProblemBase
    {
        protected override string Input => "3\r\n4\r\n9\r\n14";

        protected override string Output => "5\r\n55\r\n610";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int d = int.Parse(input[0]);
            for(int i = 1; i <= d; i++)
            {
                int a = 1, b = 1, c = 1;
                int N2 = int.Parse(input[i]);
                while(c < N2)
                {
                    int temp = b;
                    b = a + b;
                    a = temp;
                    c++;
                }
                output.Add(b.ToString());
            }
        }
    }
}
