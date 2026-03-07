namespace OPSS
{
    /* 4/5
     * Na prostokątnej szachownicy składającej się z m x n kwadratowych pól wybieramy jedno pole
znajdujące się na krawędzi szachownicy - nazywamy je "polem startowym". Następnie
umieszczamy na jego środku piłeczkę i wprawiamy ją w ruch tak, aby toczyła się po szachownicy.
Średnica piłeczki równa jest szerokości (i wysokości) każdego z pól szachownicy. Kąt pomiędzy
kierunkiem ruchu piłeczki a krawędzią szachownicy wynosi 45 stopni. Piłeczka odbija się od
krawędzi szachownicy następująco: jeżeli piłeczka dotknie krawędzi szachownicy wtedy każda
składowa prędkości piłeczki prostopadła do krawędzi, z którą nastąpiło zetkniecie, zostaje
odwrócona. Na początku piłeczka zostaje rozpędzona w kierunku wzrostu wartości współrzędnych
(w przypadku gdy pole startowe ma największa wartość której ze współrzędnych, piłeczka
natychmiastowo odbija się od krawędzi).
Przypisujemy punkt polu szachownicy za każdym razem gdy piłeczka przetacza się przez obszar
wnętrza pola. Grę uważamy za zakończona jeżeli punkt zostanie przypisany polu startowemu. Jaka
jest liczba pól szachownicy, którym przypisano nieparzysta liczbę punktów? Poniższe rysunki
obrazują problem. Trasa piłeczki zaznaczona jest linią przerywaną. Pola z nieparzystą liczba
punktów są zaznaczone kolorem szarym.
Zadanie
Napisać program, który dla każdego zestawu danych wejściowych, należącego do ciągu kilkunastu
zestawów danych:
● wczytuje ze standardowego wejścia wymiary szachownicy oraz współrzędne pola
startowego,
● znajduje liczbę pól, którym przypisano nieparzystą liczbę punktów,
● wypisuje wynik na standardowe wyjście,
Wejście
Pierwsza linia wejścia zawiera jedną liczbę całkowita d, 1<=d<=20, która oznacza liczbę zestawów
danych. Po niej, w kolejnych liniach, następują zestawy danych wejściowych - każdy zestaw
znajduje się w jednej linii. Linia taka zawiera cztery liczby całkowite x, y, a, b oddzielone
pojedynczymi spacjami. Liczby te to odpowiednio: x'owy i y'owy wymiar szachownicy oraz x'owa
oraz y'owa współrzędna pola startowego. Liczby x oraz y są większe niż 2, liczba pól szachownicy
nie przekracza 10^9, pole startowe leży na krawędzi szachownicy.
Wyjście
I-ta linia wyjścia powinna zawierać jedną liczbę całkowita, która jest równa liczbie pól
szachownicy, którym przypisano nieparzystą liczbę punktów.
     */
    public sealed class Pileczka : ProblemBase
    {
        protected override string Input => "2\r\n10 7 1 5\r\n13 6 1 5";

        protected override string Output => "22\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var tab = input[i].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                int temp;
                if (tab[1] > tab[0])
                {
                    temp = tab[1];
                    tab[1] = tab[0];
                    tab[0] = temp;
                    temp = tab[3];
                    tab[3] = tab[2];
                    tab[2] = temp;
                }
                List<(int, int)> pts = [];
                var pt = (tab[2], tab[3]);
                int h = 1, v = 1, dist = 0;
                do
                {
                    pts.Add(pt);
                    int x = Math.Min(Math.Abs(pt.Item1 - (h == 1 ? tab[0] : 0)), Math.Abs(pt.Item2 - (v == 1 ? tab[1] : 0)));
                    dist += x;
                    pt = (pt.Item1 + h * x, pt.Item2 + v * x);
                    if (pt.Item1 == 0 || pt.Item1 == tab[0])
                        h *= -1;
                    if (pt.Item2 == 0 || pt.Item2 == tab[1])
                        v *= -1;
                    if ((pt.Item1 == 0 || pt.Item1 == tab[0]) && (pt.Item2 == 0 || pt.Item2 == tab[1]))
                    {
                        output.Add("2");
                        pts.Clear();
                        break;
                    }
                }
                while (pt != pts[0]);
                if (pts.Count > 0)
                {
                    List<int> bPlus = [], bMinus = [];
                    for (int j = 0; j < pts.Count; j++)
                    {
                        if (j % 2 == 0)
                            bPlus.Add(pts[j].Item2 - pts[j].Item1);
                        else
                            bMinus.Add(pts[j].Item2 + pts[j].Item1);
                    }
                    bPlus.Sort((a, b) => -a.CompareTo(b));
                    bMinus.Sort();
                    int crosses = 0;
                    foreach (var bp in bPlus.Skip(1).Take(bPlus.Count - 2))
                    {
                        crosses += bMinus.Select(b => 0.5 * (b - bp)).Count(b => b > 0 && b < tab[0] && b + bp > 0 && b + bp < tab[1]);
                    }
                    output.Add((dist - (crosses << 1)).ToString());
                }
            }
        }
    }
}
