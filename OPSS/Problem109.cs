namespace OPSS
{
    /* Difficulty: 5/5
     * Opsslandia miejscami porośnięta jest bujnymi lasami. Przez jeden z nich drogowcy muszą
poprowadzić drogę. Las nie jest w każdym miejscu tak samo gęsty, dlatego wybór przebiegu drogi
wpływa na liczbę wyciętych drzew podczas jej budowy. Ponieważ wszyscy mieszkańcy Opsslandii
cenią sobie lasy, istotne jest, żeby wyciąć jak najmniej drzew. W celu ułatwienia zadania, drogowcy
sporządzili kwadratowy plan tej części lasu, przez którą może przebiegać droga. Następnie przy
użyciu siatki podzielili sporządzony plan na N * N równych kwadratowych obszarów. Jeden koniec
przyszłej drogi ma być w obszarze znajdującym się w północno-zachodnim rogu siatki, a drugi
koniec w obszarze znajdującym się południowo-wschodnim rogu siatki. Z pewnych względów
przez dany obszar drogę można poprowadzić albo na wprost (łącząc obszar sąsiadujący od północy
z obszarem sąsiadującym od południa lub łącząc obszar sąsiadujący od zachodu z obszarem
sąsiadującym od wschodu), albo wytyczyć zakręt pod kątem prostym (na przykład łącząc obszar
sąsiadujący od południa z obszarem sąsiadującym od zachodu lub łącząc obszar sąsiadujący od
wschodu z obszarem sąsiadującym od północy). Kolejnym krokiem drogowców było wyznaczenie
dla każdego obszaru liczby drzew, jaką muszą ściąć, gdyby chcieli poprowadzić przez ten obszar
drogę (wyznaczona liczba drzew do ścięcia jest zawsze taka sama, niezależnie czy jest to zakręt,
droga na wprost, czy jeden z końców drogi). Jednak nie tylko liczba ściętych drzew decyduje o
przebiegu drogi. Najważniejsze jest, żeby droga przebiegała przez minimalną liczbę obszarów, co
zdaniem drogowców spowoduje, że będzie najkrótsza. Ponadto każdy zakręt musi być dobrze
oznakowany, a drogowcy dysponują znakami, które wystarczą na oznakowanie tylko K zakrętów
(końce przyszłej drogi nie są zakrętami).
Zadanie
Napisz program, który wyznaczy najmniejszą liczbę drzew, jakie drogowcy muszą ściąć budując
drogę, która będzie przebiegać przez minimalną liczbę obszarów i będzie zawierać nie więcej niż K
zakrętów.
Wejście
Pierwsza linia zawiera liczbę całkowitą D (1 ≤ D ≤ 10) określającą liczbę zestawów danych. W
następnych liniach opisane są kolejno po sobie zestawy danych. W pierwszej linii zestawu danych
znajdują się dwie liczby całkowite rozdzielone spacją: liczba N (2 ≤ N ≤ 100) oraz liczba K (1 ≤ K
< N * N). W każdej z kolejnych N linii zestawu danych znajduje się opis kolejnego wiersza
obszarów sporządzonego planu. Wiersze obszarów planu podane są w kolejności od najbardziej
północnego do najbardziej południowego. Opis pojedynczego wiersza planu składa się z N liczb
całkowitych nieujemnych mniejszych od 1000 oddzielonych od siebie pojedynczymi spacjami.
Kolejne liczby w opisie wiersza przyporządkowane są kolejnym obszarom sporządzonego planu (w
kolejności od obszaru najbardziej zachodniego do najbardziej wschodniego). Liczba
przyporządkowana obszarowi oznacza liczbę drzew, jaką muszą ściąć drogowcy, gdyby chcieli
poprowadzić drogę przez ten obszar.
Wyjście
W oddzielnych liniach dla każdego zestawu danych należy wypisać jedną liczbę całkowitą
oznaczającą najmniejszą liczbę drzew, jakie drogowcy muszą ściąć budując drogę, która będzie
przebiegać przez minimalną liczbę obszarów i będzie zawierać nie więcej niż K zakrętów.
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
