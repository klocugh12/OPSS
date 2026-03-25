using System.Text;

namespace OPSS
{
    /* Difficulty: 2/5
     * Find smallest positive number, whose product of digits is equal to N.
     * 
     * Input
     * A single number N (0 ≤ N ≤ 10^9).
     * 
     * Output:
     * A number meeting the criteria or -1 if no such number exists.
     */
    public sealed class IloczynCyfr : ProblemBase
    {
        protected override string Input => "10";

        protected override string Output => "25";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            if (N < 10)
            {
                output.Add(input[0]);
                return;
            }
            Dictionary<int, int> factors = new() { { 2, 0 }, { 3, 0 }, { 5, 0 }, { 7, 0 } };
            foreach (var p in factors.Keys)
            {
                while (N % p == 0)
                {
                    factors[p]++;
                    N /= p;
                }
            }
            if (N > 1)
            {
                output.Add("-1");
                return;
            }
            StringBuilder sb = new();
            if ((factors[2] % 3) - (factors[3] % 2) == 1)
            {
                sb.Append(2);
                factors[2]--;
            }
            if (factors[3] % 2 == 1 && factors[2] % 3 == 0)
            {
                sb.Append(3);
                factors[3]--;
            }
            if (factors[2] % 3 == 2)
            {
                sb.Append(4);
                factors[2] -= 2;
            }
            sb.Append('5', factors[5]);
            sb.Append('7', factors[7]);
            sb.Append('8', factors[2] / 3);
            sb.Append('9', factors[3] >> 1);
            output.Add(sb.ToString());
        }
    }
}
