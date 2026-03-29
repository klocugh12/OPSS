using System.Text;

namespace OPSS
{
    /* Difficulty: 2/5
     * 
     * A balanced number is a number, which has same number of odd and even divisors.
     * For a given N, find smallest number greater than N.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100. 
     * Each data set contains a natural number N. N has at most 200 digits.
     * 
     * Output
     * C lines, each containing smallest balanced number greater than respective N numbers.
     */
    public sealed class LiczbyWywazone : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n2";

        protected override string Output => "2\r\n6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                StringBuilder N = new(input[i]);
                int k = int.Parse(input[i].Substring(Math.Max(input[i].Length - 2, 0)));
                int toAdd = 4 - ((k + 2) % 4);
                k = N.Length - 1;
                bool carry = true;
                while (k >= 0 && carry)
                {
                    int x = (N[k] - '0') + toAdd;
                    carry = x > 9;
                    N[k] = (char)((x % 10) + '0');
                    k--;
                }
                if (carry)
                    N.Insert(0, '1');
                output.Add(N.ToString());
            }
        }
    }
}
