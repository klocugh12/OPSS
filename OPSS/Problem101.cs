using System.Text;

namespace OPSS
{
    /* Difficulty: 3/5
     * Write a program to add common fractions. Assume the following:
     * a fractions can be represented as "a", "a/b/c" or "b/c" where a, b, c are nonnegative
     * integers. "a" is an integer, "a/b/c" is equivalent to a + b/c, and "b/c" is a common
     * fraction. Fractions need not be reduced. Write output in the same format.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 1000.
     * Each data set contains two fractions represented in format described above,
     * separated by a single + sign. Assume 0 ≤ a ≤ 10000, 0 < b < c ≤ 10000.
     * 
     * Output
     * D lines, each containing reduced sum of provided fractions in a format described above.
     */
    public sealed class Ulamki : ProblemBase
    {
        protected override string Input => "2\r\n10+7/8\r\n1/2/3+1/1/2";

        protected override string Output => "10/7/8\r\n3/1/6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            for(int i = 1; i <= D; i++)
            {
                var splits = input[i].Split('+');
                var val1 = splits[0].Split('/').Select(s => int.Parse(s)).ToArray();
                var val2 = splits[1].Split('/').Select(s => int.Parse(s)).ToArray();
                int[] results = [0, 0, 1];
                if (val1.Length != 2)
                    results[0] = val1[0];
                if(val1.Length > 1)
                {
                    results[1] = val1[^2];
                    results[2] = val1[^1];
                }
                if (val2.Length != 2)
                    results[0] += val2[0];
                if (val2.Length > 1)
                {
                    results[1] = results[1] * val2[^1] + val2[^2] * results[2];
                    results[2] *= val2[^1];
                    int whole = results[1] / results[2];
                    results[0] += whole;
                    results[1] -= whole * results[2];
                }
                int b = results[1], c = results[2];
                while(b > 0)
                {
                    int temp = b;
                    b = c % b;
                    c = temp;
                }
                if(c > 1)
                {
                    results[1] /= c;
                    results[2] /= c;
                }
                StringBuilder sb = new();
                if (results[0] > 0)
                    sb.Append(results[0]);
                if (results[1] > 0)
                {
                    if (sb.Length > 0)
                        sb.Append("/");
                    sb.Append($"{results[1]}/{results[2]}");
                }
                output.Add(sb.ToString());
            }
        }
    }
}
