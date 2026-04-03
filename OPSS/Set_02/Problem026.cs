namespace OPSS
{
    /* Time limit: 2.5s, Memory limit: 16MB, Difficulty: 3/5
     * 
     * An AB-tree is a complete tree, whose nodes are words made using letters {a, b}.
     * Its root is an empty word "0". For any word w in a tree, it has two children 
     * { xw: x in {a, b}}, i.e two words being result of appending one of {a, b} letters
     * at the beginning of word w. All words in the tree are distinct.
     * An AB-tree has height h, if it contains words of length h. From a complete tree
     * we want to remove certain words with all their children.
     * Find number of words remaining in the tree after removing said words with their children.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 10.
     * First line of each data contains height of a tree H, 1 ≤ H ≤ 30.
     * Second line of each data set contains number of words to remove N, 0 ≤ N ≤ 50000.
     * Following N lines each contain a single word to remove from a tree.
     * All words are contained within a tree and empty word is not allowed.
     * 
     * Output
     * C lines, each containing number of words remaining in a tree for each respective data set.
     */
    public sealed class ABDrzewo : ProblemBase
    {
        protected override string Input => "1\r\n2\r\n3\r\nb\r\nab\r\naa";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                int H = int.Parse(input[j]);
                int c = (1 << (H + 1)) - 1;
                j++;
                int N = int.Parse(input[j]);
                j++;
                List<string> strings = [];
                for (int k = 0; k < N; k++)
                {
                    if (!strings.Any(s => input[j].EndsWith(s)))
                        strings.Add(input[j]);
                    j++;
                }
                strings.Sort((a, b) => a.Length.CompareTo(b.Length));
                for (int k = 0; k < strings.Count; k++)
                {
                    for (int l = k + 1; l < strings.Count; l++)
                        if (strings[l].EndsWith(strings[k]))
                        {
                            strings.RemoveAt(l);
                            l--;
                        }
                }
                foreach (var s in strings)
                {
                    c -= (1 << (H - s.Length + 1)) - 1;
                }
                output.Add(c.ToString());
            }
        }
    }
}
