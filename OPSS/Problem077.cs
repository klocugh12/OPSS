namespace OPSS
{
    /* Difficulty: 5/5
     * Zadanie
Dana jest plansza o wymiarach 8x7, podzielona na 56 identycznych kwadratów oraz zestaw 28
różnych kamieni domina. Na każdym kwadracie (polu) znajduje się jedna liczba całkowita z
przedziału [0..6]. Należy przykryć tę planszę jednym zestawem kamieni domina tak, aby liczby
znajdujące się na dwóch sąsiednich, przykrywanych polach były równe liczbom znajdującym się na
połówkach kamienia. Kamienie nie mogą na siebie zachodzić, ani leżeć jeden na drugim.
Nie zawsze przykrycie planszy zestawem kamieni domina jest możliwe, często jednak jest wiele
możliwości by to zrobić. Twoim zadaniem jest stwierdzenie dla zadanego opisu planszy, na ile
sposobów można pokryć planszę kamieniami z jednego zestawu domina oraz podać pierwsze
leksykograficznie pokrycie spełniające warunki zadania.
Przykładowa plansza oraz ułożone na niej kamienie domina
Wejście
Wejście zawiera opis planszy. Opis złożony jest z 8 wierszy, w których występuje po 7 liczb
całkowitych (oddzielonych pojedynczą spacją) z przedziału [0..6]. J-ta liczba w i-tym wierszu
wejścia określa liczbę jaka stoi w i-tym wierszu, licząc od góry i j-tej kolumnie planszy.
Wyjście
W pierwszej linii wyjścia należy wypisać liczbę możliwych ustawień kamieni domina, tak aby
pokryć planszę. W następnych liniach wyjścia należy podać opis pierwszego leksykograficznie
pokrycia spełniającego warunki zadania (o ile takie istnieje).
Opis pokrycia zawiera 8 wierszy, w których występuje po 7 liczb naturalnych od 1 do 28
(oddzielonych pojedyncza spacja). J-ta liczba w i-tym wierszu opisu pokrycia oznacza numer
kamienia jaki leży na polu planszy w i-tym wierszu i j-tej kolumnie w tym pokryciu.
Kamienie numerujemy w następujący sposób:
kamień | numer
0:0 | 1
0:1 | 2
0:2 | 3
...
1:1 | 8
1:2 | 9
...
6:6 | 28
Porządek leksykograficzny opisów pokryć oznacza porządek leksykograficzny ciągów uzyskanych
przez zapisanie opisu pokrycia wierszami, rozpoczynając od pierwszego górnego wiersza.
     */
    public sealed class DzienDziecka : ProblemBase
    {
        protected override string Input => "3 5 3 4 1 1 1\r\n4 1 5 0 4 5 0\r\n0 3 6 4 4 0 2\r\n1 6 2 4 2 3 5\r\n2 5 6 0 3 1 5\r\n1 4 4 3 3 6 6\r\n1 2 2 2 6 0 6\r\n5 0 0 6 3 2 5";

        protected override string Output => "5\r\n20 21 21 11 11 8 8\r\n20 10 6 6 24 24 3\r\n2 10 18 23 5 5 3\r\n2 27 18 23 15 15 26\r\n9 27 25 4 4 13 26\r\n9 16 25 19 19 13 28\r\n12 16 14 14 7 7 28\r\n12 1 1 22 22 17 17";

        static int StoneIndex(int k, int j, List<int[]> stones)
        {
            int[] t = j < k ? [j, k] : [k, j];
            for (int i = 0; i < stones.Count; i++)
                if (stones[i].SequenceEqual(t))
                    return i + 1;
            return 0;
        }

        const int X = 7;
        const int Y = 8;

        record Solution(List<((int, int), int)> Items, long MaskPositions, long MaskTiles);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int[]> stones = [];
            for (int i = 0; i <= 6; i++)
                for (int j = i; j <= 6; j++)
                    stones.Add([i, j]);
            List<int[]> stage = [];
            for (int i = 0; i < Y; i++)
            {
                stage.Add(input[i].Split(' ').Select(s => int.Parse(s)).ToArray());
            }
            int index;
            List<(int, (int, int))>[,] optionsStage = new List<(int, (int, int))>[Y, X];
            Dictionary<int, List<(int, int)[]>> optionsTile = [];
            for (int i = 0; i < Y; i++)
            {
                for (int j = 0; j < X; j++)
                {
                    optionsStage[i, j] = [];
                    if (i != 0)
                    {
                        index = StoneIndex(stage[i][j], stage[i - 1][j], stones);
                        optionsStage[i, j].Add((index, (i - 1, j)));
                        optionsStage[i - 1, j].Add((index, (i, j)));
                        if (!optionsTile.ContainsKey(index))
                            optionsTile.Add(index, []);
                        optionsTile[index].Add([(i - 1, j), (i, j)]);
                    }
                    if (j != 0)
                    {
                        index = StoneIndex(stage[i][j], stage[i][j - 1], stones);
                        optionsStage[i, j].Add((index, (i, j - 1)));
                        optionsStage[i, j - 1].Add((index, (i, j)));
                        if (!optionsTile.ContainsKey(index))
                            optionsTile.Add(index, []);
                        optionsTile[index].Add([(i, j - 1), (i, j)]);
                    }
                }
            }
            List<int> keys = optionsTile.Keys.Where(k => optionsTile[k].Count == 1).ToList();
            while (keys.Count > 0)
            {
                var key = keys[^1];
                keys.RemoveAt(keys.Count - 1);
                foreach (var pos in optionsTile[key][0])
                {
                    var stg = optionsStage[pos.Item1, pos.Item2];
                    index = 0;
                    while (stg.Count > 1)
                    {
                        while (stg[index].Item1 == key)
                            index++;
                        var item = stg[index];
                        optionsTile[item.Item1].RemoveAll(ot => (ot[0] == pos || ot[1] == pos) && (ot[0] == item.Item2 || ot[1] == item.Item2));
                        if (optionsTile.Count == 1)
                            keys.Add(item.Item1);
                        optionsStage[item.Item2.Item1, item.Item2.Item2].Remove((item.Item1, pos));
                        var newIndex = item.Item2;
                        if (optionsStage[newIndex.Item1, newIndex.Item2].Count == 1)
                        {
                            var unique = optionsStage[newIndex.Item1, newIndex.Item2][0].Item1;
                            var opt = optionsTile[unique];
                            var index2 = 0;
                            while (opt.Count > 1)
                            {
                                if (opt[index2].Contains(newIndex))
                                    index2++;
                                foreach (var pos2 in opt[index2])
                                {
                                    optionsStage[pos2.Item1, pos2.Item2].Remove((unique, opt[index2][pos2 == opt[index2][0] ? 1 : 0]));
                                    if (optionsStage[pos2.Item1, pos2.Item2].Count == 1)
                                        keys.Add(optionsStage[pos2.Item1, pos2.Item2][0].Item1);
                                }
                                opt.RemoveAt(index2);
                            }
                            var other = optionsStage[newIndex.Item1, newIndex.Item2][0].Item2;
                            var opt2 = optionsStage[other.Item1, other.Item2];
                            index2 = 0;
                            while (opt2.Count > 1)
                            {
                                if (opt2[index2].Item1 == unique)
                                    index2++;
                                var otherItem = opt2[index2];
                                optionsStage[otherItem.Item2.Item1, otherItem.Item2.Item2].Remove((otherItem.Item1, other));
                                if (optionsStage[otherItem.Item2.Item1, otherItem.Item2.Item2].Count == 1)
                                    keys.Add(otherItem.Item1);
                                opt2.RemoveAt(index2);
                            }

                        }
                        optionsStage[pos.Item1, pos.Item2].Remove(item);
                    }
                }
            }
            if (optionsTile.Any(ot => ot.Value.Count == 0))
            {
                output.Add("0");
                return;
            }
            foreach (var opt in optionsStage)
            {
                opt.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            }
            List<Solution> solutions = [];
            int[,] sol = null;
            long maskUsed = (1L << 28) - 1;
            for(int k = 0; k < optionsStage[0, 0].Count; k++)
            {
                solutions.Add(new([((0, 0), k)]
                    , (0L | 1L | 1L << (optionsStage[0, 0][k].Item2.Item1 * X + optionsStage[0, 0][k].Item2.Item2))
                    , (0L | (1L << (optionsStage[0, 0][k].Item1 - 1)))));
            }
            int counter = 0;
            while(solutions.Count > 0)
            {
                var current = solutions[0];
                solutions.RemoveAt(0);
                if (current.MaskTiles == maskUsed)
                {
                    counter++;
                    int[,] newSol = new int[Y, X];
                    foreach (var s in current.Items)
                    {
                        var coord2 = s.Item1;
                        var opt = optionsStage[coord2.Item1, coord2.Item2][s.Item2];
                        newSol[coord2.Item1, coord2.Item2] = opt.Item1;
                        newSol[opt.Item2.Item1, opt.Item2.Item2] = opt.Item1;
                    }
                    if (sol == null)
                        sol = newSol;
                    else
                    {
                        bool resolved = false;
                        for (int k1 = 0; k1 < Y; k1++)
                        {
                            for (int k2 = 0; k2 < X; k2++)
                            {
                                var cmp = sol[k1, k2].CompareTo(newSol[k1, k2]);
                                resolved = cmp != 0;
                                if(cmp == 1)
                                {
                                    sol = newSol;
                                }
                                if (resolved)
                                    break;
                            }
                            if (resolved)
                                break;
                        }
                    }
                    continue;
                }
                int b = 0;
                while ((current.MaskPositions & (1L << b)) > 0)
                    b++;
                (int, int) coord = (b / X, b % X);
                for(int k = 0; k < optionsStage[coord.Item1, coord.Item2].Count; k++)
                {
                    var opt = optionsStage[coord.Item1, coord.Item2][k];
                    if (((current.MaskTiles & (1L << (opt.Item1 - 1))) == 0) && 
                        ((current.MaskPositions & (1L << (opt.Item2.Item1 * X + opt.Item2.Item2))) == 0))
                    {
                        solutions.Add(new(current.Items.Append(((coord), k)).ToList()
                            , (current.MaskPositions | 1L << (coord.Item1 * X + coord.Item2) | 1L << (opt.Item2.Item1 * X + opt.Item2.Item2))
                            , current.MaskTiles | (1L << (opt.Item1 - 1))));
                    }
                }
            }
            output.Add(counter.ToString());
            if (counter > 0)
            {
                var en = Enumerable.Range(0, X).ToArray();
                for(int k = 0; k < Y; k++)
                    output.Add(string.Join(" ", en.Select(en2 => sol![k, en2])));
            }
        }
    }
}
