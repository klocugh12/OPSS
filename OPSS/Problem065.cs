namespace OPSS
{
    /* Difficulty: 4/5
     * Szkrable is a popular and complex word game.
     * It has a predefined dictionary of legal three-letter words (triplets).
     * A word is legal only if each triplet of consecutive characters in it forms a legal word
     * from the dictionary. Each triplet has specific number of points assigned in the dictionary.
     * Score for a final word is equal to sum of individual scores for each triplet contained in it.
     * If there are repetitions of same triplet in the word, each occurence is scored separately.
     * All triplet consist of uppercase English letters, and all scores assigned are positive.
     * As a special rule, one- or two-letter words are allowed, if they are themselves contained
     * in any of triplet rom the dictionary, but they are worth 0 points each.
     * 
     * Your goal is to find, how much can a score for a given word be reduced by adding or removing
     * letters from it, as long as modified words is still legal and first and last letters remain unchanged.
     * 
     * Input
     * First line contains number of data sets n (1 ≤ n ≤ 1000).
     * Following n lines each contain a string T and number K separated by a whitespace.
     * T is a triplet from the dictionary, K is number of points assigned to it (1 ≤ K ≤ 1000).
     * Following line contains a single number q (1 ≤ q ≤ 1000).
     * Following q lines each contain non-empty legal words. Each word is no more than 1000 characters long.
     * 
     * Output
     * q lines, each containing two numbers separated by a whitespace.
     * First number is a score given for original word.
     * Second number is a maximum amount of points you can lose by modifying original word,
     * as long as it is still legal and first and last letter are unchanged.
     */
    public sealed class Szkrable : ProblemBase
    {
        protected override string Input => "9\r\nDAB 10\r\nABC 20\r\nBCD 25\r\nCDZ 100\r\nADZ 1000\r\nRCB 40\r\nRCD 1000\r\nCBA 30\r\nCDA 50\r\n3\r\nADZ\r\nRCDABC\r\nRCDZ";

        protected override string Output => "1000 855\r\n1080 1080\r\n1100 0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            Dictionary<string, int> dict = new();
            for (int i = 1; i <= n; i++)
            {
                var s = input[i].Split(' ');
                dict.Add(s[0], int.Parse(s[1]));
            }
            int q = int.Parse(input[n + 1]);
            for (int i = 0; i < q; i++)
            {
                var s = input[n + i + 2];
                int sum1 = 0;
                for (int k = 0; k < s.Length - 2; k++)
                    sum1 += dict[s.Substring(k, 3)];
                var join = string.Join("", s[0], s[s.Length - 1]);
                if (dict.Keys.Any(k => ((s[0] == s[s.Length - 1] && k.Contains(s[0]))) || k.Contains(join)))
                {
                    output.Add($"{sum1} {sum1}");
                    continue;
                }
                var candidates = dict.Keys.Where(k => k.StartsWith(s[0]) && !s.StartsWith(k)).Select(k => (k, dict[k])).ToList();
                for (int j = 0; j < candidates.Count; j++)
                {
                    while (j < candidates.Count && !candidates[j].k.EndsWith(s[s.Length - 1]))
                    {
                        var appends = dict.Keys.Where(k =>
                            k.StartsWith(candidates[j].k.Substring(candidates[j].k.Length - 2, 2)) && !candidates[j].k.StartsWith(k));
                        foreach (var key in appends)
                        {
                            if (candidates[j].Item2 + dict[key] < sum1)
                                candidates.Insert(j + 1, (candidates[j].k + key[2], candidates[j].Item2 + dict[key]));
                            {
                                if (key[2] == s[s.Length - 1])
                                {
                                    candidates.RemoveAll(c2 => c2.Item2 >= candidates[j + 1].Item2 && c2.k != candidates[j + 1].k);
                                }
                            }
                        }
                        candidates.RemoveAt(j);
                    }
                }
                if (!candidates.Any())
                {
                    output.Add($"{sum1} 0");
                    continue;
                }
                output.Add($"{sum1} {Math.Max(sum1 - candidates.Min(c2 => c2.Item2), 0)}");
            }
        }
    }
}
