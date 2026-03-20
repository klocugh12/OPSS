namespace OPSS
{
    /* Difficulty: 4/5
     * A sliding puzzle consists of N x N square with a single free tile. Other tiles are numbered 1 .. N*N.
     * Using free tile, manipulate other tiles in other to line up all tiles in ascending order,
     * left to right, top to bottom, and the free tile is in the bottom right corner.
     * Not all initial arrangements are solvable. You need to determine, whether given
     * initial arrangement is solvable.
     * 
     * Input
     * First number contains number of data sets C, 1 ≤ C ≤ 10.
     * First line of each data set contains two numbers W and K, 2 ≤ W, K ≤ 100, 
     * a number of rows and columns respectively for a given arrangement. 
     * Following W rows each contain K numbers each separated by a single whitespace,
     * describing subsequent rows of an arrangement.
     * All numbers are from the range 0 .. W*K-1. 0 represents a free tile.
     * 
     * Output
     * C lines, each containing an answer for a respective data set: "tak", if an arrangement
     * is solvable, "nie" otherwise.
     */
    public sealed class Pietnastka : ProblemBase
    {
        protected override string Input => "2\r\n4 4\r\n1 6 2 4\r\n3 13 11 7\r\n14 5 10 8\r\n0 9 15 12\r\n4 4\r\n1 2 3 4\r\n5 6 7 8\r\n9 10 11 12\r\n13 15 14 0";

        protected override string Output => "tak\r\nnie";

        static bool FourSolvable(List<int[]> puzzle)
        {
            var last4 = Enumerable.Range(0, 2).SelectMany(i => puzzle[puzzle.Count + i - 2].TakeLast(2)).ToList();
            if (!last4.Contains(0))
                return false;
            while (last4.Last() != 0)
            {
                last4 = [last4[2], last4[0], last4[3], last4[1]];
            }
            return last4[0] < last4[1] && last4[1] < last4[2];
        }
        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                var splits = input[j].Split(' ');
                int W = int.Parse(splits[0]), K = int.Parse(splits[1]);
                j++;
                List<int[]> rows = [];
                for (int k = 0; k < K; k++)
                {
                    rows.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                    j++;
                }
                int rowsTop = 0;
                while (rowsTop < rows.Count && Enumerable.Range(0, W).All(i2 => rows[rowsTop][i2] == rowsTop * W + i2 + 1))
                {
                    rowsTop++;
                }
                int colsLeft = 0;
                while (colsLeft < W && Enumerable.Range(0, rows.Count).All(
                    i2 => rows[i2][colsLeft] == i2 * W + colsLeft + 1))
                {
                    colsLeft++;
                }
                K -= rowsTop;
                W -= colsLeft;
                if (K == 1 && Enumerable.Range(0, W - 1).All(
                    i2 => rows[rows.Count - 1][i2] == (rows.Count - 1) * W + i2 + 1
                    || rows[rows.Count - 1][i2 + 1] == (rows.Count - 1) * W + i2 + 1))
                    K = 0;
                if (W == 1 && Enumerable.Range(0, W - 1).All(
                    i2 => rows[i2][W - 1] == i2 * W
                    || rows[i2 + 1][W - 1] == i2 * W))
                    W = 0;
                bool solvable = W != 1 && K != 1;
                solvable = solvable && (W != 2 || K != 2 || FourSolvable(rows));
                output.Add(solvable ? "tak" : "nie");
            }
        }
    }
}
