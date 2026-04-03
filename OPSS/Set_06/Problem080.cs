namespace OPSS
{
    /* Time limit: 1.5s, Memory limit: 32MB, Difficulty: 4/5
     * Consider a genome coded with pairs of uppercase letters A-J. A healthy genome consists of
     * the same pair P repeated n times. For example, if P equals 'AA', then healthy genome with
     * length 3 is 'AA AA AA'. A virus alters a healthy gene to another one, different than P.
     * Each gene can only be modified at most once. For instance, infected genotype may be
     * 'AB AC AA', in which case virus has genotype 'AB AC'. Each subsequence in such genotype is a 
     * separate bacteria. 'AB AC' genome has three distinct bacterias: 'AB', 'AC' and 'AB AC'.
     * Consider a picobot containing a program of length X. It starts from first gene in a genotype
     * and compares X consecutive genes to its program. If they are equal, original (healthy) sequence
     * is restored. Otherwise, a picobot moves by X genes to do another comparison.
     * A single picobot may not always be able to perform a full restore of genome.
     * Find a program which restorest largest possible portion of infected genome.
     * If there are multiple candidates, select one, which removed the most bacteria.
     * If that still results in more than one candidate, write a 0 instead.
     * Additionally, if infected genotype is no more than 10000 genes long, find number of ways
     * a virus can alter healthy genome.
     * 
     * Input
     * First line contains a number and two letters separated by a whitespace. First number N, 0 < N ≤ 1000000
     * represents length of genome. It is followed by two letters A-J describing a healthy gene P.
     * Next line contains N pairs of letters, each pair separated by a single whitespace,
     * describing infected genome.
     * Following line contains number of bacteria's lengths D, 0 < D ≤ 40.
     * Last line contains numbers di, 1 ≤ di ≤ 40, where 0< i ≤ D, each separated by a whitespace 
     * and representing a bacteria's length.
     * 
     * Output
     * First line contains best picobot program (or 0 if it cannot be determined).
     * If length of genome does not exceed 10000 characters, also write number of distinct ways
     * a healthy genome can be altered by a virus (at least one gene must be altered).
     * If that number has more than 50 digits, write down 50 last ones.
     */
    public sealed class Pikoboty : ProblemBase
    {
        protected override string Input => "7 AA\r\nBC BC FD GF FG FD FD\r\n3\r\n1 2 3";

        protected override string Output => "FD\r\n78124";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string ok = input[0].Split(' ')[1];
            var unhealthy = input[1].Split(' ').ToArray();
            Dictionary<string, int> counts = [];
            int[] values = input[3].Split(' ').Select(c => int.Parse(c)).ToArray();
            foreach (var v in values)
            {
                string healthy = string.Join("", Enumerable.Range(0, v).Select(s => ok));
                for (int i = 0; i + v <= unhealthy.Length; i++)
                {
                    string key = string.Join("", Enumerable.Range(i, v).Select(s => unhealthy[s]));
                    if (key == healthy)
                        continue;
                    if (!counts.ContainsKey(key))
                        counts.Add(key, 1);
                    else
                        counts[key]++;
                }
            }
            List<(string, int, int)> results = [];
            foreach (var k in counts.Keys)
            {
                results.Add((k, counts[k], counts[k] * (k.Length >> 1)));
            }
            results.Sort((a, b) =>
            {
                var c = -a.Item3.CompareTo(b.Item3);
                return c == 0 ? -a.Item2.CompareTo(b.Item2) : c;
            });
            output.Add(results.Count == 1 || results[0].Item3 > results[1].Item3 || results[0].Item2 > results[1].Item2
                ? results[0].Item1 : "0");
            if (unhealthy.Length <= 10000)
            {
                int min = values.Min();
                int counter = 0;
                for (int k = 0; k <= unhealthy.Length - min; k++)
                {
                    if (Enumerable.Range(0, min).Any(j => unhealthy[k + j] != ok))
                        counter++;
                }
                List<int> result = [counts.Keys.Count(c => c.Length == 2) + 1];
                while (result[0] > 9)
                {
                    result = [result[0] / 10, result[0] % 10];
                }
                List<int> factor = new(result);
                for (int j = 0; j < min * counter - 1; j++)
                {
                    List<int> newRes = [0];
                    int carry = 0;
                    for (int k = result.Count - 1; k >= 0; k--)
                    {
                        for (int l = factor.Count - 1; l >= 0; l--)
                        {
                            int index = k - (factor.Count - l - 1);
                            while (newRes.Count <= index)
                                newRes.Insert(0, 0);
                            newRes[index] += result[k] * factor[l] + carry;
                            carry = newRes[index] / 10;
                            newRes[index] %= 10;
                        }
                        int index2 = k - factor.Count;
                        while (carry > 0)
                        {
                            if (index2 >= 0)
                            {
                                newRes[index2] += carry;
                                carry = newRes[index2] / 10;
                                newRes[index2] %= 10;
                                index2--;
                            }
                            else
                            {
                                newRes.Insert(0, carry);
                                carry = 0;
                            }
                        }
                    }
                    result = newRes;
                }
                int x = result.Count - 1;
                result[x]--;
                while (x >= 0 && result[x] < 0)
                {
                    result[x] += 10;
                    result[x - 1]--;
                    x--;
                }
                if (result[0] == 0)
                    result.RemoveAt(0);
                int toSkip = Math.Max(result.Count - 50, 0);
                output.Add(string.Join("", result.Skip(toSkip).Take(50)));
            }
        }
    }
}
