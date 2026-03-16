namespace OPSS
{
    /* Difficulty: 4/5
     * John is building a new house. He tiled the floor using square tiles, forming a rectangle.
     * Then he wants to build walls on the tiled floor. He wants to put as much wall as possible 
     * on the edges of tiles, otherwise he is forced to cut the tiles.
     * Find the smallest number of tiles to cut, ignoring thickness of walls.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 10).
     * First line of each data set contains a single integer D (1 ≤ D ≤ 5), equal to side of each tile.
     * Second line of each data set contains a single integer N (4 ≤ N < 10000),
     * equal to number of walls. Following N lines each contain two integers
     * separated by a whitespace. They are xi and yi, such as (xi, yi) is i-th corner of a house,
     * xi, 0 ≤ xi ≤ 1000000, 0 ≤ yi ≤ 1000000. Each of the consecutive pairs of corners, as well as
     * last and first pair together describe a wall of John's house.
     * 
     * Output
     * C lines, each containing a single integer equal to smallest number of tiles to cut.
     */
    public sealed class Plytki : ProblemBase
    {
        protected override string Input => "1\r\n3\r\n4\r\n1 1\r\n9 1\r\n9 10\r\n1 10";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[j]);
                j++;  
                int b = int.Parse(input[j]);
                j++;
                List<int[]> points = [];
                int[] modulosX = new int[a];
                int[] modulosY = new int[a];
                for (int k = 0; k < b; k++)
                {
                    points.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                    if (k > 0)
                    {
                        if (points[k][0] == points[k - 1][0])
                        {
                            var dist = Math.Abs(points[k][1] - points[k - 1][1]);
                            modulosX[points[k][0] % a] += dist;
                        }
                        else
                        {
                            var dist = Math.Abs(points[k][0] - points[k - 1][0]);
                            modulosY[points[k][1] % a] += dist;
                        }
                    }
                    j++;
                }
                if (points[points.Count - 1][0] == points[0][0])
                {
                    var dist = Math.Abs(points[points.Count - 1][1] - points[0][1]);
                    modulosX[points[0][0] % a] += dist;
                }
                else
                {
                    var dist = Math.Abs(points[points.Count - 1][0] - points[0][0]);
                    modulosY[points[0][1] % a] += dist;
                }
                int maxX = 0, maxY = 0;
                for(int k = 1; k < a; k++)
                {
                    if (modulosX[k] > modulosX[maxX])
                        maxX = k;
                    if (modulosY[k] > modulosY[maxY])
                        maxY = k;
                }
                List<(int, int)> cuts = [];
                for (int k = 0; k < points.Count; k++)
                {
                    var p1 = points[k];
                    var p2 = points[(k + 1) % points.Count];
                    if (p1[0] != p2[0])
                    {
                        if (p1[1] % a != maxY)
                        {
                            int x1 = Math.Min(p1[0], p2[0]);
                            int x2 = Math.Max(p1[0], p2[0]);
                            x1 -= (((x1 % a) - maxX + a)) % a;
                            x2 += (((x2 % a) - maxX + a)) % a;
                            int y = p1[1];
                            y -= (((y % a) - maxY + a)) % a;
                            var toAdd = Enumerable.Range(0, (x2 - x1) / a).Select(k => (x1 + k * a, y));
                            foreach (var cut in toAdd.Where(ta => !cuts.Contains(ta)))
                                cuts.Add(cut);
                        }
                    }
                    else
                    {
                        if (p1[0] % a != maxX)
                        {
                            int y1 = Math.Min(p1[1], p2[1]);
                            int y2 = Math.Max(p1[1], p2[1]);
                            y1 -= (((y1 % a) - maxY + a)) % a;
                            y2 += (((y2 % a) - maxY + a)) % a;
                            int x = p1[0];
                            x -= (((x % a) - maxX + a)) % a;
                            var toAdd = Enumerable.Range(0, (y2 - y1) / a).Select(k => (x, y1 + k * a));
                            foreach (var cut in toAdd.Where(ta => !cuts.Contains(ta)))
                                cuts.Add(cut);
                        }
                    }
                }
                output.Add(cuts.Count.ToString());
            }
        }
    }
}
