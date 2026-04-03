namespace OPSS
{
    /* Time limit: 1s, Memory limit: 1.5MB, Difficulty: 3/5
     * A frog leaps along the path consisting of n steps, starting from position 1 and ending at position n.
     * A frog can only move forward. Each leap traverses at least kmin and no more than kmax steps.
     * Find number of ways a frog can traverse whole path, assuming each leap is at least as long as previous.
     * 
     * Input
     * First line contains number of data sets m, 1 ≤ m ≤ 100.
     * Following m lines contain one data set each. Each data set consists of three numbers
     * n, kmin, kmax, each separated by a whitespace. (2 ≤ n ≤ 1000, 1 ≤ kmin ≤ kmax ≤ 1000).
     * 
     * Output
     * m lines, where i-th line contains number of ways a frog can reach final step for i-th data set.
     * This number is no greater than 2^31.
     */
    public sealed class Zabka : ProblemBase
    {
        protected override string Input => "4\r\n10 2 5\r\n15 5 8\r\n20 1 20\r\n22 2 7";

        protected override string Output => "5\r\n2\r\n490\r\n72";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int m = int.Parse(input[0]);
            for (int i = 1; i <= m; i++)
            {
                var splits = input[i].Split(' ');
                int n = int.Parse(splits[0]), kmin = int.Parse(splits[1]), kmax = Math.Min(n - 1, int.Parse(splits[2]));
                int[] tab = new int[n];
                for (int k = kmin; k <= kmax; k++)
                {
                    tab[k]++;
                    for (int j = k + 1; j < n; j++)
                        tab[j] += tab[j - k];
                }
                output.Add(tab[n - 1].ToString());
            }
        }
    }
}
