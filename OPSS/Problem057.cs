namespace OPSS
{
    /* Difficulty: 5/5
     * Consider W-dimensional space.
     * It contains a number of laser cannons bounding the safe area. 
     * Each cannon is placed in some Di point and is described by a vector Li indicating its direction.
     * Half of the whole space is under the influence of each individual cannon. 
     * It is safe to be behind the cannon or on a line (in case of 2D space).
     * Some cannons may be inside region of space that is already under the influence of another cannon,
     * and as such those cannons can be ignored.
     * Now, consider there is a hazardous radiation in the space. It is equal to 0 at certain
     * point of the space A. Intensity of radiation grows along some vector R.
     * Intensity of radiation at any given point P is a product of modulus of R (|R|)
     * by distance to a line (in case of 2D space) perpendicular to R vector anchored at point A.
     * Your goal is to find a safe point with minimum radiation intensity. 
     * 
     * Input
     * First line contains two numbers separated by a whitespace N and W,
     * where N is number of cannons 2 < N < 1024, and W is number of dimensions of a space,  1 < W ≤ 10.
     * Second line contains 2W real numbers separated by a whitespace.
     * First W numbers are coordinates of point A (zero radiation point).
     * Second W numbers are coordinates of a radiation vector R.
     * Following N lines contain 2W real numbers separated by a whitespace each.
     * First W numbers in each of those lines are coordinates of a cannon Di.
     * Second W numbers in each of those lines are coordinates of vector Li.
     * All points' coordinates (Aj, Dij, 1 ≤ i ≤ N, 1 ≤ j ≤ W) meet following conditions: 0 ≤ Aj, Dij <10^9.
     * All vectors' coordinates (Rj, Lij, 1 ≤ i ≤ N, 1 ≤ j ≤ W) meet following conditions: -10^9< Rj, Lij <10^9.
     * 
     * Output
     * Single line containing W+1 integer numbers separated by a whitespace each. 
     * First number is radiation intensity in found point p, 0 ≤ p < 10^9.
     * Following W numbers are that point's coordinates, -10^9 < Xi < 10^9.
     * Input data guarantees that there is only one solution and that result only contains integers.
     */
    public sealed class SztukaPrzetrwania : ProblemBase
    {
        protected override string Input => "8 2\r\n80.0 70.0 -9.0 -18.0\r\n55.5 11.0 12.0 -17.0\r\n36.0 7.0 -4.0 -22.0\r\n16.0 18.0 -18.0 -18.0\r\n7.0 39.0 -24.0 0.0\r\n13.5 54.5 -7.0 13.0\r\n34.0 52.0 12.0 28.0\r\n56.0 31.5 29.0 16.0\r\n70.0 15.0 14.0 0.0";

        protected override string Output => "720 48 46";

        const double PI_2 = Math.PI / 2.0;

        static double[] Norm(double[] a)
        {
            if (a.Length == 2)
                return [-a[1], a[0]];
            var ret = new double[a.Length];
            for (int k = 0; k < a.Length; k++)
            {
                double val = 1.0, val2 = -1.0;
                for (int l = (k + 2) % a.Length; l != (k + a.Length - 2) % a.Length; l = (l + 1) % a.Length)
                {
                    val *= a[l];
                }
                val2 = -val;
                val *= a[(k + 1) % a.Length];
                val2 *= a[(k + a.Length - 1) % a.Length];
                ret[k] = a[k] * (val - val2);
            }
            return ret;
        }

        static double[] Crossing(double[] pt1, double[] pt2, double[] norm1, double[] norm2)
        {
            int index1 = 0;
            while (norm1[index1] == 0)
                index1++;
            int index2 = 0;
            while (index2 < norm1.Length && (index2 == index1 || (norm1[index2] == 0 && norm2[index2] == 0)))
                index2++;
            if (index2 == norm1.Length)
                index2 = (index1 == 0) ? 1 : 0;
            var t1 = (pt2[index1] - pt1[index1] + norm2[index1] * (pt1[index2] - pt2[index2]) / norm2[index2]) / (norm1[index1] - norm2[index1] * norm1[index2] / norm2[index2]);
            return pt1.Zip(norm1, (a, b) => b * t1 + a).ToArray();
        }

        record Laser(double[] Point, double[] Norm, List<double> Atans);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int N = int.Parse(splits[0]), W = int.Parse(splits[1]);
            splits = input[1].Split(' ');
            var radiationPt = splits.Take(W).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            var radiationVec = splits.Skip(W).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            var radiationNorm = Norm(radiationVec);
            var modulus = Math.Sqrt(radiationVec.Select(s => s * s).Sum());
            List<Laser> lasers = [];
            double[] pt = [];
            double dist = double.MaxValue;
            for (int i = 2; i <= N + 1; i++)
            {
                splits = input[i].Split(' ');
                var laserPt = splits.Take(W).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                var laserVec = splits.Skip(W).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                var laserNorm = Norm(laserVec);
                List<double> atans = [];
                for (int j = 0; j < W; j++)
                    for (int k = j + 1; k < W; k++)
                        atans.Add(Math.Atan2(laserVec[j], laserVec[k]));
                lasers.Add(new(laserPt, laserNorm, atans));
            }
            List<(double, double[])> candidates = [];
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    var cross = Crossing(lasers[i].Point, lasers[j].Point, lasers[i].Norm, lasers[j].Norm);
                    if (cross.Any(c => double.IsNaN(c)))
                        continue;
                    bool valid = true;
                    var cross2 = Crossing(radiationPt, cross, radiationNorm, radiationVec);
                    var dist2 = Math.Sqrt(cross2.Zip(cross, (a, b) => (a - b) * (a - b)).Sum()) * modulus;
                    if (dist2 < dist)
                    {
                        foreach (var l in lasers.Except([lasers[i], lasers[j]]))
                        {
                            int dim = 0;
                            var diffs = cross.Zip(l.Point, (a, b) => a - b).ToArray();
                            for (int k = 0; k < W; k++)
                            {
                                for (int k2 = k + 1; k2 < W; k2++)
                                {
                                    var atan = Math.Atan2(diffs[k], diffs[k2]);
                                    if (Math.Abs(atan - l.Atans[dim]) < PI_2)
                                    {
                                        valid = false;
                                        break;
                                    }
                                    dim++;
                                }
                                if (!valid)
                                    break;
                            }
                        }
                        if (valid)
                        {
                            dist = dist2;
                            pt = cross;
                        }
                    }
                }
            }
            output.Add($"{(int)Math.Round(dist)} {string.Join(" ", pt.Select(p => (int)p))}");
        }
    }
}
