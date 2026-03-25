using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * Find a sum of two arbitrarily long integers.
     * 
     * Input
     * First line contains a single number N (1 ≤ N ≤ 1000000). 
     * Next two lines each contain N digits of numbers to add.
     * Digits are separated by whitespaces. Shorter number is padded with leading zeros.
     * Both numbers are greater than zero and length of their sum does not exceed N digits.
     * 
     * Output
     * A single line containing sum of two numbers.
     */
    public sealed class SuperdlugaSuma : ProblemBase
    {
        protected override string Input => "4\r\n0 4\r\n4 2\r\n6 8\r\n3 7";

        protected override string Output => "4750";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int current = 0, prev = 0;
            bool nonZero = false;
            StringBuilder result = new();
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                prev = current;
                current = int.Parse(splits[0]) + int.Parse(splits[1]);
                if (current > 9)
                {
                    result.Append(prev + 1);
                    current -= 10;
                }
                else
                {
                    if (nonZero)
                        result.Append(prev);
                    else
                        nonZero = current > 0;
                }
            }
            result.Append(current);
            output.Add(result.ToString());
        }
    }
}
