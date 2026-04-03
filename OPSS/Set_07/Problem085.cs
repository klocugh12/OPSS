namespace OPSS
{
    /* Time limit: 0.5s, Memory limit: 8MB, Difficulty: 4/5
     * Consider set of 66 tiles, each containing distinct pairs of numbers from 1 to 11:
     * 
     * 1 | 1
     * 1 | 2
     * ..
     * 1 | 11
     * 2 | 2
     * 2 | 3
     * ..
     * 2 | 11
     * ..
     * 11 | 11
     * Try arranging them in such a way:
     * ● Select initial value P
     * ● Arrange tiles horizontally such as adjacent tiles have same numbers on their near sides.
     * ● Each number from 1 to 11 appears exactly twice in a line.
     * ● Leftmost and rightmost numbers are both P.
     * Each of tiles is assigned a certain score.
     * Find minimum and maximum possible score.
     * Tilesets can have 6, 10, 15, 21, 28, 36, 45, 55 or 66 pieces, corresponding to maximum numbers
     * from range 3 to 11.
     * For example 15 tileset is described below:
     * 1 | 1
     * 1 | 2
     * ..
     * 1 | 5
     * 2 | 2
     * 2 | 3
     * ..
     * 2 | 5
     * ..
     * 5 | 5
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 3.
     * Each data set consists three lines.
     * First line contains number N of tiles in a tileset (possible values: 6, 10, 15, 21, 28, 36,
     * 45, 55, 66). Second line contains N integers from range <-1000; 1000>, assigning scores to tiles
     * (tiles are ordered as follows: 1 | 1, 1 | 2, .. 2 | 2, 2 | 3, ..). 
     * Third line contains a single number equal to initial number P, 1 ≤ P ≤ 11.
     * 
     * Output
     * C lines, each containing minimum and maximum score for each data set.
     * Numbers are separated by a single whitespace. If there are no sequences of tiles which satisfy
     * requirements, minimum and maximum scores are both equal to 0.
     */
    public sealed class Klocki : ProblemBase
    {
        protected override string Input => "1\r\n10\r\n1 2 3 4 5 6 7 8 9 10\r\n3";

        protected override string Output => "20 21";

        static (int, int) ComposeKey(int a, int b) => a < b ? (a, b) : (b, a);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[j]);
                N = (int)Math.Sqrt(N << 1);
                Dictionary<(int, int), int> scores = [];
                j++;
                List<int> splits = input[j].Split(' ').Select(s => int.Parse(s)).ToList();
                j++;
                for (int k = 0; k < N; k++)
                    for (int l = k; l < N; l++)
                    {
                        if (k != l)
                        {
                            scores.Add((k + 1, l + 1), splits[0]);
                        }
                        splits.RemoveAt(0);
                    }
                int start = int.Parse(input[j]);
                j++;
                if (start > N)
                {
                    output.Add("0 0");
                    continue;
                }
                List<int> toUse = Enumerable.Range(1, N).ToList();
                List<int> max = [start];
                List<int> min = [start];
                toUse.Remove(start);
                for (int k = 0; k < 2; k++)
                {
                    max.Add(toUse[0]);
                    min.Add(toUse[0]);
                    toUse.RemoveAt(0);
                }
                max.Add(start);
                min.Add(start);
                while (toUse.Any())
                {
                    int next = toUse[0];
                    toUse.RemoveAt(0);
                    int min1 = 0, max1 = 0, deltaMin = int.MaxValue, deltaMax = int.MinValue;
                    for (int k = 1; k < min.Count; k++)
                    {
                        var key1 = ComposeKey(next, min[k]);
                        var key2 = ComposeKey(next, min[k - 1]);
                        var key3 = ComposeKey(min[k], min[k - 1]);
                        int delta = scores[key1] + scores[key2] - scores[key3];
                        if (delta < deltaMin)
                        {
                            min1 = k;
                            deltaMin = delta;
                        }
                        key1 = ComposeKey(next, max[k]);
                        key2 = ComposeKey(next, max[k - 1]);
                        key3 = ComposeKey(max[k], max[k - 1]);
                        delta = scores[key1] + scores[key2] - scores[key3];
                        if (delta > deltaMax)
                        {
                            max1 = k;
                            deltaMax = delta;
                        }
                    }
                    min.Insert(min1, next);
                    max.Insert(max1, next);
                }
                int sum1 = 0, sum2 = 0;
                for (int k = 1; k < min.Count; k++)
                {
                    sum1 += scores[ComposeKey(min[k], min[k - 1])];
                    sum2 += scores[ComposeKey(max[k], max[k - 1])];
                }
                output.Add($"{sum1} {sum2}");
            }
        }
    }
}
