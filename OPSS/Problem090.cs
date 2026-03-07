namespace OPSS
{
    /* 3/5
     * Niedawno cały świat opanowała nowa gra o nazwie "Samotnik". Jej zasady są proste: w grze bierze
udział jeden gracz, który ma do dyspozycji szachownicę oraz pewną liczbę pionków rozłożonych na
polach szachownicy. Na jednym polu może leżeć co najwyżej 1 pionek.
Pionki są zdejmowane w kolejnych ruchach zgodnie z zasadami, że w jednym ruchu można usunąć
jedynie takie pionki, które znajdują się w jednym wierszu bądź w jednej kolumnie szachownicy (w
szczególności oznacza to, że dopuszczone jest wykonanie ruchu polegającego na zdjęciu tylko
jednego pionka). Wszystkie pionki należy zdjąć, wykonując przy tym najmniejszą liczbę ruchów.
Zadanie
Napisz program, który dla zadanego ustawienia pionków wyznaczy najmniejszą liczbę ruchów
potrzebną do zdjęcia wszystkich pionków z szachownicy.
Wejście
Pierwsza linia wejścia zawiera liczbę całkowitą C (1 ≤ C ≤ 5), oznaczającą liczbę zestawów
danych. W kolejnych wierszach znajdują się opisy zestawów danych. W pierwszej linii opisu
zestawu znajduje się liczba N (1 ≤ N ≤ 100000), oznaczająca liczbę pionków znajdujących się na
szachownicy. Kolejne N wierszy zawiera opis położenia pionków. Opis położenia pionka to dwie
liczby naturalne, określające współrzędne X i Y pionka oddzielone pojedynczą spacją: x, y, (1 ≤ x ≤
2*10^9, 1 ≤ y ≤ 2*10^9).
Wyjście
Dla każdego zestawu danych należy wypisać w jednej linii minimalną liczbę ruchów potrzebnych
do usunięcia wszystkich pionków z planszy w danej grze.
     */
    public sealed class Samotnik : ProblemBase
    {
        protected override string Input => "1\r\n7\r\n5 12\r\n10 55\r\n10 30\r\n25 12\r\n44 25\r\n5 25\r\n10 1";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[j]);
                j++;
                Dictionary<int, List<int>> horiz = [];
                Dictionary<int, List<int>> vert = [];
                for (int k = 0; k < a; k++)
                {
                    var s = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (!vert.ContainsKey(s[0]))
                        vert.Add(s[0], [s[1]]);
                    else
                        vert[s[0]].Add(s[1]);
                    if (!horiz.ContainsKey(s[1]))
                        horiz.Add(s[1], [s[0]]);
                    else
                        horiz[s[1]].Add(s[0]);
                    j++;
                }
                int moves = 0;
                while(horiz.Any())
                {
                    var keyH = horiz.Keys.OrderByDescending(k => horiz[k].Count).First();
                    var keyV = vert.Keys.OrderByDescending(k => vert[k].Count).First();
                    bool useHoriz = (horiz[keyH].Count > vert[keyV].Count) || (horiz[keyH].Count == vert[keyV].Count && horiz.Count < vert.Count);
                    if (useHoriz)
                    {
                        foreach (var v in horiz[keyH])
                        {
                            vert[v].Remove(keyH);
                            if (vert[v].Count == 0)
                                vert.Remove(v);
                        }
                        horiz.Remove(keyH);
                    }
                    else
                    {
                        foreach (var v in vert[keyV])
                        {
                            horiz[v].Remove(keyV);
                            if (horiz[v].Count == 0)
                                horiz.Remove(v);
                        }
                        vert.Remove(keyV);
                    }
                    moves++;
                }
                output.Add(moves.ToString());
            }
        }
    }
}
