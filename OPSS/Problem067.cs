namespace OPSS
{
    /* Difficulty: 4/5
     * Consider a numeric pattern consisting of digits 0-9 and a wildcard X.
     * A wildcard can be any digit other than leading zero.
     * For example, pattern 'X12' is not matched by '012', but is by '112'.
     * For a given pattern find amount of matching numbers that, when divided by 11, give remainder r.
     * 
     * Input
     * First line contains number of data sets  n, 1 ≤ n ≤ 100.
     * Each data set consists of two lines. First line contains two numbers separated by 
     * a whitespace. They are, respectively, length of pattern d, 1 ≤ d ≤ 20,
     * and remainder r after dividing by 11, 0 ≤ r ≤ 10 z dzielenia przez 11.
     * Second line of a data set contains a pattern consisting of digits and wildcards 'X'.
     * 
     * Output
     * n lines, each containing number of values P matching given pattern, which give remainder r
     * when divided by 11. You can assume that  0 ≤ P < 2^63-1.
     */
    public sealed class Jedenastka : ProblemBase
    {
        protected override string Input => "3\r\n2 5\r\nXX\r\n1 8\r\nX\r\n1 8\r\n7";

        protected override string Output => "8\r\n1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= n; i++)
            {
                int r = int.Parse(input[j].Split(' ')[1]);
                j++;
                string pattern = input[j];
                j++;
                int count = 0;
                int xxx = 0;
                for(int k = 0; k < pattern.Length; k++)
                {
                    bool isMod10 = (pattern.Length - k) % 2 == 1;
                    if (pattern[k] == 'X')
                    {
                        xxx++;
                    }
                    else
                    {
                        if (isMod10)
                            r = (r + pattern[k] - '0') % 11;
                        else
                            r = (r + 11 - pattern[k] + '0') % 11;
                    }
                }
                if (xxx == 0)
                {
                    output.Add(r == 0 ? "1" : "0");
                    continue;
                }
                else
                {
                    count = (int)(9 * Math.Pow(10, xxx - 1) / 11.0);
                    if ((pattern.Length % 2 == 1) ^ (r == 0 || r == 10))
                        count++;
                }    
                output.Add(count.ToString());
            }
        }
    }
}
