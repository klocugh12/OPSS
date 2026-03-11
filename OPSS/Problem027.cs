namespace OPSS
{
    /* Difficulty: 3/5
     * 
     * You have unlimited amount of 2x1 domino tiles of size.
     * Find number of ways you can tile a 3xN rectangle using those tiles.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 2000.
     * Each data set consists of a single number N, 1 ≤ N ≤ 100000.
     * 
     * Output
     * C lines, each containing P mod 10^6, where P is number of ways to tile a 3xN rectangle.
     */
    public sealed class Domino : ProblemBase
    {
        protected override string Input => "2\r\n2\r\n4";

        protected override string Output => "3\r\n11";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int> dominos = [3];
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                if (a % 2 == 1)
                {
                    output.Add("0");
                    continue;
                }
                a >>= 1;
                for (int j = dominos.Count; j < a; j++)
                {
                    dominos.Add(0);
                    for (int k = 0; k < j; k++)
                    {
                        dominos[j] += (k == 0 ? 3 : 2) * dominos[j - k - 1];
                    }
                    dominos[j] += 2;
                    dominos[j] %= 1_000_000;
                }
                output.Add(dominos[a - 1].ToString());
            }
        }
    }
}
