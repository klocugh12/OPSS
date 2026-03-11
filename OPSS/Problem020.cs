namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * Find last digit of 3^n.
     * 
     * Input
     * First line contains number of data sets d, 1 ≤ d ≤ 10.
     * Following d lines each contain a data set.
     * Each data set consists of a single nonnegative integer n, 0 ≤ n < 10200.
     * 
     * Output
     * d lines, each containing result for its respective data set.
     */
    public sealed class Potega : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n3\r\n2005";

        protected override string Output => "9\r\n7\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string[] digits = ["1", "3", "9", "7"];
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                output.Add(digits[a % 4]);
            }
        }
    }
}
