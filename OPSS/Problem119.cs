namespace OPSS
{
    /* 3/5
     * 
Rozważmy kwadratową planszę z polami o standardowych współrzędnych. Pole o współrzędnych (1, 1) 
    jest zawsze polem startowym. Każde pole ma miejsce na etykietę oznaczającą kierunek (góra, 
    prawo, dół, lewo), lub w przypadku pola końcowego etykietę koniec. Pola na planszy począwszy 
    od pola startowego do pola końcowego tworzą ścieżkę w taki sposób, że pierwszym polem ścieżki 
    jest pole startowe, następnym polem jest pole znajdujące się na planszy najbliżej w kierunku 
    opisanym etykietą pola startowego, i tak dalej aż do pola końcowego (zobacz rysunek 1.).

rysunek 1.

Dwa złośliwe chochliki postanowiły trochę namieszać na planszy. Pierwszy z nich usunął część 
    etykiet z pól należących do ścieżki. Na szczęście usuwał on etykiety w taki sposób, że 
    z dowolnych dwóch sąsiednich (przylegających do siebie bokami) pól ścieżki zniknęła co 
    najwyżej jedna etykieta. Dodatkowo na pewno nie zniknęły etykiety z pól: startowego i 
    końcowego oraz pól które mają więcej niż dwa sąsiednie pola należące do ścieżki.

Drugi złośliwy chochlik wprowadził etykiety na wszystkie pola planszy nienależące do ścieżki 
    w taki sposób, że jeśli w polu z usuniętą (przez pierwszego chochlika) etykietą wstawimy inną 
    etykietę niż usunięta, to idąc w kierunku przez nią wskazywanym wejdziemy na fałszywą ścieżkę, 
    czyli taką, która nie prowadzi do pola końcowego (idąc po niej albo wyjdziemy poza planszę, 
    albo wpadniemy w nieskończoną pętlę - zobacz rysunek 2.).

rysunek 2.
Zadanie

Napisać program, który dla danej planszy z etykietami (będącej wynikiem działania złośliwych 
    chochlików) odtworzy ścieżkę, z której usunięto etykiety.
Wejście

Pierwsza linia wejścia zawiera liczbę zestawów danych C (1 ≤ C ≤ 50). W kolejnych wierszach 
    wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa się z jednego wiersza 
    zawierającego liczbę naturalną N (2 ≤ N ≤ 1000) określającą rozmiar planszy oraz N wierszy 
    składających się z N znaków reprezentujących planszę. Każde pole planszy reprezentowane jest 
    przez jeden ze znaków: g, p, d, l, k (pierwsza litera etykiety pola) lub znaku . (kropki), 
    oznaczającego pole scieżki pozbawione etykiety.
Dane wejściowe składają się co najwyżej z 5000 wierszy.

Dla przykładu plansza z rysunku 2. reprezentowana jest przez zestaw postaci:

4
pplg
.pdg
gd.g
gdpk

Wyjście

Dla każdego zestawu danych należy wypisać K+1 wierszy: w pierwszym powinna się znaleźć liczba K 
    oznaczająca długość ścieżki, a w K kolejnych wierszach powinny zostać wypisane po dwie liczby 
    oznaczające wpółrzędne kolejnych punktów ścieżki.
     */
    public sealed class Sciezka : ProblemBase
    {
        protected override string Input => "2\r\n4\r\npplg\r\n.pdg\r\ngd.g\r\ngdpk\r\n6\r\ngldl.k\r\npgppgg\r\ngpp.pd\r\n.p.ddl\r\nglddpd\r\np.gppp";

        protected override string Output => "8\r\n1 1\r\n1 2\r\n1 3\r\n2 3\r\n3 3\r\n3 2\r\n3 1\r\n4 1\r\n13\r\n1 1\r\n2 1\r\n2 2\r\n1 2\r\n1 3\r\n2 3\r\n3 3\r\n3 4\r\n4 4\r\n4 5\r\n5 5\r\n5 6\r\n6 6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                List<(int, int)> solution = [(1, 1)];
                int n = int.Parse(input[j]);
                List<string> maze = new(n);
                j++;
                (int, int) end = (-1, -1);
                for (int k = 0; k < n; k++)
                {
                    maze.Insert(0, input[j]);
                    if (end.Item1 < 0)
                    {
                        int index = input[j].IndexOf('k');
                        if (index >= 0)
                            end = (index, n - k - 1);
                    }
                    j++;
                }
                List<(int, int)> solution2 = [(end.Item1 + 1, end.Item2 + 1)];
                List<(int, int)> options = [];
                if (end.Item1 > 0)
                    options.Add((end.Item1 - 1, end.Item2));
                if (end.Item1 < n - 1)
                    options.Add((end.Item1 + 1, end.Item2));
                if (end.Item2 > 0)
                    options.Add((end.Item1, end.Item2 - 1));
                if (end.Item2 < n - 1)
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
                        if (y < n - 1
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
                        if (x < n - 1
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
                                            if (y < n - 1)
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
                                            if (x < n - 1)
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
