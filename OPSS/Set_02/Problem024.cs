namespace OPSS
{
    /* Difficulty: 3/5
     * In order to hang the curtains it is a good idea to first hang clips on both ends,
     * then find the middle to hang a clip there, then repeat process recursively for two halves
     * that still need hanging. If however we have even number of clips remaining, it is necessary
     * to use two of them. This requires using measurement tape to find two points around the middle
     * to put clips. We try to do that as few times as possible.
     * 
     * For a given amount of clips, find minimum number of times using measurement tape is necessary.
     * 
     * Input
     * First line contains number of data sets  d, 1 ≤ d ≤ 500000.
     * Each data set consists of single number n, 3 ≤ n < 2^31 equal to number of clips to use.
     * 
     * Output
     * d lines, each containing a single number equal to number of times to use measuring tape for respective number of clips.
     */
    public sealed class Firanki : ProblemBase
    {
        protected override string Input => "2\r\n18\r\n15";

        protected override string Output => "1\r\n6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[i]) - 2;
                int result = 0, halves = 1;
                while (n > 0)
                {
                    if ((n / halves) % 2 == 0)
                    {
                        n -= (halves << 1);
                        result += halves;
                    }
                    else
                    {
                        n -= halves;
                    }
                    halves <<= 1;
                }
                output.Add(result.ToString());
            }
        }
    }
}
