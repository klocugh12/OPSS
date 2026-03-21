namespace OPSS
{
    /* Difficulty: 2/5
     * Given list of numbers, find number of digits of their product.
     * 
     * Input
     * First line contain number of data sets C, 1 ≤ C ≤ 5000.
     * Each data set contains two lines.
     * First line of each data set consists of number of factors to multiply N, 1 ≤ N < 1000.
     * Each factor is an positive integer with maximum 3 digits.
     * 
     * Output
     * C lines, each containing number of digits in respective product using decimal system.
     */
    public sealed class Multyplikator : ProblemBase
    {
        protected override string Input => "2\r\n2\r\n2 5\r\n5\r\n10 10 10 10 5";

        protected override string Output => "2\r\n5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                output.Add(Math.Floor(input[i << 1].Split(' ').Select(s => Math.Log10(int.Parse(s))).Sum() + 1.0).ToString());
            }
        }
    }
}
