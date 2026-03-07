namespace OPSS
{
    /* 4/5
     * Jaś postanowił zbudować sobie domek na działce. Chciał rozpocząć od położenia terakoty, więc
zakupił dużą ilość kwadratowych płytek i ułożył je na całej działce w szachownicę nie docinając
żadnej z płytek. Ściany natomiast Jaś chciał postawić na płytkach w taki sposób, aby całe wnętrze
domku było pokryte płytkami. Jeśli Jaś stawiał ścianę na płytce (ale nie na krawędzi płytki), to
musiał przyciąć płytkę.
Napisz program, który pomoże Jasiowi znaleźć najmniejszą ilość płytek, które musi pociąć.
Zakładamy, że ściana ma grubość zerową.
Wejście
Pierwszy wiersz zawiera dodatnią liczbę całkowitą C (1 ≤ C ≤ 10) określającą ilość zestawów
danych. W kolejnych liniach znajdują się zestawy danych. Pierwszy wiersz zestawu danych zawiera
dodatnią liczbę całkowitą D (1 ≤ D ≤ 5) określającą długość boku płytki. Drugi wiersz zestawu
danych zawiera liczbę całkowitą N (4 ≤ N < 10000) określającą liczbę ścian w domku.
Wiersz o numerze i + 2 (i = 1, 2, ..., N) zestawu danych zawiera dwie liczby całkowite xi (0 ≤ xi
≤1000000) oraz yi (0 ≤ yi ≤1000000) oddzielone spacjami określające kolejne rogi domku, czyli
współrzędne (xi, yi) i (xi+1, yi+1) dla i=1, 2, ..., N-1 oraz (xN, yN) i (x1, y1) określają końce ścian.
Ponadto (xi=xi+1) lub (yi=yi+1) dla i=1, 2, ..., N-1. Długości ścian są liczbami dodatnimi.
     */
    public sealed class Plytki : ProblemBase
    {
        protected override string Input => "1\r\n3\r\n4\r\n1 1\r\n9 1\r\n9 10\r\n1 10";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[j]);
                j++;  
                int b = int.Parse(input[j]);
                j++;
                List<int[]> points = [];
                int[] modulosX = new int[a];
                int[] modulosY = new int[a];
                for (int k = 0; k < b; k++)
                {
                    points.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                    if (k > 0)
                    {
                        if (points[k][0] == points[k - 1][0])
                        {
                            var dist = Math.Abs(points[k][1] - points[k - 1][1]);
                            modulosX[points[k][0] % a] += dist;
                        }
                        else
                        {
                            var dist = Math.Abs(points[k][0] - points[k - 1][0]);
                            modulosY[points[k][1] % a] += dist;
                        }
                    }
                    j++;
                }
                if (points[points.Count - 1][0] == points[0][0])
                {
                    var dist = Math.Abs(points[points.Count - 1][1] - points[0][1]);
                    modulosX[points[0][0] % a] += dist;
                }
                else
                {
                    var dist = Math.Abs(points[points.Count - 1][0] - points[0][0]);
                    modulosY[points[0][1] % a] += dist;
                }
                int maxX = 0, maxY = 0;
                for(int k = 1; k < a; k++)
                {
                    if (modulosX[k] > modulosX[maxX])
                        maxX = k;
                    if (modulosY[k] > modulosY[maxY])
                        maxY = k;
                }
                List<(int, int)> cuts = [];
                for (int k = 0; k < points.Count; k++)
                {
                    var p1 = points[k];
                    var p2 = points[(k + 1) % points.Count];
                    if (p1[0] != p2[0])
                    {
                        if (p1[1] % a != maxY)
                        {
                            int x1 = Math.Min(p1[0], p2[0]);
                            int x2 = Math.Max(p1[0], p2[0]);
                            x1 -= (((x1 % a) - maxX + a)) % a;
                            x2 += (((x2 % a) - maxX + a)) % a;
                            int y = p1[1];
                            y -= (((y % a) - maxY + a)) % a;
                            var toAdd = Enumerable.Range(0, (x2 - x1) / a).Select(k => (x1 + k * a, y));
                            foreach (var cut in toAdd.Where(ta => !cuts.Contains(ta)))
                                cuts.Add(cut);
                        }
                    }
                    else
                    {
                        if (p1[0] % a != maxX)
                        {
                            int y1 = Math.Min(p1[1], p2[1]);
                            int y2 = Math.Max(p1[1], p2[1]);
                            y1 -= (((y1 % a) - maxY + a)) % a;
                            y2 += (((y2 % a) - maxY + a)) % a;
                            int x = p1[0];
                            x -= (((x % a) - maxX + a)) % a;
                            var toAdd = Enumerable.Range(0, (y2 - y1) / a).Select(k => (x, y1 + k * a));
                            foreach (var cut in toAdd.Where(ta => !cuts.Contains(ta)))
                                cuts.Add(cut);
                        }
                    }
                }
                output.Add(cuts.Count.ToString());
            }
        }
    }
}
