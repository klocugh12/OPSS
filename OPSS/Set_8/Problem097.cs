namespace OPSS
{
    /* Difficulty: 2/5
     * Given an integer c, find pairs of nonnegative integers a, b such as a^2 - b^2 = c.
     * If there are multiple such pairs, pick one with smallest difference between a and b.
     * If there are still multiple candidates, pick one with smallest value of b.
     * 
     * Input
     * First line contains number of data sets L, 1 ≤ L ≤ 60000.
     * Each data set contains a single number c, 0 ≤ c ≤ 5∙10^6.
     * 
     * Output
     * L lines, each containing b value satisfying described conditions, or -1 if no solution exists..
     */
    public sealed class Rachmistrz : ProblemBase
    {
        protected override string Input => "2\r\n133\r\n28900";

        protected override string Output => "66\r\n7224";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int L = int.Parse(input[0]);
            for (int i = 1; i <= L; i++)
            {
                int c = int.Parse(input[i]);
                if (c == 0)
                    output.Add("0");
                else if (c % 2 == 1)
                    output.Add((c >> 1).ToString());
                else if (c % 4 == 2)
                    output.Add("-1");
                else
                    output.Add((((c >> 1) - 1) >> 1).ToString());
            }
        }
    }
}
