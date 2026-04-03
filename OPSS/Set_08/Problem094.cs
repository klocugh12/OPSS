namespace OPSS
{
    /* Time limit: 1s, Memory limit: 16MB, Difficulty: 2/5
     * Installing software packages comes with its own challenges. Notable one is resolving dependencies.
     * Each dependency is described by a pair of different numbers separated by a whitespace.
     * Pair A B means package A must be installed before package B.
     * For instance, dependency chain described as follows:
     * 1 2
     * 2 3
     * 2 4
     * 
     * Can be resolved in either of following orders:
     * 1 2 3 4
     * 1 2 4 3
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 10.
     * First line of each data set contains two integers separated by a whitespace.
     * First value is a number of packages to install N, 1 ≤ N ≤ 100000.
     * Second value is a number of dependencies Z, 0 ≤ Z ≤ 100000. 
     * Following Z lines each contain a pair of integers separated by a whitespace A B, 1 ≤ A, B ≤ N,
     * where package B must be installed after package A. 
     * There are no circular dependencies in input data.
     * 
     * Output
     * D lines, each containing order of packages to install. Indexes of packages must be 
     * separated by a whitespace. If there are multiple solutions, write smallest one lexicographically.
     */
    public sealed class Zaleznosci : ProblemBase
    {
        protected override string Input => "2\r\n5 6\r\n1 2\r\n1 4\r\n3 1\r\n3 4\r\n4 5\r\n2 5\r\n4 3\r\n1 2\r\n2 3\r\n3 4";

        protected override string Output => "3 1 2 4 5\r\n1 2 3 4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                Dictionary<string, List<string>> dependencies = [];
                for (int k = 1; k <= splits[0]; k++)
                    dependencies.Add(k.ToString(), []);
                j++;
                for (int k = 0; k < splits[1]; k++)
                {
                    var splits2 = input[j].Split(' ');
                    dependencies[splits2[1]].Add(splits2[0]);
                    j++;
                }
                List<string> order = [];
                while (dependencies.Count > 0)
                {
                    string toRemove = dependencies.Keys.First(t => dependencies[t].Count == 0);
                    foreach (var dep in dependencies.Values.Where(l => l.Contains(toRemove)))
                        dep.Remove(toRemove);
                    dependencies.Remove(toRemove);
                    order.Add(toRemove);
                }
                output.Add(string.Join(" ", order));
            }
        }
    }
}
