namespace OPSS
{
    /* Difficulty: 2/5
     * Alice threw a birthday party, baked a cake and invited her guests.
     * To slice a cake, she made K straight cuts to make as many pieces as possible, 
     * so that each guest gets a single piece.
     * 
     * For a given number of guests N find minimum number of cuts needed to provide each guest with a piece.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 65535.
     * Each of lines contains a single number N, 1 ≤ N < 2^31, equal to number of guests invited.
     * 
     * Output
     * C lines, each containing number of cuts K necessary to slice the cake.
     */
    public sealed class Tort : ProblemBase
    {
        protected override string Input => "4\r\n1\r\n2\r\n4\r\n7";

        protected override string Output => "0\r\n1\r\n2\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                double N = (double)int.Parse(input[i]);
                output.Add(Math.Ceiling((Math.Sqrt(((N - 1) * 8.0) + 1) - 1.0) / 2.0).ToString());
            }
        }
    }
}
