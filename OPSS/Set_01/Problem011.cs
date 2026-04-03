namespace OPSS
{
    /* Time limit: 1s, Memory limit: 1MB, Difficulty: 3/5
     * 
     * Consider a base-K N-digit number.
     * Such number is KN correct if it has no adjacent zeros.
     * 
     * Examples:
     * 1010230 is 7-digit KN-correct (n=7, k=4).
     * 1000198 is not KN-correct (adjacent zeros).
     * 0121235 is 6-digit (NOT 7-digit) KN-correct (n=7, k=7).
     * Given K and N, find number of KN-correct numbers, where 2 ≤ K ≤ 10; 2 ≤ N; 4 ≤ N+K ≤ 18.
     * Input
     * A single line containing two numbers N and K separated by a whitespace.
     * 
     * Output
     * Number of KN-correct numbers.
     */
    public sealed class KNLiczby : ProblemBase
    {
        protected override string Input => "2\r\n10";

        protected override string Output => "90";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int K = int.Parse(input[0]);
            int N = int.Parse(input[1]);
            List<(int, int)> tree = [(N - 1, N - 1)];
            for (int i = 1; i < K; i++)
            {
                for (int j = 0; j < tree.Count; j++)
                {
                    if (tree[j].Item1 == 1)
                        tree[j] = (K - 1, tree[j].Item2);
                    else
                    {
                        tree.Insert(j, (1, tree[j].Item2));
                        j++;
                    }
                    tree[j] = (K - 1, tree[j].Item2 * (N - 1));
                }
            }
            output.Add(tree.Sum(i => i.Item2).ToString());
        }
    }
}
