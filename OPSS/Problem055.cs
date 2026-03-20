namespace OPSS
{
    /* Difficulty: 4/5
     * A communication company has set up network of its transmitters.
     * It decided to configure it in such way that each transmitter can reach any other tranmsmitter
     * either directly or using at most a single intermediate transmitter.
     * In case of disabling some transmitter, transmission should also be direct or require at most
     * a single intermediate transmitter.
     * Connecting all transmitters directly pairwise together is not allowed.
     * Find number of configurations meeting those criteria.
     * Transmitters are nondistinct.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 50.
     * Each data set consists of a single line containing a single number N, 2 ≤ N ≤ 50,
     * equal to number of transmitters in a network.
     * 
     * Output
     * C lines, each containing number of configurations meeting described criteria,
     * assuming transmitters are nondistinct.
     */
    public sealed class Nadajniki : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n3\r\n4";

        protected override string Output => "0\r\n1\r\n4";

        static List<int> Mul(List<int> target, List<int> factor)
        {
            List<int> res = [];
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

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[i]);
                if(N < 4)
                {
                    output.Add((N - 2).ToString());
                    continue;
                }
                int b = ((N - 2) * (N - 3) >> 1) + 1;
                List<int> res = [1];
                List<int> p2 = [2];
                while (b > 0)
                {
                    if (b % 2 == 0)
                    {
                        p2 = Mul(p2, p2);
                        b >>= 1;
                    }
                    else
                    {
                        res = Mul(res, p2);
                        b--;
                    }
                }
                output.Add(string.Join("", res));
            }
        }
    }
}
