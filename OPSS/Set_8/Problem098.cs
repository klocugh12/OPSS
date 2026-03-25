namespace OPSS
{
    /* Difficulty: 5/5
     * Consider a sequence defined as follows. You're given N initial terms a1, a2, a3, .. , aN.
     * To derive following terms, add up N previous ones. For instance:
     * aN+1 = a1 + a2 + .. + aN,
     * aN+2 = a2 + a3 + .. + aN+1.
     * 
     * ...and so on.
     * Your job is to determine five least significant digits of sum of terms of this sequence
     * starting from ap and ending with ak for certain p and k.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 20.
     * Each data set consists of three lines.
     * First line contains number of initial terms N, 2 ≤ N ≤ 50.
     * Second line contains N numbers, each separated by a whitespace, equal to those initial terms 
     * in order; all values are in range <0; 10^5>,.
     * Third line contains two numbers p and k separated by a whitespace, 0 < p ≤ k < 2^31,
     * equal to beginning and end of range of terms to add up.
     * 
     * Output
     * D lines, each containing at most 5 (or less if there are leading zeros) least significant 
     * digits of sum of terms of respective sequence starting with ap and ending with ak (inclusive).
     */
    public sealed class Ciag : ProblemBase
    {
        protected override string Input => "1\r\n4\r\n2 3 4 5\r\n5 6";

        protected override string Output => "40";

        const int C = 100_000;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                j++;
                List<int> list = input[j].Split(' ').Select(s => int.Parse(s)).Reverse().ToList();
                j++;
                var splits = input[j].Split(' ');
                int p = int.Parse(splits[0]), k = int.Parse(splits[1]);
                j++;
                int sum = GetSum(k + 2, list);
                sum -= GetSum(p + 2, list);
                sum -= list[0];
                output.Add(((sum + C) % C).ToString());
            }

            static int GetSum(int a, List<int> first)
            {
                if (first.Count > a)
                    return first.Take(a).Sum() % C;
                var exp = Exp(a - first.Count, first.Count);
                return exp.Zip(first, (a, b) => a * b).Sum() % C;
            }

            static int[] Exp(int power, int rank)
            {
                int[][] ret = new int[rank][];
                ret[0] = Enumerable.Range(0, rank).Select(i => 1).ToArray();
                for (int i = 1; i < rank; i++)
                {
                    ret[i] = Enumerable.Range(0, rank).Select(j => 0).ToArray();
                    ret[i][i - 1] = 1;
                }
                int[][] ret2 = new int[rank][];
                Array.Copy(ret, ret2, rank);
                power--;
                while (power > 0)
                {
                    if (power % 2 == 0)
                    {
                        ret2 = Mul(ret2, ret2);
                        power >>= 1;
                    }
                    else
                    {
                        ret = Mul(ret, ret2);
                        power--;
                    }
                }
                return ret[0];
            }

            static int[][] Mul(int[][] dest, int[][] src)
            {
                int[][] ret = new int[dest.Length][];
                for (int i = 0; i < dest.Length; i++)
                    ret[i] = Enumerable.Range(0, dest.Length).Select(j => 0).ToArray();
                for (int i = 0; i < dest.Length; i++)
                    for (int j = 0; j < dest.Length; j++)
                    {
                        for (int k = 0; k < dest.Length; k++)
                        {
                            ret[i][j] += dest[i][k] * src[k][j] % C;
                        }
                        ret[i][j] %= C;
                    }
                return ret;
            }
        }
    }
}
