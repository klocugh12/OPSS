namespace OPSS
{
    /* Difficulty: 4/5
     * A Pascal's triangle is constructed as follows:
     * Left and right edges of a triangle are both filled with ones.
     * Inner numbers inside of a triangle are a sum of two numbers above. Picture below
     * represents a triangle of height H = 4:
     * 
     * H = 0       1
     * H = 1      1 1
     * H = 2     1 2 1
     * H = 3    1 3 3 1
     * H = 4   1 4 6 4 1
     * 
     * Given a triangle of any height H, find number of values in it, which are not divisible
     * by some given prime number P.
     * 
     * Input
     * First line contains number of data sets d, 0 < d ≤ 100. 
     * Each data set consists of two numbers separated by a single whitespace.
     * First number P, 2 ≤ P ≤ 1000000 is equal to a prime number to check.
     * Second number H, 0 ≤ H ≤ 10000 is equal to triangle's height.
     * 
     * Output
     * d lines, each containing number of values inside triangle of height H, which are not divisible
     * by P.
     */
    public sealed class Lotki : ProblemBase
    {
        protected override string Input => "3\r\n2 2\r\n3 4\r\n7 6";

        protected override string Output => "5\r\n12\r\n28";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int d = int.Parse(input[0]);
            for (int i = 1; i <= d; i++)
            {
                var splits = input[i].Split(' ');
                int P = int.Parse(splits[0]), H = int.Parse(splits[1]) + 1;
                if (P >= H)
                {
                    output.Add(((H * (H + 1)) >> 1).ToString());
                    continue;
                }
                int total = 1;
                int pp = P;
                int add = 2;
                int pow = 1;
                while (pp * P <= H)
                {
                    total += add;
                    add = (add << 1) + 2;
                    pp *= P;
                    pow++;
                }
                pp = H - pp;
                add = 0;
                for (int k = 0; k < pp / P; k++)
                {
                    add = (add << 1) + 2;
                }
                total += add;
                total *= (P * (P + 1)) >> 1;
                total += (1 << pow) * ((pp % P) * ((pp % P) + 1) >> 1);
                output.Add(total.ToString());
            }
        }
    }
}
