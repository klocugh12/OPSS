namespace OPSS
{
    /* Difficulty: 2/5
     * You're given groups of samples, which are evaluated according to 10 parameters each.
     * Find number of samples, which meet given evaluation criteria.
     * 
     * Input
     * First line contains number of sample groups k (1 ≤ k ≤ 10000).
     * Each of the following k lines contains 11 numbers separated by a whitespace.
     * First 10 numbers x1, x2, .., x10, 0 ≤ xi ≤ 100, represent values of each of 10 parameters.
     * Eleventh number y, 0 ≤ y ≤ 1000 represents number of samples in a group.
     * Following line contains number of queries m (1 ≤ m ≤ 10000).
     * Following m lines each contain 10 integer numbers a1 to a10 (-1 ≤ ai ≤ 100) separated by a whitespace.
     * Each of those lines reprents a single query. If ai=-1, any value of i-th parameter is accepted,
     * otherwise i-th parameter must be equal to given ai.
     * 
     * Output
     * m lines, each containing number of samples, which satisfy all criteria in a respective query.
     */
    public sealed class MarsjanskieSkaly : ProblemBase
    {
        protected override string Input => "5\r\n10 1 1 4 5 6 7 8 9 1 100\r\n1 2 4 4 5 5 5 5 5 1 200\r\n2 2 4 4 6 6 6 8 5 2 300\r\n10 2 1 9 5 5 5 5 5 3 400\r\n10 2 1 9 5 5 5 5 5 3 500\r\n3\r\n10 -1 -1 9 -1 -1 -1 -1 -1 -1\r\n-1 2 -1 -1 5 -1 -1 -1 -1 -1\r\n1 2 4 4 -1 5 5 5 5 -1";

        protected override string Output => "900\r\n1100\r\n200";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int k = int.Parse(input[0]);
            List<int[]> samples = [];
            for (int i = 1; i <= k; i++)
            {
                samples.Add(input[i].Split(' ').Select(s => int.Parse(s)).ToArray());
            }
            k = int.Parse(input[k + 1]);
            for (int i = 0; i < k; i++)
            {
                List<(int, int)> pattern = [];
                int j = 0;
                foreach (var j2 in input[samples.Count + i + 2].Split(' ').Select(s => int.Parse(s)))
                {
                    if (j2 >= 0)
                        pattern.Add((j, j2));
                    j++;
                }
                int sum = 0;
                foreach (var s in samples)
                {
                    if (pattern.All(p => s[p.Item1] == p.Item2))
                        sum += s[^1];
                }
                output.Add(sum.ToString());
            }
        }
    }
}
