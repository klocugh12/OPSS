namespace OPSS
{
    /* Difficulty: 3/5
     * Consider a fixed point in 2D space. A space contains multiple non-overlapping rectangles.
     * All rectangles are axis-aligned. Find minimum number of rays to draw starting from 
     * a given point, so that each rectangle intersects with at least one ray.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 10.
     * First line of each data set contains a number of rectangles N, 1 ≤ N ≤ 10^5.
     * Following N lines each contain four integers separated by a whitespace: xl, yg, xp, yd, 
     * -10^9 ≤ xl, yg, xp, yd ≤ 10^9, xl < xp, yd < yg, such as (xl, yg) is a top-left corner 
     * of the rectangle is (xl, yg) and bottom-right corner of same rectangle is (xp, yd).
     * All rectangles are axis-aligned. No two rectangles overlap, but they might have common
     * corners or edges. Assume that point to draw rays from is (0, 0) and no rectangle contains
     * that point.
     * 
     * Output
     * C lines, each containing a single number equal to minimum number of rays, which
     * would intersect at least one rectangle in a data set.
     */
    public sealed class Kosmita : ProblemBase
    {
        protected override string Input => "1\r\n7\r\n4 0 6 -4\r\n7 0 9 -2\r\n6 -6 8 -8\r\n8 -8 10 -10\r\n-2 -4 4 -9\r\n-3 6 4 2\r\n-4 11 4 7";

        protected override string Output => "3";

        bool IsInside(double from, double to, double toCheck)
        {
            if (from <= to)
                return from <= toCheck && to >= toCheck;
            else
                return from <= toCheck && to >= toCheck - Math.PI;
        }

        (double, double) Overlap((double, double) first, (double, double) second)
        {
            bool pass1 = first.Item2 < first.Item1;
            bool pass2 = second.Item2 < second.Item1;
            if (pass1 == pass2)
                return (Math.Max(first.Item1, second.Item1), Math.Min(first.Item2, second.Item2));
            else if (pass1)
            {
                return (second.Item1 < 0 ? Math.Min(first.Item1, second.Item1) : Math.Max(first.Item1, second.Item1),
                    second.Item2 < 0 ? Math.Max(first.Item1, second.Item1) : Math.Min(first.Item1, second.Item1));
            }
            else
            {
                return (first.Item1 < 0 ? Math.Min(first.Item1, second.Item1) : Math.Max(first.Item1, second.Item1),
                    first.Item2 < 0 ? Math.Max(first.Item1, second.Item1) : Math.Min(first.Item1, second.Item1));
            }

        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                List<(double, double)> houseSweeps = [];
                int N = int.Parse(input[j]);
                j++;
                for (int k = 0; k < N; k++)
                {
                    var coords = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    double[] angles = [Math.Atan2(coords[1], coords[0]), Math.Atan2(coords[3], coords[0]),
                            Math.Atan2(coords[1], coords[2]), Math.Atan2(coords[3], coords[2])];
                    houseSweeps.Add((angles.Min(), angles.Max()));
                    j++;
                }
                houseSweeps.Sort();
                List<(double, double)> lasers = [];
                foreach (var house in houseSweeps)
                {
                    int index = -1;
                    if (lasers.Count > 0 && (IsInside(lasers[^1].Item1, lasers[^1].Item2, house.Item1) || IsInside(lasers[^1].Item1, lasers[^1].Item2, house.Item2)))
                        index = lasers.Count - 1;
                    else if (lasers.Count > 1 && (IsInside(lasers[0].Item1, lasers[0].Item2, house.Item1) || IsInside(lasers[0].Item1, lasers[0].Item2, house.Item2)))
                        index = 0;
                    if (index >= 0)
                    {
                        var found = lasers[index];
                        lasers.RemoveAt(index);
                        lasers.Insert(index, Overlap(found, house));
                    }
                    else
                        lasers.Add(house);
                }
                output.Add(lasers.Count.ToString());
            }
        }
    }
}
