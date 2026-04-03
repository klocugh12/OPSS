namespace OPSS
{
    /* Time limit: 2s, Memory limit: 16MB, Difficulty: 4/5
     * A sliding puzzle consists of N x N square with a single free tile. Other tiles are numbered 1 .. N*N.
     * You can only manipulate it by swapping free space with a tile directly adjacent to it. 
     * A puzzle is solved when all tiles are arranged in ascending order,
     * left to right, top to bottom, and with free space in the bottom right corner.
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
