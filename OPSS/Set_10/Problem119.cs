namespace OPSS
{
    /* Memory limit: 40MB, Difficulty: 3/5
     * Consider a board in standard coordinates system. A field (1, 1) is starting field.
     * Each field has a label meaning direction (g - up, d - down, l - left, p - right) or
     * it is a final field with label k. There is a unique path from starting to final field, 
     * following labels on fields step by step. Unfortunately, board has been tampered with.
     * Some of the labels on a path have been erased, thankfully if a label was erased, its 
     * neighbors were not. Also, final field was left untouched. However, that is not all. 
     * All tiles, which are not on the path, have also been given labels indicating directions.
     * Moreso, following those tiles will get you either off the board, or stuck in an infinite loop.
     * Given altered board, try to restore original path.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 50).
     * First line of each data set contains length of side of the board N (2 ≤ N ≤ 1000).
     * Following N lines contain board description. Each such line is N characters long.
     * Allowed characters are: g, p, d, l (directions as described above), k (final field),
     * or . (erased label). Input is no more than 5000 lines long.
     * 
     * Output
     * For each data set write K + 1 rows, First row contains a single number K, equal to 
     * length of restored path. Following K rows each contain two numbers separated by a 
     * whitespace, representing coordinates of successive tiles on the path, starting from
     * (1, 1) and ending with tile labelled k.
     */
    public sealed class Sciezka : ProblemBase
    {
        protected override string Input => "2\r\n4\r\npplg\r\n.pdg\r\ngd.g\r\ngdpk\r\n6\r\ngldl.k\r\npgppgg\r\ngpp.pd\r\n.p.ddl\r\nglddpd\r\np.gppp";

        protected override string Output => "8\r\n1 1\r\n1 2\r\n1 3\r\n2 3\r\n3 3\r\n3 2\r\n3 1\r\n4 1\r\n13\r\n1 1\r\n2 1\r\n2 2\r\n1 2\r\n1 3\r\n2 3\r\n3 3\r\n3 4\r\n4 4\r\n4 5\r\n5 5\r\n5 6\r\n6 6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= C; i++)
            {
                List<(int, int)> solution = [(1, 1)];
                int N = int.Parse(input[j]);
                List<string> maze = new(N);
                j++;
                (int, int) end = (-1, -1);
                for (int k = 0; k < N; k++)
                {
                    maze.Insert(0, input[j]);
                    if (end.Item1 < 0)
                    {
                        int index = input[j].IndexOf('k');
                        if (index >= 0)
                            end = (index, N - k - 1);
                    }
                    j++;
                }
                List<(int, int)> solution2 = [(end.Item1 + 1, end.Item2 + 1)];
                List<(int, int)> options = [];
                if (end.Item1 > 0)
                    options.Add((end.Item1 - 1, end.Item2));
                if (end.Item1 < N - 1)
                    options.Add((end.Item1 + 1, end.Item2));
                if (end.Item2 > 0)
                    options.Add((end.Item1, end.Item2 - 1));
                if (end.Item2 < N - 1)
                    options.Add((end.Item1, end.Item2 + 1));
                foreach (var pt in options)
                {
                    int x = pt.Item1, y = pt.Item2;
                    HashSet<(int, int)> moves = [(end.Item1 + 1, end.Item2 + 1), (x + 1, y + 1)];
                    bool hasMove = true;
                    List<(int, int, int)> dots = [];
                    while (hasMove)
                    {
                        moves.Add((x + 1, y + 1));
                        hasMove = false;
                        if (y > 0
                            && (maze[y - 1][x] == 'g' || maze[y - 1][x] == '.')
                            && !moves.Contains((x + 1, y)))
                        {
                            if (maze[y][x] == '.')
                                dots.Add((x, y, 0));
                            y--;
                            hasMove = true;
                            continue;
                        }
                        if (y < N - 1
                            && (maze[y + 1][x] == 'd' || maze[y + 1][x] == '.')
                            && !moves.Contains((x + 1, y + 2)))
                        {
                            if (maze[y][x] == '.')
                                dots.Add((x, y, 1));
                            y++;
                            hasMove = true;
                            continue;
                        }
                        if (x > 0
                            && (maze[y][x - 1] == 'p' || maze[y][x - 1] == '.')
                            && !moves.Contains((x, y + 1)))
                        {
                            if (maze[y][x] == '.')
                                dots.Add((x, y, 2));
                            x--;
                            hasMove = true;
                            continue;
                        }
                        if (x < N - 1
                            && (maze[y][x + 1] == 'l' || maze[y][x + 1] == '.')
                            && !moves.Contains((x + 2, y + 1)))
                        {
                            if (maze[y][x] == '.')
                                dots.Add((x, y, 3));
                            x++;
                            hasMove = true;
                            continue;
                        }
                        if (x + y > 0 && dots.Count > 0)
                        {
                            while (dots[^1].Item3 == 3)
                            {
                                dots.RemoveAt(dots.Count - 1);
                            }
                            if (dots.Count > 0)
                            {
                                x = dots[^1].Item1;
                                y = dots[^1].Item2;
                                while (!hasMove)
                                {
                                    dots[^1] = dots[^1] with { Item3 = dots[^1].Item3 + 1 };
                                    switch (dots[^1].Item3)
                                    {
                                        case 1:
                                            if (y < N - 1)
                                            {
                                                y++;
                                                hasMove = true;
                                            }
                                            break;

                                        case 2:
                                            if (x > 0)
                                            {
                                                x--;
                                                hasMove = true;
                                            }
                                            break;

                                        case 3:
                                            if (x < N - 1)
                                            {
                                                x++;
                                                hasMove = true;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    if (moves.Contains((1, 1)))
                    {
                        var list = moves.Reverse().ToList();
                        output.Add(list.Count.ToString());
                        output.AddRange(list.Select(s => $"{s.Item1} {s.Item2}"));
                        break;
                    }
                }
            }
        }
    }
}
