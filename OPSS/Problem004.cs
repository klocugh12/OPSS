namespace OPSS
{
    /* Difficulty: 1/5
     * A prime number is an integer greater than 1 that is only divisible by 1 and itself.
     * Find n-th prime number.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 200000.
     * Following C lines contain one data set each. Each data set consist of single number n, 1 ≤ n ≤ 15000.
     * 
     * Output
     * C lines, where i-th line contains n-th prime number, where n is value from i-th data set.
     */
    public sealed class LiczbyPierwsze : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n2\r\n5\r\n7";

        protected override string Output => "5\r\n3\r\n11\r\n17";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> primes = [3];

            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                int n = int.Parse(input[i]);
                if (n == 1)
                    output.Add("2");
                else
                {
                    int candidate = primes.Last() + 2;
                    while (primes.Count < n - 1)
                    {
                        if (!primes.Any(p => candidate % p == 0))
                            primes.Add(candidate);
                        else
                            candidate += 2;
                    }
                    output.Add(primes[n - 2].ToString());
                }
            } 
        }
    }
}
