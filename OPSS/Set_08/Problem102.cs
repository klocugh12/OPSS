namespace OPSS
{
    /* Time limit: 2s, Memory limit: 16MB, Difficulty: 4/5
     * You're given a number of points on a plane. Find minimum perimeter of a polygon that contains
     * all of them. A point can lie on a perimeter of said polygon.
     * 
     * Input
     * First line contains number of data sets L 0 < L < 1001.
     * Each data set contains of integers separated by a whitespace.
     * First number is a number of points Ni (2 < Ni < 200000), followed by Ni pairs of numbers 
     * xi, yi (-100001 < xi, yi < 100001), each representing coordinates of a point. 
     * 
     * Output
     * L lines, each containing numbers seaparated by a whitespace.
     * First number is minimum length of a circumference of said polygon divided by 150.
     * Following numbers are indexes of points (1 to Ni) on the perimeter, starting with point 
     * with smallest x coordinate and continuing counterclockwise. If there are multiple points with 
     * same xmin, then select a point with smallest y-coordinate. Indexes of points match order
     * they appeared in input.
     */
    public sealed class ProtestEkologow : ProblemBase
    {
        protected override string Input => "2\r\n8 500 1000 1200 1400 0 1200 2000 1000 1300 500 0 0 1000 0 1800 0\r\n14 200 1000 1200 1400 500 1000 2000 1000 1300 500 0 0 1000 -200 1800 0 100 500 600 600 1400 200 1400 1000 1600 1200 1900 500";

        protected override string Output => "41 6 7 8 4 2 3\r\n40 6 7 8 14 4 13 2 1 9";

        class PointData
        {
            public int Position;

            public int X;

            public int Y;

            public double Angle;

            public double Radius;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int L = int.Parse(input[0]);
            for (int i = 1; i <= L; i++)
            {
                List<PointData> points = [];
                int minX = int.MaxValue, minY = int.MaxValue;
                var splits = input[i].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                Dictionary<int, PointData> mapping = [];
                for (int j = 0; j < splits.Length; j += 2)
                {
                    PointData point = new()
                    {
                        X = splits[j],
                        Y = splits[j + 1],
                        Position = (j >> 1) + 1
                    };
                    if (minY > point.Y || (minY == point.Y && minX > point.X))
                    {
                        minX = point.X;
                        minY = point.Y;
                    }
                    points.Add(point);
                    mapping.Add(point.Position, point);
                }
                foreach (var p in points)
                {
                    p.X -= minX;
                    p.Y -= minY;
                    p.Radius = Math.Sqrt(p.X * p.X + p.Y * p.Y);
                    p.Angle = Math.Atan2(p.Y, p.X);
                    if (p.Angle < 0)
                        p.Angle += Math.PI * 2.0;
                }
                double course = 0.0;
                points.Sort((a, b) =>
                {
                    var res = a.Angle.CompareTo(b.Angle);
                    return res != 0 ? res : a.Radius.CompareTo(b.Radius);
                });
                List<int> solution = [points[0].Position];
                for (int k = 1; k < points.Count; k++)
                {
                    var div = Math.Atan2(points[k].Y - mapping[solution[^1]].Y, points[k].X - mapping[solution[^1]].X);
                    if (div < 0)
                        div += Math.PI * 2.0;
                    while (div < course)
                    {
                        solution.RemoveAt(solution.Count - 1);
                        div = Math.Atan2(points[k].Y - mapping[solution[^1]].Y, points[k].X - mapping[solution[^1]].X);
                        if (div < 0)
                            div += Math.PI * 2.0;
                        course = Math.Atan2(mapping[solution[^1]].Y - mapping[solution[^2]].Y, mapping[solution[^1]].X - mapping[solution[^2]].X);
                        if (course < 0)
                            course += Math.PI * 2.0;
                    }
                    solution.Add(points[k].Position);
                    course = div;
                }
                course = 0;
                for (int k = 0; k < solution.Count; k++)
                {
                    var curr = mapping[solution[k]];
                    var prev = mapping[solution[(k + 1) % (solution.Count)]];
                    course += Math.Sqrt((curr.X - prev.X) * (curr.X - prev.X)
                        + (curr.Y - prev.Y) * (curr.Y - prev.Y));
                }
                while (mapping[solution[^1]].X < mapping[solution[0]].X ||
                    (mapping[solution[^1]].X == mapping[solution[0]].X && mapping[solution[^1]].Y < mapping[solution[0]].Y))
                {
                    solution.Insert(0, solution[^1]);
                    solution.RemoveAt(solution.Count - 1);
                }
                var lastLine = points.Where(p => p.Angle == mapping[solution[^1]].Angle).ToList();
                if (lastLine.Count > 1)
                {
                    lastLine.Sort((a, b) => -a.Radius.CompareTo(b.Radius));
                    solution.RemoveAt(solution.Count - 1);
                    solution.AddRange(lastLine.Select(p => p.Position));
                }
                output.Add($"{Math.Ceiling(course / 150.0)} {string.Join(" ", solution)}");
            }
        }
    }
}
