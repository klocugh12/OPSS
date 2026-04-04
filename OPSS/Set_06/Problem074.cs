namespace OPSS
{
    /* Time limit: 0.75s, Memory limit: 16MB, Difficulty: 3/5
     * You're given a number of samples, which contain specific particles.
     * You're given a set of norms, which specify allowed minimum and maximum number of particles in a sample.
     * Find number of samples, which satisfy each norm.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 3).
     * First line of each data set contains number of samples N (1 ≤ N ≤ 100000).
     * Second line of data set contains N numbers separated by a whitespace, equal to amount of particles
     * in each sample a1, a2,..., aN (0 ≤ ai < 2^31, 1 ≤ i ≤ N). 
     * Third line of each data set contains number of norms P (1 ≤ P ≤ 5000).
     * Each of following P lines contains two numbers separated by a whitespace.
     * They describe lower and upper limit for each norm respectively, bk, ck (0 ≤ bk ≤ ck < 2^31, 1 ≤ k ≤ P).
     * Sample ai satisfies k-th norm, if bk ≤ ai ≤ ck.
     * 
     * Output
     * For each data set write P lines with numbers p1, p2,..., pP, where pk (1 ≤ k ≤ P) 
     * is equal to number of samples satisfying k-th norm in a data set.
     */
    public sealed class Normy : ProblemBase
    {
        protected override string Input => "1\r\n5\r\n123 304 200604 12 3000\r\n3\r\n1 300\r\n300 3000\r\n5 5";

        protected override string Output => "2\r\n2\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 0; i < C; i++)
            {
                j++;
                List<int> samples = input[j].Split(' ').Select(s => int.Parse(s)).ToList();
                samples.Sort();
                j++;
                int P = int.Parse(input[j]);
                j++;
                for (int k = 0; k < P; k++)
                {
                    int[] bounds = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (samples[0] > bounds[1] || samples[^1] < bounds[0])
                    {
                        output.Add("0");
                        j++;
                        continue;
                    }
                    int l = 0, p = samples.Count - 1, m;
                    while (p != l)
                    {
                        m = (l + p) >> 1;
                        if (samples[m] < bounds[0])
                            l = m + 1;
                        else
                            p = m;
                    }
                    int first = l;
                    l = 0;
                    p = samples.Count - 1;
                    while (p != l)
                    {
                        m = (l + p) >> 1;
                        if (samples[m] > bounds[1])
                            p = m - 1;
                        else
                        {
                            if (samples[m + 1] > bounds[1])
                                p = m;
                            else
                                l = m + 1;
                        }
                    }
                    int second = l;
                    output.Add((second - first + 1).ToString());
                    j++;
                }
            }
        }
    }
}
