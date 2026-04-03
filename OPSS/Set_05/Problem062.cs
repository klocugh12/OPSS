namespace OPSS
{
    /* Time limit: 2s, Memory limit: 4MB, Difficulty: 4/5
     * You're given a bitmap representing a rectangle or ellipse.
     * Find an angle it is rotated against x-axis. Assume the following:
     * Bitmaps are black and white, 1 meaning black pixel, 0 meaning white pixel.
     * A figure can be either filled or just be an outline.
     * Ratio of shorter side/radius to longer one is in the range from 0.2 to 0.8.
     * Angle 0 means a rectangle/ellipse is placed with longer side/radius parallel to x-axis.
     * Angle is an integer within range -85 ≤ alfa ≤ 90 degrees, being a multiple of 5.
     * 
     * Input
     * First line contains number of data sets N, 1 ≤ N ≤ 5.
     * Each data set consists of a single line of numbers seaparated by a whitespace.
     * Amount of numbers in each line is a multiple of 3.
     * Each triplet of numbers in a line has a form Y, X1, X2, 0≤Y,X1,X2≤1000,
     * where Y is a vertical coordinate and X1 and X2 describe a range of horizontal coordinates,
     * for which all pixels in line Y are black. If a figure is an outline, there may be more than one
     * triplet with same Y coordinates. Each line describes a single image.
     * 
     * Output
     * N lines, each containing an integer from range -85 to 90, equal to rotation angle
     * of a figure in degrees, approximated to 5 degrees of accuracy.
     */
    public sealed class KatObrotu : ProblemBase
    {
        protected override string Input => "1\r\n3 25 43 4 19 48 5 17 51 6 16 23 6 46 55 7 14 21 7 49 58 8 13 17 8 53 61 9 12 16 9 57 63 10 11 14 10 59 65 11 10 13 11 62 67 12 9 12 12 64 69 13 8 11 13 66 71 14 8 10 14 68 73 15 7 10 15 70 74 16 7 9 16 72 76 17 6 9 17 73 78 18 6 8 18 75 80 19 6 8 19 77 81 20 5 7 20 78 82 21 5 7 21 79 83 22 5 7 22 81 85 23 5 7 23 82 86 24 4 6 24 84 87 25 4 6 25 85 89 26 4 6 26 86 90 27 4 6 27 88 91 28 4 6 28 88 92 29 4 6 29 90 93 30 4 6 30 91 94 31 4 6 31 91 95 32 4 7 32 93 96 33 5 7 33 94 97 34 5 7 34 95 98 35 5 8 35 96 99 36 5 8 36 96 99 37 6 8 37 98 100 38 6 8 38 99 101 39 6 8 39 99 102 40 7 8 40 101 103 41 7 9 41 101 104 42 7 10 42 102 105 43 8 10 43 103 105 44 8 10 44 103 106 45 9 11 45 104 107 46 9 11 46 104 107 47 10 12 47 105 108 48 10 13 48 106 108 49 11 13 49 107 109 50 11 14 50 107 109 51 12 14 51 108 110 52 13 15 52 108 110 53 13 16 53 109 111 54 14 16 54 109 111 55 14 17 55 110 112 56 15 18 56 110 112 57 16 18 57 111 113 58 16 19 58 111 113 59 17 20 59 112 114 60 18 21 60 112 114 61 19 22 61 112 115 62 20 22 62 112 115 63 21 23 63 112 115 64 22 24 64 113 115 65 23 25 65 113 115 66 24 26 66 113 115 67 25 27 67 114 116 68 26 28 68 114 116 69 27 29 69 114 116 70 28 31 70 114 116 71 29 32 71 114 116 72 29 33 72 114 116 73 31 35 73 114 116 74 32 36 74 114 116 75 33 37 75 114 115 76 34 39 76 114 115 77 36 39 77 114 115 78 37 41 78 113 115 79 38 43 79 113 115 80 39 44 80 112 115 81 41 46 81 112 114 82 43 47 82 111 114 83 44 49 83 111 113 84 46 50 84 110 113 85 48 53 85 110 112 86 49 55 86 109 112 87 51 57 87 108 110 88 54 59 88 107 110 89 55 61 89 105 108 90 58 64 90 103 108 91 60 68 91 101 106 92 63 71 92 99 106 93 66 75 93 96 104 94 69 81 94 92 103 95 74 100 96 77 95 97 82 90";

        protected override string Output => "35";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                Dictionary<int, List<(int, int)>> lines = [];
                int minX = int.MaxValue, maxX = 0, minY = int.MaxValue, maxY = 0;
                for (int j = 0; j < splits.Length; j += 3)
                {
                    int key = int.Parse(splits[j]);
                    if (!lines.ContainsKey(key))
                        lines.Add(key, []);
                    lines[key].Add((int.Parse(splits[j + 1]), int.Parse(splits[j + 2])));
                    minY = Math.Min(minY, key);
                    maxY = Math.Max(maxY, key);
                    minX = Math.Min(minX, lines[key][lines[key].Count - 1].Item1);
                    maxX = Math.Max(maxX, lines[key][lines[key].Count - 1].Item2);
                }

                (int, int) center = ((minX + maxX) >> 1, (minY + maxY) >> 1);
                double dist = 0, angle = 0;
                for (int k = center.Item2; k <= maxY; k++)
                {
                    int x0 = lines[k][0].Item1;
                    int x1 = lines[k][lines[k].Count - 1].Item2;
                    var d = Math.Sqrt((x0 - center.Item1) * (x0 - center.Item1) + (k - center.Item2) * (k - center.Item2));
                    if (d > dist)
                    {
                        dist = d;
                        angle = Math.Atan2(k - center.Item2, x0 - center.Item1);
                    }
                    d = Math.Sqrt((x1 - center.Item1) * (x1 - center.Item1) + (k - center.Item2) * (k - center.Item2));
                    if (d > dist)
                    {
                        dist = d;
                        angle = Math.Atan2(k - center.Item2, x1 - center.Item1);
                    }
                }
                output.Add((Math.Round(angle * 36.0 / Math.PI) * 5).ToString());
            }
        }
    }
}
