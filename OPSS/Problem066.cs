namespace OPSS
{
    /* Difficulty: 4/5
     * A text search should work quickly, but also help users in case they make a typing error.
     * In case a user types a word that is not found, a some alternative should be given, sorted descending
     * by similarity to original word.
     * A similarity between two words H1 and H2 is described by minimum number of letters to add, 
     * remove or replace in a word H1 to get H2 instead. The lower the number, the more similar words are.
     * Example: to get word 'MAREK' from 'ALA' you need 4 operations:
     * ● Add 'M' at the beginning, resulting in 'MALA'.
     * ● Replace 'L' with 'R', resulting in 'MARA'.
     * ● Replace 'A' with 'E', resulting in 'MARE'.
     * ● Add 'K' at the end, resulting in 'MAREK'.
     * For word 'OLA' it is 5 operations: remove 'O', replace 'L' with 'M', then add 'R', 'E' and 'K' at the end.
     * Therefore for word 'MAREK' word 'ALA', should appear before 'OLA' as a suggestion.
     * Given a certain dictionary S and word T, sort S in order from most to least similar to T,
     * i.e., from the word needing least operations to transform to T, to the one needing the most.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 30.
     * First line of each data set contains a given word T.
     * Second line of data set contains number of words in a dictionary N, 1 ≤ N ≤ 20.
     * Following N lines each contain a word from a dictionary. Only English uppercase characters are allowed.
     * Each word is from 1 to 200 characters long.
     * 
     * Output
     * D lines, each containing N words separated by a whitespace.
     * Words are sorted in order of decreasing similarity to word T from a respective data set.
     * If they are equally similar, write them down in order they appeared in the input.
     */
    public sealed class Gugle : ProblemBase
    {
        protected override string Input => "2\r\nMAREK\r\n4\r\nOLA\r\nHELA\r\nALA\r\nKERAM\r\nDAREK\r\n5\r\nFOO\r\nABCDEFGH\r\nDAREEC\r\nFOOBAR\r\nKERAD";

        protected override string Output => "ALA KERAM OLA HELA\r\nDAREEC KERAD FOO FOOBAR ABCDEFGH";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= D; i++)
            {
                string pattern = input[j];
                j++;
                int c = int.Parse(input[j]);
                j++;
                Dictionary<string, int> candidates = [];
                for(int k = 0; k < c; k++)
                {
                    candidates.Add(input[j], Math.Max(pattern.Length, input[j].Length));
                    j++;
                }
                foreach(var s in candidates.Keys)
                {
                    List<(int, int, int)> anchors = [];
                    for(int k = 0; k < pattern.Length; k++)
                    {
                        for(int l = 0; l < s.Length; l++)
                        {
                            int length = -1, k2 = k, l2 = l;
                            while (k2 < pattern.Length && l2 < s.Length && pattern[k2] == s[l2])
                            {
                                length++;
                                k2++;
                                l2++;
                            }
                            if (length >= 0)
                            {
                                anchors.Add((k, l, length));
                                l += length;
                            }
                        }
                    }
                    foreach(var anchor in anchors)
                    {
                        int similarity = Math.Max(anchor.Item2, anchor.Item1);
                        similarity += Math.Max(s.Length - (anchor.Item2 + anchor.Item3 + 1), pattern.Length - (anchor.Item1 + anchor.Item3 + 1));
                        candidates[s] = Math.Min(similarity, candidates[s]);
                    }
                }
                output.Add(string.Join(" ", candidates.OrderBy(x => x.Value).Select(s => s.Key)));
            }
        }
    }
}
