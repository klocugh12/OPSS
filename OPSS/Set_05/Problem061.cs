namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * You're given a sequence of parentheses ().
     * Find the number of brackets that need to be added in order for sequence to be valid.
     * A valid sequence of brackets is any of the following:
     * ● ()
     * ● (X), where X is another valid sequence
     * ● XY, where X and Y are valid sequences.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * Each data set consists of a single line containing nonempty string with a sequence
     * of brackets. String is no more than 1000 characters long.
     * 
     * Output
     * C lines, each containing number of brackets to add to each string to make bracketing valid.
     */
    public sealed class MaszynaDrukarska : ProblemBase
    {
        protected override string Input => "2\r\n)\r\n(()))()(()))";

        protected override string Output => "1\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                int l = 0, p = 0;
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '(')
                        l++;
                    else
                        p++;
                }
                output.Add(Math.Abs(l - p).ToString());
            }
        }
    }
}
