namespace OPSS
{
    /* Difficulty: 3/5
     * A Pythagorean triangle is described by a triplet of integers, which satisfy equation a^2 + b^2 = c^2.
     * A subset of Pythagorean triangles are so called Perfect Pythagorean triangles,
     * for which additionally two of (a, b, c) numbers are prime.
     * There are infinitely many of them, and first one is so called Egyptian Triangle (3, 4, 5).
     * Find n-th Perfect Pythagorean triangle.
     * 
     * Input
     * First line contains number of data sets  C, 1 ≤ C ≤ 1000.
     * Each data set contains an integer 1..4000 equal to index of Perfect Pythagorean triangle
     * to find.
     * 
     * Output
     * C lines, each containing two prime numbers m, n, m ≤ n, equal to two prime sides
     * of a Perfect Pythagorean triangle with a given index.
     */
    public sealed class DoskonaleTrojkatyPitagorejskie : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n7";

        protected override string Output => "3 5\r\n61 1861";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> sieve = [3];
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                int index = int.Parse(input[i]);
                int m = 0, n = 0;
                int j = 0, k = 0;
                while (j < index)
                {
                    if (k == sieve.Count)
                    {
                        int next = sieve[k - 1] + 2;
                        if (sieve.All(p => next % p != 0))
                            sieve.Add(next);
                    }
                    m = sieve[k];
                    n = ((m * m) >> 1) + 1;
                    int l = sieve[^1];
                    while (sieve[^1] < n)
                    {
                        if (sieve.All(p => l % p != 0))
                            sieve.Add(l);
                        l += 2;
                    }
                    if (sieve.Contains(n))
                        j++;
                    k++;
                }
                output.Add($"{m} {n}");
            }
        }
    }
}
