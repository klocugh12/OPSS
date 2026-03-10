namespace OPSS
{
    /* Difficulty: 3/5
     * Zadanie
Małgosia zastanawia się, ile jest różnych ustawień k hetmanów na szachownicy o wymiarach nxn
pól, tak aby żadne dwa z nich nie szachowały się. Pomóż Małgosi rozwiązać ten problem.
Wejście
W pierwszym wierszu dana jest liczba naturalna d oznaczająca ilość zestawów danych, 1 ≤ d ≤ 10.
W następnych wierszach podane są kolejne zestawy danych. Każdy zestaw składa się z jednego
wiersza, w którym znajdują się, oddzielone pojedynczą spacją, liczby naturalne n oraz k, 1 ≤ n ≤ 12,
1 ≤ k ≤ n^2, oznaczające odpowiednio: "wymiar" szachownicy oraz ilość hetmanów.
Wyjście
W d wierszach wyjścia należy podać wyznaczoną dla każdego zestawu liczbę różnych ustawień k
hetmanów na szachownicy nxn tak, aby żadne dwa z nich nie szachowały się.
     */
    public sealed class Hetmani : ProblemBase
    {
        protected override string Input => "2\r\n3 2\r\n8 8";

        protected override string Output => "8\r\n92";

        struct Position
        {
            public int Column;
            public int Diag1;
            public int Diag2;
        }

        static Position Deconstruct(int j, int a)
        {
            int b = j / a, c = j % a;
            return new Position()
            {
                Column = c,
                Diag1 = c + b,
                Diag2 = b + (a - c - 1)
            };
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int j = 0;
                bool[] cols = new bool[a], diag1 = new bool[(a << 1) - 1], diag2 = new bool[(a << 1) - 1];
                List<int> queens = new(b);
                int c = 0;
                int a2 = a * a;
                int limit = a2 - Math.Max(a, b + 1);
                while (true)
                {
                    var coords = Deconstruct(j, a);
                    if (queens.Count == 0)
                    {
                        if (j >= limit)
                            break;
                        queens.Add(j);
                        cols[coords.Column] = true;
                        diag1[coords.Diag1] = true;
                        diag2[coords.Diag2] = true;
                        j += a - (j % a);
                        continue;
                    }
                    if (!(cols[coords.Column] || diag1[coords.Diag1] || diag2[coords.Diag2]))
                    {
                        if (queens.Count < b - 1)
                        {
                            queens.Add(j);
                            cols[coords.Column] = true;
                            diag1[coords.Diag1] = true;
                            diag2[coords.Diag2] = true;
                            j += a - (j % a);
                        }
                        else
                        {
                            c++;
                            j++;
                        }
                    }
                    else
                        j++;
                    while(j >= a * a)
                    {
                        j = queens[^1];
                        coords = Deconstruct(j, a);
                        cols[coords.Column] = false;
                        diag1[coords.Diag1] = false;
                        diag2[coords.Diag2] = false;
                        queens.RemoveAt(queens.Count - 1);
                        j++;
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
