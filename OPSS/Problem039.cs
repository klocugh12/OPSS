using System.Globalization;

namespace OPSS
{
    /* Difficulty: 1/5
     * Find out value for a given polynomial for a given argument.
     * 
     * Input:
     * First line contains number of data sets d, 0 < d ≤ 100.
     * Each data set conists of three rows.
     * First row contains argument t.
     * Second line contains degree of a polynomial N, 0 ≤ N ≤ 100000
     * Third line contains N + 1 coefficients of polynomial starting from the highest power.
     * For example W(t) = a0 + a1 * t + a2 * t^2 + a3 * t^3 is described as: a3 a2 a1 a0.
     * Both t and coefficients are real numbers with up to 3 fractional digits.
     * 
     * Output
     * d numbers, each representing value W(t) for a respective data set, rounded to 3 fractional digits.
     * All values will fit inside standard floating point type variable.
     */
    public sealed class DrJudym : ProblemBase
    {
        protected override string Input => "3\r\n0.100\r\n4\r\n-0.700 3.000 0.000 8.000 -5.000\r\n3.000\r\n3\r\n3.000 0.000 0.000 10000.000\r\n128.000\r\n2\r\n1.000 -1.000 1.000";

        protected override string Output => "-4.197\r\n10081.000\r\n16257.000";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int d = int.Parse(input[0]);
            for(int i = 0; i < d; i++)
            {
                double t = double.Parse(input[3 * i + 1], CultureInfo.InvariantCulture);
                double[] coeffs = input[3 * (i + 1)].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                double W = 0;
                for (int j = 0; j < coeffs.Length; j++)
                {
                    W = W * t + coeffs[j];
                }
                output.Add(W.ToString("#.000").Replace(",", "."));
            }
        }
    }
}
