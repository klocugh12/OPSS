namespace OPSS
{
    /* Difficulty: 4/5
     * In order to send signals to outer space, a new, peculiar interface has been developed.
     * It consists of arbitrarily large board divided into square cells.
     * Each cell can be set to on or off state at any given moment.
     * Initially all cells are off. In order to create a message, chessboards of limited size are overlapped
     * with the board, which toggle cells on the board corresponding to black cells on a chessboard.
     * Top left corner of a chessboard is always black.
     * If a cell is toggled on and another black piece from another chessboard is put on it, cell is toggled off again.
     * Your goal is to find number of cells toggled on given positions and dimensions of used chessboards.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 20.
     * First line of each data set contains a number N, 1 ≤ N ≤ 1000, which is number of chessboards used.
     * Following N lines each contain four integers separated by a single whitespace 
     * x1,y1,x2,y2, -1000000000 ≤ x1,y1,x2,y2 ≤ 1000000000. They mean, respectively,
     * x and y coordinates of top left and bottom right corner of a chessboard.
     * Assume x1 ≤ x2 oraz y1 ≤ y2.
     * 
     * Output
     * C lines, each containing number of cells turned on for each data set.
     */
    public sealed class KosmiczneSygnaly : ProblemBase
    {
        protected override string Input => "5\r\n2\r\n0 0 3 3\r\n2 2 5 5\r\n2\r\n0 0 3 3\r\n3 3 6 6\r\n2\r\n0 0 3 3\r\n0 1 3 4\r\n2\r\n0 0 3 3\r\n3 1 6 4\r\n2\r\n1 2 5 5\r\n0 0 3 3";

        protected override string Output => "12\r\n14\r\n16\r\n12\r\n18";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= C; i++)
            {
                List<int[]>[] arrays = [[], []];
                int N = int.Parse(input[j]);
                j++;
                for (int k = 0; k < N; k++)
                {
                    var tab = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    arrays[Math.Abs((tab[0] % 2) - (tab[1] % 2))].Add(tab);
                    j++;
                }
                int total = 0;
                foreach (var i2 in Enumerable.Range(0, 2))
                {
                    int sign = 1;
                    List<int[]> intersections;
                    do
                    {
                        intersections = [];
                        var tab = arrays[i2];
                        int sum = 0;
                        for (int k = 0; k < tab.Count; k++)
                        {
                            for (int l = k + 1; l < tab.Count; l++)
                            {
                                int[] mins = new int[4];
                                int[] maxs = new int[4];
                                for (int m = 0; m < 2; m++)
                                {
                                    if (tab[k][m] < tab[l][m])
                                    {
                                        mins[m] = tab[k][m];
                                        maxs[m] = tab[l][m];
                                        mins[m + 2] = tab[k][m + 2];
                                        maxs[m + 2] = tab[l][m + 2];
                                    }
                                    else
                                    {
                                        mins[m] = tab[l][m];
                                        maxs[m] = tab[k][m];
                                        mins[m + 2] = tab[l][m + 2];
                                        maxs[m + 2] = tab[k][m + 2];
                                    }
                                }
                                if (mins[2] >= maxs[0] && mins[0] <= maxs[0] && mins[3] >= maxs[1] && mins[1] <= maxs[1])
                                    intersections.Add([maxs[0], maxs[1], mins[2], mins[3]]);
                            }
                            sum += ((tab[k][2] - tab[k][0] + 1) * (tab[k][3] - tab[k][1] + 1) >> 1);
                            if (tab[k].All(x => x % 2 == tab[k][0] % 2))
                                sum++;
                        }
                        total += sign * (sign == -1 ? (sum << 1) : sum);
                        sign *= -1;
                        arrays[i2] = intersections;
                    }
                    while (arrays[i2].Any());
                }
                output.Add(total.ToString());
            }
        }
    }
}
