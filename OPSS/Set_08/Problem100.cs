namespace OPSS
{
    /* Time limit: 0.75s, Memory limit: 16MB, Difficulty: 5/5
     * On a wedding reception there was a certain even number of guests.
     * Determine number of ways guests can clink their glasses with each other, assuming as follows:
     * All guests participate in cheers. All guests clink their glasses at the same time.
     * No cheering guests cross hands with each other.
     * 
     * Input
     * First line contains number of data sets D (0 < D ≤ 1000).
     * Each data set contains a single line with number of pairs of guests Ni (0 < Ni ≤ 8000).
     * 
     * Output
     * D lines, each containing two numbers separated by a whitespace.
     * They are, respectively Ki - number of ways guests can clink their glasses, and Ci - number 
     * of digits Ki has. If Ki is greater or equal to 10^9, write only first 9 digits of Ki.
     */
    public sealed class WeselneToasty : ProblemBase
    {
        protected override string Input => "2\r\n5\r\n100";

        protected override string Output => "42 2\r\n896519947 57";

        static List<int> Mul(List<int> target, List<int> factor)
        {
            List<int> res = new(target.Count + factor.Count);
            for (int k = 0; k < target.Count + factor.Count - 1; k++)
                res.Add(0);
            for (int k = 0; k < target.Count; k++)
            {
                for (int l = 0; l < factor.Count; l++)
                {
                    res[k + l] += target[k] * factor[l];
                }
            }
            int l2 = res.Count - 1;
            while (l2 >= 0)
            {
                if (res[l2] < 10)
                {
                    l2--;
                    continue;
                }
                if (l2 > 0)
                    res[l2 - 1] += res[l2] / 10;
                else
                {
                    res.Insert(0, res[l2] / 10);
                    l2++;
                }
                res[l2] %= 10;
                l2--;
            }
            return res;
        }

        static List<int> primes = [2, 3];

        static void Factorize(Dictionary<int, int> dic, int n)
        {
            int limit = (int)Math.Sqrt(n);
            int i = 0;
            while (primes[i] <= limit && n > 1)
            {
                int c = primes[i];
                while (n % primes[i] == 0)
                {
                    if (!dic.TryAdd(c, 1))
                        dic[c]++;
                    n /= c;
                }
                i++;
            }
            if (n > 1)
            {
                if (n > primes[^1])
                    primes.Add(n);
                if (!dic.TryAdd(n, 1))
                    dic[n]++;
            }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            List<Dictionary<int, int>> catalan = [new() { { 1, 1 } }, new() { { 1, 1 } }, new() { { 2, 1 } }];
            for (int i = 1; i <= D; i++)
            {
                int Ni = int.Parse(input[i]);
                while (catalan.Count <= Ni)
                {
                    Dictionary<int, int> factors = new(catalan[^1]);
                    Factorize(factors, (catalan.Count << 1) - 1);
                    if (!factors.TryAdd(2, 1))
                        factors[2]++;
                    Dictionary<int, int> dens = new();
                    Factorize(dens, catalan.Count + 1);
                    foreach (var c in dens.Keys)
                    {
                        factors[c] -= dens[c];
                        if (factors[c] == 0)
                            factors.Remove(c);
                    }
                    catalan.Add(factors);
                }
                List<int> res = [1];
                foreach (var c in catalan[Ni].Keys)
                {
                    int k = c;
                    for (int j = 1; j < catalan[Ni][c]; j++)
                        k *= c;
                    List<int> fac = [];
                    while (k > 0)
                    {
                        fac.Insert(0, k % 10);
                        k /= 10;
                    }
                    res = Mul(res, fac);
                }
                output.Add($"{string.Join("", res.Take(Math.Min(res.Count, 9)))} {res.Count}");
            }
        }
    }
}
