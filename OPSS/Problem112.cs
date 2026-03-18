namespace OPSS
{
    /* Difficulty: 5/5
     * Consider a building, whose base can be described by a simple, axis-aligned polygon 
     * with no holes, and each side of the base has integer length.
     * We want to arrange a pavement around the whole building, putting tiles around it, so that 
     * they touch either side or corner of the building. Find number of tiles needed.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10).
     * First line of each data set contains number of walls N (4 ≤ N ≤ 20000).
     * Each of the following N lines contain an uppercase English letter and a number separated
     * by a whitespace. A Letter can be any of 'G', 'L', 'D' or 'P' meaning up, let, down and right
     * respectively. A number determines, how many units to move in following direction to draw a wall.
     * A whole building is no wider than 100000 units and no longer than 100000 units.
     * 
     * Output
     * D lines, each containing number of tiles needed to place around the building.
     */
    public sealed class Chodnik : ProblemBase
    {
        protected override string Input => "2\r\n4\r\nP 3\r\nD 2\r\nL 3\r\nG 2\r\n10\r\nL 4\r\nG 3\r\nP 3\r\nG 3\r\nL 7\r\nD 9\r\nP 11\r\nG 5\r\nL 3\r\nD 2";

        protected override string Output => "14\r\n53";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1, k;
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[j]);
                j++;
                List<(int, int)> pts1 = [(0, 0)];
                Dictionary<int, List<(int, int)>> horiz = [];
                Dictionary<int, List<(int, int)>> vert = [];
                int minX = 0, minY = 0, maxX = 0, maxY = 0;
                for (k = 0; k < a; k++)
                {
                    var s = input[j].Split(' ');
                    int num = int.Parse(s[1]);
                    var p2 = pts1[^1];
                    switch (s[0])
                    {
                        case "P":
                            maxX = Math.Max(maxX, p2.Item1 + num);
                            pts1.Add((p2.Item1 + num, p2.Item2));
                            if (!horiz.ContainsKey(p2.Item2))
                                horiz.Add(p2.Item2, []);
                            horiz[p2.Item2].Add((p2.Item1, p2.Item1 + num));
                            break;

                        case "L":
                            minX = Math.Min(minX, p2.Item1 - num);
                            pts1.Add((p2.Item1 - num, p2.Item2));
                            if (!horiz.ContainsKey(p2.Item2))
                                horiz.Add(p2.Item2, []);
                            horiz[p2.Item2].Add((p2.Item1 - num, p2.Item1));
                            break;

                        case "G":
                            maxY = Math.Max(maxY, p2.Item2 + num);
                            pts1.Add((p2.Item1, p2.Item2 + num));
                            if (!vert.ContainsKey(p2.Item1))
                                vert.Add(p2.Item1, []);
                            vert[p2.Item1].Add((p2.Item2, p2.Item2 + num));
                            break;

                        case "D":
                            minY = Math.Min(minY, p2.Item2 - num);
                            pts1.Add((p2.Item1, p2.Item2 - num));
                            if (!vert.ContainsKey(p2.Item1))
                                vert.Add(p2.Item1, []);
                            vert[p2.Item1].Add((p2.Item2 - num, p2.Item2));
                            break;
                    }
                    j++;
                }
                List<int> toRemove = [];
                int index2 = 0;
                while (index2 < pts1.Count - 1)
                {
                    var line = (pts1[index2].Item1, pts1[index2].Item2, pts1[index2 + 1].Item1, pts1[index2 + 1].Item2);
                    if (line.Item2 == line.Item4)
                    {
                        int count = 3;
                        foreach (var v in vert.Keys.Where(k => k < line.Item1 || k > line.Item3))
                        {
                            foreach (var line2 in vert[v])
                            {
                                if (line.Item2 > line2.Item1 && line.Item2 < line2.Item2)
                                {
                                    if (v < line.Item1)
                                        count &= 1;
                                    else
                                        count &= 2;
                                }
                                if (count == 0)
                                {
                                    if (!toRemove.Contains(index2))
                                        toRemove.Add(index2);
                                    toRemove.Add(index2 + 1);
                                    break;
                                }
                            }
                            if (count == 0)
                                break;
                        }
                    }
                    else
                    {
                        int count = 3;
                        foreach (var h in horiz.Keys.Where(k => k < line.Item2 || k > line.Item4))
                        {
                            foreach (var line2 in horiz[h])
                            {
                                if (line.Item1 > line2.Item1 && line.Item1 < line2.Item2)
                                {
                                    if (h < line.Item2)
                                        count &= 1;
                                    else
                                        count &= 2;
                                }
                                if (count == 0)
                                {
                                    if (!toRemove.Contains(index2))
                                        toRemove.Add(index2);
                                    toRemove.Add(index2 + 1);
                                    break;
                                }
                            }
                            if (count == 0)
                                break;
                        }
                    }
                    index2++;
                }
                pts1.RemoveAt(pts1.Count - 1);
                int total = 0;
                int index;
                while (toRemove.Count > 0)
                {
                    index = 0;
                    while (index < toRemove.Count - 1 && toRemove[index] + 1 == toRemove[index + 1])
                        index++;
                    var indexH = (index >> 1);
                    var pt1 = pts1[indexH];
                    var pt2 = pts1[indexH + 1];
                    if (pt1.Item1 != pt2.Item1)
                    {
                        var opt1 = pts1[(indexH + 2) % pts1.Count].Item2;
                        var opt2 = pts1[(indexH + pts1.Count - 1) % pts1.Count].Item2;
                        opt1 = Math.Abs(opt1 - pt1.Item2) < Math.Abs(opt2 - pt1.Item2) ? opt1 : opt2;
                        var d1 = Math.Abs(pt1.Item1 - pt2.Item1);
                        total += d1 + ((Math.Abs(opt1 - pt1.Item2) - 1) << (d1 == 1 ? 0 : 1));
                        if (opt2 == opt1)
                        {
                            pts1[toRemove[indexH] + 1] = (pt2.Item1, opt2);
                            pts1.RemoveAt(toRemove[indexH]);
                            pts1.RemoveAt((toRemove[indexH] + pts1.Count - 1) % pts1.Count);
                        }
                        else
                        {
                            pts1[toRemove[indexH]] = (pt1.Item1, opt1);
                            pts1.RemoveAt(toRemove[indexH] + 1);
                            pts1.RemoveAt(toRemove[indexH] + 1);
                        }                       
                        toRemove.RemoveAt(index);
                        if (index == 1)
                            toRemove.RemoveAt(0);
                    }
                    else
                    {
                        var opt1 = pts1[(indexH + 2) % pts1.Count].Item1;
                        var opt2 = pts1[(indexH + pts1.Count - 1) % pts1.Count].Item1;
                        opt1 = Math.Abs(opt1 - pt1.Item1) < Math.Abs(opt2 - pt1.Item1) ? opt1 : opt2;
                        var d1 = Math.Abs(pt1.Item2 - pt2.Item2);
                        total += d1 + ((Math.Abs(opt1 - pt1.Item1) - 1) << (d1 == 1 ? 0 : 1));
                        if (opt2 == opt1)
                        {
                            pts1[toRemove[indexH] + 1] = (opt2, pt2.Item2);
                            pts1.RemoveAt(toRemove[indexH]);
                            pts1.RemoveAt((toRemove[indexH] + pts1.Count - 1) % pts1.Count);
                        }
                        else
                        {
                            pts1[toRemove[indexH]] = (opt1, pt1.Item2);
                            pts1.RemoveAt(toRemove[indexH] + 1);
                            pts1.RemoveAt(toRemove[indexH] + 1);
                        }
                        toRemove.RemoveAt(index);
                        if (index == 1)
                            toRemove.RemoveAt(0);
                    }
                }
                total += ((maxX - minX) + (maxY - minY) + 2) << 1;
                output.Add(total.ToString());
            }
        }
    }
}
