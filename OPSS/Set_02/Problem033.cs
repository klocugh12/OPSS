namespace OPSS
{
    /* Time limit: 1.5s, Memory limit: 6MB, Difficulty: 5/5
     * A certain company has its office in a center of a circle, on whose perimeter lie
     * houses of all of company's employees.
     * A company attempts to build network of roads to its office from all homes.
     * Each road can either connect a house and office directly, or two adjacent houses
     * lying on a circle.
     * Your goal is to find number of distinct networks containing minimum possible
     * number of roads that meet those criteria.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * Each data set consists of a number of employees of the company n, 3 ≤ n ≤ 1000.
     * 
     * Output
     * c lines, each containing a single number equal to number of distinct networks
     * meeting task's criteria.
     */
    public sealed class Drogi : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n5";

        protected override string Output => "16\r\n121";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            List<long> denoms = [1L, 3L];
            List<List<int>> denoms2 = [[1], [3]];
            //Kirchoff's theorem, matrix determinant after Gaussian elimination reduced to 2 elements.
            for (int i = 1; i <= C; i++)
            {
                int n = int.Parse(input[i]);
                while (denoms.Count < n)
                {
                    denoms.Add(3L * denoms[^1] - denoms[^2]);
                    denoms2.Add(Sub(Mul(denoms2[^1], [3]), denoms2[^2]));
                }
                long last = denoms[n - 2] + 1L;
                var last2 = Add([1], denoms2[n - 2]);
                var resx2 = Div(Mul(Sub(denoms2[n - 1], last2), Add(last2, denoms2[n - 1])), denoms2[n - 2]);
                long resx = (denoms[n - 1] - last) * (denoms[n - 1] + last) / denoms[n - 2];
                output.Add(string.Join("", resx2).ToString());
            }
        }

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

        static List<int> Add(List<int> shorter, List<int> longer)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for (k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] += shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a >= 10;
                if (carry)
                    longer[longer.Count - k - 1] %= 10;
            }
            k = longer.Count - shorter.Count - 1;
            while (carry && k >= 0)
            {
                longer[k]++;
                carry = longer[k] >= 10;
                if (carry)
                    longer[k] %= 10;
            }
            if (k < 0 && carry)
                longer.Insert(0, 1);
            return longer;
        }

        static List<int> Sub(List<int> longer, List<int> shorter)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for (k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] -= shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a < 0;
                if (carry)
                    longer[longer.Count - k - 1] += 10;
            }
            k = longer.Count - shorter.Count - 1;
            while (carry && k >= 0)
            {
                longer[k]--;
                carry = longer[k] < 0;
                if (carry)
                    longer[k] += 10;
            }
            if (k < 0 && carry)
                longer.RemoveAt(0);
            return longer;
        }

        static List<int> Div(List<int> num, List<int> den)
        {
            List<int> ret = new(num.Count - den.Count + 1);
            List<(int, List<int>)> patterns = [(1, den)];
            for (int i = 2; i < 10; i++)
                patterns.Add((i, Add(den, patterns[^1].Item2)));
            patterns.Sort((a, b) => a.Item2[0] == b.Item2[0] ?
                a.Item2[1].CompareTo(b.Item2[1]) : a.Item2[0].CompareTo(b.Item2[0]));

            while (num.Count > 0)
            {
                int guess2 = patterns.Count - 1;
                int digit = 0;
                while (digit < patterns[guess2].Item2.Count)
                {
                    while (guess2 >= 0 && num[digit] < patterns[guess2].Item2[digit])
                        guess2--;
                    if (guess2 < 0)
                        break;
                    if (num[digit] == patterns[guess2].Item2[digit])
                        digit++;
                    else
                        break;
                }
                int i = 0;
                if (guess2 < 0)
                {
                    ret.Add(0);
                    i++;
                    guess2 = patterns.Count - 1;
                }
                ret.Add(patterns[guess2].Item1);
                if (patterns[guess2].Item2.Count <= num.Count)
                {
                    for (; i < patterns[guess2].Item2.Count; i++)
                    {
                        num[i] -= patterns[guess2].Item2[i];
                        if (num[i] < 0)
                        {
                            num[i] += 10;
                            int j = i - 1;
                            while (j > 0 && num[j] == 0)
                            {
                                num[j] = 9;
                                j--;
                            }
                            num[j]--;
                        }
                    }
                    int zeros = 0;
                    while (num.Any() && num[0] == 0)
                    {
                        zeros++;
                        if (zeros > patterns[guess2].Item2.Count)
                            ret.Add(0);
                        num.RemoveAt(0);
                    }
                }
                else
                {
                    ret.Add(0);
                    break;
                }
            }
            return ret;
        }
    }
}
