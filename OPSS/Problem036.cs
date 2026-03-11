namespace OPSS
{
    /* Difficulty: 3/5
     * Consider n lamps handled by a robot. Each robot has its series number k.
     * A robot with series number k can toggle a switch on first k lamps (either turning them on or off).
     * Then they move on to next k lamps, doing the same operation. They continue this cycle
     * until ordered to stop, however they only stop after finishing toggling each batch of k lamps.
     * For example, if n=4, all series of robots are able to have all lamps on.
     * If n=3, only series 1 and 3 can.
     * However, we want only one lamp to be on. Not all series of robots can achieve this state.
     * Find number of series of robots, which can.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 100. 
     * Each data set contains one number n, 1 ≤ n ≤ 10^9, equal to number of lamps.
     * 
     * Output
     * D lines, each containing number of series of robots which can leave only one lamp on.
     */
    public sealed class Robot : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n6";

        protected override string Output => "2\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                int c = a, k = 2, sqrt = (int)Math.Ceiling(Math.Sqrt(a));
                List<int> divisors = [];
                while(k <= sqrt)
                {
                    while(c % k == 0)
                    {
                        if (!divisors.Contains(k))
                            divisors.Add(k);
                        c /= k;
                    }
                    k++;
                }
                c = a;
                if (divisors.Count == 0)
                    c--;
                else
                {
                    int prod = 1;
                    for (int j = 0; j < divisors.Count; j++)
                    {
                        prod *= divisors[j];
                        c -= a / prod;
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
