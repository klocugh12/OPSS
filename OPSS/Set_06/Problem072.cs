namespace OPSS
{
    /* Time limit: 2s, Memory limit: 16MB, Difficulty: 4/5
     * You're given N integer numbers. Find out whether you can create a subset of those numbers,
     * whose sum is equal to a given value.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 10.
     * Each data set consists of two lines. First line contains two numbers N and S, separated by a whitespace.
     * N is a number of values in a set, and S is a sum to reach (1 ≤ N ≤ 500, -2^31 < S < 2^31).
     * Second line contains N integers, each separated by a whitespace: 
     * a1, a2, ..., an, -500 ≤ ai ≤ 500, for i: 1 ≤ i ≤ N.
     * 
     * Output
     * C lines, each containing an answer for respective data set: TAK - if exists a subset of
     * input numbers, whose sum is equal to S, NIE otherwise.
     */
    public sealed class Optymalizator : ProblemBase
    {
        protected override string Input => "2\r\n5 32\r\n1 2 5 10 25\r\n5 -16\r\n1 2 5 -10 -10";

        protected override string Output => "TAK\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                var splits = input[(i << 1) - 1].Split(' ');
                int sum = int.Parse(splits[1]);
                var list = input[i << 1].Split(' ').Select(s => int.Parse(s)).ToArray();
                int min = 0, max = 0;
                foreach (var l in list)
                {
                    if (l > 0)
                        max += l;
                    else
                        min += l;
                }
                if (sum > max || sum < min)
                {
                    output.Add("NIE");
                    continue;
                }
                bool[] possibles = new bool[max - min + 1];
                foreach (var p in list)
                {
                    if (p > 0)
                    {
                        int k = max - p;
                        while (k > 0)
                        {
                            if (possibles[k])
                                possibles[p + k] = true;
                            k--;
                        }
                    }
                    else
                    {
                        int k = p - min;
                        while (k < possibles.Length)
                        {
                            if (possibles[k])
                                possibles[p + k] = true;
                            k++;
                        }
                    }
                    possibles[p - min] = true;
                }
                output.Add(possibles[sum - min] ? "TAK" : "NIE");
            }
        }
    }
}
