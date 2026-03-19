namespace OPSS
{
    /* Difficulty: 3/5
     * You're given 10 numbers from 1 to 10000.
     * Find last digit of number of divisors of product of those numbers.
     * For example, number 6 has 4 divisors (1, 2, 3 and 6).
     * 
     * Input
     * Ten lines, each containing a number from 1 to 10000.
     * 
     * Output
     * Last digit of number of divisors of product of input numbers.
     */
    public sealed class DzielniBaloniarze : ProblemBase
    {
        protected override string Input => "1\r\n2\r\n6\r\n1\r\n3\r\n1\r\n1\r\n1\r\n1\r\n1";

        protected override string Output => "9";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> primes = [2, 3];
            for (int i = 5; i <= 1000; i  += 2)
            {
                int index = 0;
                int limit = (int)Math.Sqrt(i);
                bool add = true;
                while (primes[index] <= limit)
                {
                    if(i % primes[index] != 0)
                    {
                        add = false;
                        break;
                    }
                    index++;
                }
                if (add)
                    primes.Add(i);
            }
            int[] counts = new int[primes.Count];
            for(int i = 0; i < 10; i++)
            {
                int c = int.Parse(input[i]);
                int j = 0;
                while(c > 1)
                {
                    while(c % primes[j] == 0)
                    {
                        c /= primes[j];
                        counts[j]++;
                    }
                    j++;
                }
            }
            int result = 1;
            for (int i = 0; i < counts.Length; i++)
                if (counts[i] > 0)
                    result = (result * (counts[i] + 1)) % 10;
            output.Add(result.ToString());
        }
    }
}
