namespace OPSS
{
    /* Difficulty: 5/5
     * Consider a game, where you take an integer X1, reverse its digits, and add the two together.
     * Repeat this process for a resulting sum.
     * You're given some number Z. Find a smallest number that allows you to reach Z in a finite
     * number of steps.
     * 
     * Input
     * First line contains number of data sets N (1 ≤ N ≤ 100).
     * Each data set consists of a single line containing a single integer Z (0 ≤ Z ≤ 10^9).
     * 
     * Output
     * Smallest integer X lesser than Z, which, starting from, Z can be achieved in finite number of steps.
     * If there is no such number, write -1 instead.
     */
    public sealed class Zabawa : ProblemBase
    {
        protected override string Input => "4\r\n121\r\n976071668\r\n5104\r\n187876";

        protected override string Output => "7\r\n1\r\n184\r\n-1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                List<int[]> candidates = [];
                candidates.Add([.. input[i].Select(c => c - '0')]);
                int result = int.MaxValue;
                while (candidates.Any())
                {
                    int[] splits = candidates[^1];
                    candidates.RemoveAt(candidates.Count - 1);
                    int val = 0;
                    foreach (var s in splits)
                        val = 10 * val + s;
                    if (splits.Length == 2 && splits[0] == splits[1] && splits[0] % 2 == 1 && splits[0] > 1)
                    {
                        splits = [9 + splits[0]];
                    }
                    if (splits.Length == 1)
                    {
                        while (splits[0] % 2 == 0)
                            splits[0] >>= 1;
                        val = splits[0];
                    }
                    if (splits[0] == 1 && splits[^1] != 1)
                    {
                        splits[1] += 10;
                        splits[^1] += 10;
                        splits[^2]--;
                        splits = splits.Skip(1).ToArray();
                    }
                    var half = Enumerable.Range(0, splits.Length >> 1);
                    for (int k = 0; k < splits.Length >> 1; k++)
                    {
                        if (splits[k] > 18)
                        {
                            splits[k]--;
                            splits[k + 1] += 10;
                        }
                        var diff = splits[k] - splits[splits.Length - k - 1];
                        if (diff == 0)
                            continue;
                        if (diff >= 10)
                        {
                            splits[splits.Length - k - 1] += 10;
                            splits[splits.Length - k - 2]--;
                            diff = splits[k] - splits[splits.Length - k - 1];
                        }
                        if (diff == 1)
                        {
                            splits[k]--;
                            splits[k + 1] += 10;
                        }
                        else if (diff == -1 && k > 0)
                        {
                            splits[k - 1]--;
                            splits[k] += 10;
                            if (k == 1 && splits[0] == 0)
                            {
                                splits = splits.Skip(1).ToArray();
                                k = -1;
                                continue;
                            }
                        }
                        else if (diff != 0)
                            break;
                    }
                    if (splits.Length == 1 ||
                        (splits.Length % 2 == 1 && splits[splits.Length >> 1] % 2 == 1) ||
                        !Enumerable.Range(0, splits.Length >> 1).All(k => splits[k] == splits[splits.Length - k - 1]))
                    {
                        result = Math.Min(val, result);
                        continue;
                    }
                    if (splits.Length % 2 == 0 && half.All(s => splits[s] == 1))
                        splits = Enumerable.Range(0, splits.Length - 1).Select(s => s % 2 == 0 ? 11 : 0).ToArray();
                    int[] digits = new int[splits.Length];
                    (int, int)[] options = new (int, int)[(splits.Length >> 1) + (splits.Length % 2)];
                    int start = Math.Max(1, splits[0] - 9);
                    options[0] = (start, Math.Min(splits[0], 9));
                    for (int k = 1; k < splits.Length >> 1; k++)
                    {
                        start = Math.Max(0, splits[k] - 9);
                        options[k] = (start, Math.Min(splits[k], 9));
                    }
                    if (digits.Length % 2 == 1)
                    {
                        start = splits[splits.Length >> 1] >> 1;
                        options[splits.Length >> 1] = (start, start);
                    }
                    int[] counter = options.Select(o => o.Item1).ToArray();
                    while (counter[0] <= options[0].Item2)
                    {
                        for (int k = 0; k < options.Length; k++)
                        {
                            digits[k] = counter[k];
                            digits[digits.Length - k - 1] = splits[k] - digits[k];
                        }
                        candidates.Add(digits.ToArray());
                        counter[^1]++;
                        int k2 = counter.Length - 1;
                        while (k2 > 0 && counter[k2] > options[k2].Item2)
                        {
                            counter[k2] = options[k2].Item1;
                            k2--;
                            counter[k2]++;
                        }
                    }
                }
                output.Add(result != int.Parse(input[i]) ? result.ToString() : "-1");
            }
        }
    }
}
