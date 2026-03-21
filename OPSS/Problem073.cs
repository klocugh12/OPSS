namespace OPSS
{
    /* Difficulty: 3/5
     * Bob has an algae colony and is trying to estimate its growth rate. After a day colony grew 
     * by factor 4, with new cells being 3 times as numerous as old ones, but after 5 days
     * ratio was estimated to be a little below 600, hence growth factor per day is not constantly 4.
     * More accurately, in each generation mature algae creates 4 young ones, but old algae die,
     * releasing toxin, which also kills some of young algae at 1:1 ratio.
     * After one day only three new algae were created despite there being no old ones releasing 
     * toxin, but consider that a one-off.
     * 
     * To recap: 
     * Initial state: 1 algae cell
     * After 1 day: 1 mature cell, 3 new ones created, none died.
     * After 2 days 3 mature cells, 12 new ones created, original cell dies, 
     * killing one new cell for 11 remaining.
     * 
     * Estimate size of colony after given time.
     *
     * Input
     * First line contains number of data sets N, 0 < N ≤ 10000.
     * Each line contains a single number Di, 0 ≤ Di ≤ 200000.
     * 
     * Output
     * N pairs of numbers separated by a whitespace.
     * First number is number of digits for size of colony.
     * Second number is up to 10 first digits of size of colony.
     */
    public sealed class HodowlaAlg : ProblemBase
    {
        protected override string Input => "3\r\n1\r\n70\r\n16";

        protected override string Output => "1 3\r\n40 8574848899\r\n10 1117014753";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                List<int> prev = [1], curr = [3];

                int Di = int.Parse(input[i]);
                for (int j = 1; j < Di; j++)
                {
                    List<int> next = new(curr);
                    int toCarry = 0;
                    int k = curr.Count - 1;
                    while(k >= 0)
                    {
                        next[k] <<= 2;
                        next[k] += toCarry;
                        toCarry = next[k] / 10;
                        next[k] %= 10;
                        k--;
                    }
                    if (toCarry > 0)
                        next.Insert(0, toCarry);
                    int d = next.Count - prev.Count;
                    for(k = prev.Count - 1; k >= 0; k--)
                    {
                        next[k + d] -= prev[k];
                    }
                    for (k = next.Count - 1; k > 0; k--)
                    {
                        if (next[k] < 0)
                        {
                            next[k] += 10;
                            next[k - 1]--;
                        }
                    }
                    if (next[0] == 0)
                        next.RemoveAt(0);
                    prev = curr;
                    curr = next;
                }
                output.Add($"{curr.Count} {string.Join("", curr.Take(10))}");
            }
        }
    }
}
