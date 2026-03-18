using System.Globalization;

namespace OPSS
{
    /* Difficulty: 4/5
     * You're given a sphere which is tesellated by either triangles or rectangles, and a point light source.
     * Your goal is to determine, which of the polygons receives most light.
     * For each polygon, light intensity depends on an angle, which a ray of light crossing the center 
     * of each polygon (average of polygon's vertices' coordinates) makes with a plane containing said polygon.
     * Intensity is maximum for perpendicular ray. If a ray is parallel or does not reach the polygon,
     * intensity is equal to zero. Intensity also decreases with square of distance from the 
     * light source to the polygon's center. If for best lit polygon intensity is equal to E,
     * then for all other polygons intensity is less than (1 - 10^-11) * E.
     * 
     * Input
     * First line contains number of data sets L, (1 ≤ L ≤ 5).
     * First line of each data set contains two numbers N and W separated by a whitespace.
     * N is number of polygons, W is number of vertices (4 ≤ N, W ≤ 15000).
     * Second line of each data set contains 3 floating point numbers separated by a whitespace each, 
     * xl, yl, zl (-10^10 < xl, yl, zl < 10^10), representing coordinates of a light source.
     * A light source is always outside of the sphere.
     * Following W lines each cotain 3 floating point numbers x, y, z separated by a whitespace each,
     * representing coordinates of each vertex (-10^10 < x,y,z < 10^10).
     * Vertices are numbered 1 to W each according to order they appear in input.
     * Following N rows each contain 4 integers, each separated by a whitespace, representing
     * indexes of vertices forming each polygon. First three vertices are positive, fourth is nonnegative - 
     * if equal to zero, a polygon is a triangle, otherwise a polygon is a rectangle.
     * 
     * Output
     * A single number 1 to N equal to index of best lit polygon on a surface of the sphere.
     * Numbering of polygons corresponds to order they appeared in input.
     */
    public sealed class Robaczek : ProblemBase
    {
        protected override string Input => "1\r\n4 4\r\n6.100000E+00 5.100000E+00 4.300000E+00\r\n1.619953E-01 8.379142E-02 7.330799E-03\r\n1.923704E-01 2.691684E-01 2.354919E-02\r\n3.671053E-01 3.717102E-01 3.252042E-02\r\n7.274067E-02 3.063127E-01 2.044082E-01\r\n2 1 3 0\r\n1 2 4 0\r\n3 1 4 0\r\n3 2 4 0";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                j++;
                var light = input[j].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                j++;
                List<double[]> points = [];
                double[] mins = [double.MaxValue, double.MaxValue, double.MaxValue],
                    maxs = [double.MinValue, double.MinValue, double.MinValue];
                for (int k = 0; k < splits[0]; k++)
                {
                    var splits2 = input[j].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                    for (int l = 0; l < mins.Length; l++)
                    {
                        mins[l] = Math.Min(mins[l], splits2[l]);
                        maxs[l] = Math.Max(maxs[l], splits2[l]);
                    }
                    points.Add(splits2);
                    j++;
                }
                int[] range = [0, 1, 2];
                var center = new double[3];
                foreach (var r in range)
                    center[r] = points.Select(p => p[r]).Sum() / points.Count;
                var line = center.Zip(light, (a, b) => a - b).ToArray();
                var dirs = points.Select(p => range.Select(r => p[r] - light[r]).ToArray()).ToArray();
                var dists = points.Select(p => range.Select(r => (p[r] - light[r]) * (p[r] - light[r])).Sum()).ToArray();
                var distLightCenter = light.Zip(center, (a, b) => (a - b) * (a - b)).Sum();
                double minAvg = double.MaxValue, min = 0;
                for (int k = 0; k < splits[1]; k++)
                {
                    var splits2 = input[j].Split(' ').Where(s => s != "0").Select(s => int.Parse(s)).ToArray();
                    var d = splits2.Select(s => dirs[s - 1]);
                    var p = splits2.Select(s => dists[s - 1]);
                    j++;
                    if (p.All(d2 => d2 > distLightCenter))
                        continue;
                    var avgs = range.Select(r => d.Select(d2 => d2[r]).Average()).ToArray();
                    var val = range.Select(r => (avgs[r] - line[r]) * (avgs[r] - line[r])).Sum();
                    if(val < minAvg)
                    {
                        minAvg = val;
                        min = k + 1;
                    }
                }
                output.Add(min.ToString());
            }
        }
    }
}
