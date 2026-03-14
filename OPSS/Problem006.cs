namespace OPSS
{
    /* Difficulty: 4/5
     * 
     * We're given a board of size m x n, made of square tiles.
     * We pick an starting position on the edge of the board.
     * We put a ball on a starting position and make it go at angle 45 degrees with x-axis.
     * Anytime ball hits the wall, it flips horizontal or vertical velocity, so that it stays inside the board.
     * We assign a point to any tile that ball goes over.
     * Game ends when a ball returns to starting position and assigns a point to it.
     * How many tiles have odd number of points assigned to them when the game ends?
     * 
     * +-+-+-+-+-+-+-+-+-+-+
     * | | |x| |x| | | |x| |
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * | |x| |.| |x| |x| |x|    | |.| |.| | | |.| | | |.| |
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * |X| |.| |x| |.| |x| |    |X| |.| |.| |.| |.| |.| |.|
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * | |.| |.| |.| |x| | |    | |.| |.| |.| | | |.| |.| |
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * |x| |x| |x| |.| |x| |    |.| | | |.| |.| |.| |.| | |
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * | |x| |.| |x| |x| |x|    | |.| |.| |.| |.| |.| |.| |
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * | | |x| |x| | | |x| |    | | |.| | | |.| |.| | | |x|
     * +-+-+-+-+-+-+-+-+-+-+    +-+-+-+-+-+-+-+-+-+-+-+-+-+
     * 
     * Sample boards. X is a starting point. Fields with x have odd number of poinst assigned.
     * Field with . have even number of points assigned.
     * Empty fields have no points assigned.
     
Input
    First line contains number of data sets d, 1<=d<=20.
    Following d lines contain one data set each.
    Each data set consists of four integers, x, y, a, b. x and y are dimensions of board,
    a and b describe x-coordinate and y-coordinate of starting point respectively.
    x and y are greater than 2, number of tiles on the board does not exceed 10^9,
    starting position is on the edge of the board.

    Output
    d lines, where i-th line contains number of tiles assigned odd number of points for i-th data set.

     */
    public sealed class Pileczka : ProblemBase
    {
        protected override string Input => "2\r\n10 7 1 5\r\n13 6 1 5";

        protected override string Output => "22\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var tab = input[i].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                int temp;
                if (tab[1] > tab[0])
                {
                    temp = tab[1];
                    tab[1] = tab[0];
                    tab[0] = temp;
                    temp = tab[3];
                    tab[3] = tab[2];
                    tab[2] = temp;
                }
                List<(int, int)> pts = [];
                var pt = (tab[2], tab[3]);
                int h = 1, v = 1, dist = 0;
                do
                {
                    pts.Add(pt);
                    int x = Math.Min(Math.Abs(pt.Item1 - (h == 1 ? tab[0] : 0)), Math.Abs(pt.Item2 - (v == 1 ? tab[1] : 0)));
                    dist += x;
                    pt = (pt.Item1 + h * x, pt.Item2 + v * x);
                    if (pt.Item1 == 0 || pt.Item1 == tab[0])
                        h *= -1;
                    if (pt.Item2 == 0 || pt.Item2 == tab[1])
                        v *= -1;
                    if ((pt.Item1 == 0 || pt.Item1 == tab[0]) && (pt.Item2 == 0 || pt.Item2 == tab[1]))
                    {
                        output.Add("2");
                        pts.Clear();
                        break;
                    }
                }
                while (pt != pts[0]);
                if (pts.Count > 0)
                {
                    List<int> bPlus = [], bMinus = [];
                    for (int j = 0; j < pts.Count; j++)
                    {
                        if (j % 2 == 0)
                            bPlus.Add(pts[j].Item2 - pts[j].Item1);
                        else
                            bMinus.Add(pts[j].Item2 + pts[j].Item1);
                    }
                    bPlus.Sort((a, b) => -a.CompareTo(b));
                    bMinus.Sort();
                    int crosses = 0;
                    foreach (var bp in bPlus.Skip(1).Take(bPlus.Count - 2))
                    {
                        crosses += bMinus.Select(b => 0.5 * (b - bp)).Count(b => b > 0 && b < tab[0] && b + bp > 0 && b + bp < tab[1]);
                    }
                    output.Add((dist - (crosses << 1)).ToString());
                }
            }
        }
    }
}
