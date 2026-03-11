namespace OPSS
{
    /* Difficulty: 2/5
     * You're given a sequence f, where:
f0 = 0,
fn = n^3 * (fn-1+1) + n^2, n > 0
    
    Let n = 54128. 
    fn mod 3331 = ???
    
    Find the answer for any n.

Input
    First line contains number of data sets C, 1 ≤ C ≤ 5.
    Each data set consists of a single natural number n, 0 ≤ n ≤ 100000000.
Wyjście
C lines, each containing value of fn mod 3331 for respective n values.
     */
    public sealed class PrehistorycznyKomputer : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n10\r\n225";

        protected override string Output => "28\r\n254\r\n959";

        const int K = 3331;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> tab = [0];
            for(int i = 1; i < K; i++)
            {
                tab.Add((i * i % K) * (i * (tab[tab.Count - 1] + 1) + 1) % K);
            }
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int c = int.Parse(input[i]);
                output.Add(tab[c % K].ToString());
            }
        }
    }
}
