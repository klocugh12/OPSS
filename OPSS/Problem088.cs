namespace OPSS
{
    /* Difficulty: 3/5
     * You're given two lines, a quarter of certain cosine wave and half of certain tangent graph 
     * (both starting from angle 0). a is a value for which a cosine becomes 0. 
     * b is value of cosine wave for angle 0. c is an argument, for which tg(c) = b.
     * Find x, for which cosine wave and tangent graph intersect. All lengths are expressed in Imperial
     * length units (miles, yards, feet, inches).
     * 
     * Input
     * First line contains number of data sets N, 0 < N ≤ 1000.
     * Each data set consists of three integers separated by a whitespace.
     * They are a, b and c respectively, as described above 0 < a, b, c ≤ 1000.
     * All of them express distance in miles.
     * 
     * Output
     * N lines, each containing four numbers separated by a whitespace each.
     * They represent x in Imperial length units as miles, yards, feet and inches respectively, 
     * rounded to the nearest inch. Reminder: 1 foot = 12 inches, 1 yard = 3 feet, 1 mile = 1760 yards.
     */
    public sealed class Geolog : ProblemBase
    {
        protected override string Input => "2\r\n3 4 2\r\n3 5 1";

        protected override string Output => "1 1101 1 10\r\n0 1594 2 5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int[] coeffs = [1760, 3, 12];
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ').Select(s => double.Parse(s)).ToArray();
                double k = splits[1] / Math.Tan(Math.PI * splits[2] / (2 * splits[0]));
                double kb = k / splits[1];
                double x = 2.0 * splits[0] * Math.Asin((Math.Sqrt(kb * kb + 4) - kb) / 2.0) / Math.PI;
                List<int> format = [];
                for(int j = 0; j < coeffs.Length; j++)
                {
                    format.Add((int)x);
                    x -= (int)x;
                    x *= coeffs[j];
                }
                format.Add((int)Math.Round(x));
                output.Add(string.Join(" ", format));
            }
        }
    }
}
