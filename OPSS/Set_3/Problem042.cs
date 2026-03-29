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

        static bool FourSolvable(Dictionary<int, int[]> puzzle)
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

        static void SolveLine(Dictionary<int, int[]> puzzle, bool column, int index)
        {
            int W = puzzle.Count;
            int K = puzzle[0].Length;

        }

        static void MoveTo(Dictionary<int, int[]> puzzle, int p, int k)
        {

        }

        static IEnumerable<int> RowIndexes(int row, int K) => Enumerable.Range(0, K).Select(r => K * row + r);

        static IEnumerable<int> ColIndexes(int col, int W) => Enumerable.Range(0, W).Select(r => W * r + col);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                var splits = input[j].Split(' ');
                int W = int.Parse(splits[0]), K = int.Parse(splits[1]);
                j++;
                int swaps = 0;
                List<int> positions = new(W * K);
                for (int k = 0; k < W; k++)
                {
                    positions.AddRange(input[j].Split(' ').Select(s => int.Parse(s)));
                    j++;
                }
                for(int k = 0; k < positions.Count; k++)
                {
                    if (positions[k] == 0)
                        continue;
                    swaps += positions.Skip(k + 1).Count(p => p > 0 && p < positions[k]);
                }
                if(W % 2 == 0)
                {
                    var index = positions.IndexOf(0);
                    swaps += index / K + 1;
                }
                output.Add(swaps % 2 == 0 ? "tak" : "nie");
            }
        }
    }
}
