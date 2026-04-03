namespace OPSS
{
    /* Time limit: 1s, Memory limit: 8MB, Difficulty: 3/5
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
            int D = int.Parse(input[0]);
            for (int i = 1; i <= D; i++)
            {
                int n = int.Parse(input[i]);
                int count = n, factor = 2, sqrt = (int)Math.Ceiling(Math.Sqrt(n));
                List<int> divisors = [];
                while (factor <= sqrt)
                {
                    while (count % factor == 0)
                    {
                        if (!divisors.Contains(factor))
                            divisors.Add(factor);
                        count /= factor;
                    }
                    factor++;
                }
                count = n;
                if (divisors.Count == 0)
                    count--;
                else
                {
                    int prod = 1;
                    for (int j = 0; j < divisors.Count; j++)
                    {
                        prod *= divisors[j];
                        count -= n / prod;
                    }
                }
                output.Add(count.ToString());
            }
        }
    }
}
