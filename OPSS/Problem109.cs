namespace OPSS
{
    /* Difficulty: 5/5
     * A workforce is building a road through the forest. A forest is described by 
     * a square divided into NxN square sections. Each section contains specified number of trees.
     * A road starts in northwest corner of the forest and ends in southeast corner.
     * It can go straight on, or turn at 90 degrees. Determine, how to build a road, given
     * following restrictions:
     * ● A road must have lowest possible length.
     * ● As few trees as possible need to be cut building the road.
     * ● A road can contain no more than given K turns.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10).
     * First line of each data set contains two numbers separated by a whitespace.
     * They are, respectively: length of square's sides N (2 ≤ N ≤ 100), and maximum number of turns 
     * K (1 ≤ K < N * N). Following N lines each contain N numbers separated by a whitespace,
     * representing number of trees in each segment needed to cut down to build a road through it.
     * Each of those numbers is nonnegative and lesser than 1000.
     * 
     * Output
     * D lines, each containing least number of trees to cut down to build a road with at most K turns.
     */
    public sealed class Drogowcy : ProblemBase
    {
        protected override string Input => "3\r\n4 3\r\n8 4 9 7\r\n5 1 4 6\r\n6 9 2 5\r\n2 8 9 3\r\n4 1\r\n8 4 9 7\r\n5 1 4 6\r\n6 9 2 5\r\n2 8 9 3\r\n4 6\r\n8 4 9 7\r\n5 1 4 6\r\n6 9 2 5\r\n2 8 9 3";

        protected override string Output => "31\r\n41\r\n27";

        class Point
        {
            public int X;
            public int Y;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                List<int[]> tab = [];
                for (int k = 0; k < a; k++)
                {
                    tab.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                    j++;
                }
                int sumH = 0, sumV = 0;
                for (int k2 = 1; k2 < a; k2++)
                {
                    sumH += tab[0][k2];
                    sumV += tab[k2][0];
                }
                for (int k2 = 1; k2 < a - 1; k2++)
                {
                    sumH += tab[k2][a - 1];
                    sumV += tab[1 - 1][k2];
                }
                List<(int, int)> corners = [(0, 0)];
                corners.Add(sumH > sumV ? (a - 1, 0) : (0, a - 1));
                corners.Add((a - 1, a - 1));
                while (corners.Count < b + 2)
                {
                    var minSol = (-1, (0, 0), (0, 0), int.MaxValue);
                    for (int k = 0; k < corners.Count - 2; k++)
                    {
                        int ogCost = 0;
                        (int, int) pos2 = (corners[k].Item1, corners[k].Item2);
                        while (pos2.Item1 < corners[k + 1].Item1)
                        {
                            ogCost += tab[pos2.Item1][pos2.Item2];
                            pos2 = (pos2.Item1 + 1, pos2.Item2);
                        }
                        while (pos2.Item2 < corners[k + 1].Item2)
                        {
                            ogCost += tab[pos2.Item1][pos2.Item2];
                            pos2 = (pos2.Item1, pos2.Item2 + 1);
                        }
                        while (pos2.Item1 < corners[k + 2].Item1)
                        {
                            ogCost += tab[pos2.Item1][pos2.Item2];
                            pos2 = (pos2.Item1 + 1, pos2.Item2);
                        }
                        while (pos2.Item2 < corners[k + 2].Item2)
                        {
                            ogCost += tab[pos2.Item1][pos2.Item2];
                            pos2 = (pos2.Item1, pos2.Item2 + 1);
                        }
                        List<((int, int), int)> newCosts = [];
                        int vBound = corners[k].Item1 == corners[k + 1].Item1 ? corners[k + 2].Item1 : corners[k + 1].Item1;
                        int hBound = corners[k].Item2 == corners[k + 1].Item2 ? corners[k + 2].Item2 : corners[k + 1].Item2;
                        {
                            for (int k2 = corners[k].Item1 + 1; k2 < vBound; k2++)
                            {
                                int newSum = 0;
                                Point pos = new()
                                {
                                    Y = corners[k].Item1,
                                    X = corners[k].Item2
                                };
                                while (k2 > pos.Y)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.Y++;
                                }
                                while (pos.X < corners[k + 2].Item2)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.X++;
                                }
                                while (pos.Y < corners[k + 2].Item1)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.Y++;
                                }
                                if ((newSum - ogCost) < minSol.Item4)
                                {
                                    minSol = (k + 1, (k2, corners[k].Item2), (k2, corners[k + 2].Item2), newSum - ogCost);
                                }
                            }
                            for (int k2 = corners[k].Item2 + 1; k2 < hBound; k2++)
                            {
                                int newSum = 0;
                                Point pos = new()
                                {
                                    Y = corners[k].Item1,
                                    X = corners[k].Item2
                                };
                                while (k2 > pos.X)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.X++;
                                }
                                while (pos.Y < corners[k + 2].Item1)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.Y++;
                                }
                                while (pos.X < corners[k + 2].Item2)
                                {
                                    newSum += tab[pos.Y][pos.X];
                                    pos.X++;
                                }
                                if ((newSum - ogCost) < minSol.Item4)
                                {
                                    minSol = (k + 1, (corners[k].Item1, k2), (corners[k + 2].Item1, k2), newSum - ogCost);
                                }
                            }
                        }
                    }
                    if (minSol.Item4 > 0)
                        break;
                    corners[minSol.Item1] = minSol.Item3;
                    corners.Insert(minSol.Item1, minSol.Item2);
                }
                int cost = tab[a - 1][a - 1];
                Point pos3 = new()
                {
                    X = 0,
                    Y = 0
                };
                for (int k = 1; k < corners.Count; k++)
                {
                    while (pos3.Y < corners[k].Item1)
                    {
                        cost += tab[pos3.Y][pos3.X];
                        pos3.Y++;
                    }
                    while (pos3.X < corners[k].Item2)
                    {
                        cost += tab[pos3.Y][pos3.X];
                        pos3.X++;
                    }
                }
                output.Add(cost.ToString());
            }
        }
    }
}
