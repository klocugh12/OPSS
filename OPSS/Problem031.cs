namespace OPSS
{
    /* Difficulty: 4/5
     * Profesor Przemysław pasjonuje się obliczeniami równoległymi. Wykorzystuje sieć komputerową
uczelni i nocami prowadzi eksperymenty przy użyciu maszyn w pracowniach. Obliczenia te
przebiegają najczęściej według następującego schematu:
● Najpierw program wykonujący obliczenia jest uruchamiany na jednym z komputerów.
(Nazwiemy go komputerem głównym).
● Uruchomiony program dzieli dane na małe fragmenty i rozsyła je do wszystkich
komputerów w sieci. Każdy komputer w sieci otrzymuje inną, niepodzielną i przesyłaną w
pojedynczym pakiecie porcję danych.
● Komputery wykonują obliczenia dla danych które otrzymały.
● Komputer główny czeka na wyniki od wszystkich komputerów, a następnie po odebraniu
wszystkich wyników częściowych, wykonuje jakieś czynności związane z obliczeniem na
ich podstawie wyniku końcowego.
Nie bez znaczenia jest wybór komputera na którym będą wykonywane obliczenia. Nie wszystkie
komputery w sieci są ze sobą bezpośrednio połączone. (Zawsze istnieje jakaś droga pomiędzy
komputerami, ale nie zawsze jest to połączenie bezpośrednie). Komputery komunikując się między
sobą korzystają więc czasami z komputerów pośrednich, na zasadach podobnych do tych jakie
panują we współczesnych sieciach komputerowych. Jeśli komputer otrzymuje porcję danych do
obliczeń, która nie jest do niego adresowana, przekazuje ją dalej, tak aby dotarła do adresata
najkrótszą drogą. Komputer rozsyłający dane również wysyła je w kierunku który gwarantuje
przekazanie danych adresatowi najkrótszą drogą. Oczywiste jest, że wybierając komputer który ma
służyć jako główny komputer wykonujący obliczenia, trzeba zminimalizować ilość pracy jaka
zostanie wykonana podczas rozsyłania danych i zbierania wyników cząstkowych. To będzie
właśnie Twoje zadanie.
Sieć komputerowa na uczelni ma strukturę drzewiastą. To znaczy, że pomiędzy dwoma
komputerami istnieje zawsze dokładnie jedna droga. Znaczy to też, że można tę sieć opisać
przypisując każdemu komputerowi (za wyjątkiem jednego) komputer dla niego nadrzędny (tak jak
w drzewie każdy węzeł za wyjątkiem korzenia ma jednego ojca). Komputery zostały
ponumerowane kolejnymi liczbami naturalnymi począwszy od 1, przy czym komputer o numerze 1
nie ma komputera nadrzędnego - jest to główny router uczelni. Profesor może wybrać dowolny
komputer do prowadzenia obliczeń, ale nie ma bezpośredniego dostępu do routera, gdyż znajduje
się on za drzwiami z zamkiem szyfrowym, a Profesor nie zna kodu wejściowego. Komputer o
numerze 1 tak jak wszystkie komputery prowadzi więc obliczenia cząstkowe, ale nie może zostać
wybrany jako komputer główny.
Twoim zadaniem będzie wyznaczenie dla zadanej sieci komputerowej, komputera przy wyborze
którego, suma pracy wykonywanej przez WSZYSTKIE komputery sieci podczas rozsyłania danych
będzie najmniejsza. Jeśli będzie kilka takich komputerów, wybieramy ten o najmniejszym numerze.
Poprosimy Cię też o podanie sumy pracy wykonywanej przez wszystkie komputery podczas
rozsyłania danych, przy założeniu, że dokonano optymalnego wyboru. Przyjmujemy, że jednostką
pracy wykonywanej przez komputer jest operacja wysłania lub przekazania dalej pakietu danych.
Przypominamy, że nie można wybrać komputera o numerze 1.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, 1 ≤ C ≤ 30, oznaczająca ilość zestawów
danych. W kolejnych wierszach znajdują się zestawy danych. W pierwszym wierszu każdego
zestawu danych znajduje się liczba N, 2 ≤ N ≤ 100000 - jest to ilość komputerów w uczelnianej
sieci. W kolejnych N-1 wierszach znajdują się numery komputerów nadrzędnych kolejnych
komputerów, od 2 do N, po jednym w każdym wierszu. (W terminologii "drzewowej" są to numery
ojców w drzewie.)
Wyjście
Dla każdego zestawu danych należy wypisać 1 wiersz wyniku, zawierający 2 liczby naturalne
oddzielone spacją. Pierwsza z nich to numer komputera wybranego jako komputer główny, a druga
to suma pracy wykonanej przez wszystkie komputery podczas rozsyłania danych od komputera
głównego do pozostałych komputerów, przy założeniu że komputer główny wybrano optymalnie.
     */
    public sealed class ObliczeniaRownolegle : ProblemBase
    {
        protected override string Input => "2\r\n7\r\n1\r\n1\r\n2\r\n2\r\n3\r\n3\r\n9\r\n9\r\n2\r\n9\r\n4\r\n4\r\n4\r\n4\r\n1";

        protected override string Output => "2 11\r\n4 12";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[j]);
                j++;
                List<int>[] nodes = new List<int>[n];
                for (int k = 0; k < n; k++)
                    nodes[k] = [];
                for (int k = 1; k < n; k++)
                {
                    nodes[int.Parse(input[j]) - 1].Add(k);
                    j++;
                }
                int head = 0;
                int combined = 1;
                List<int> trav = [];
                while (true)
                {
                    List<(int, int)> counts = [];
                    foreach (var c in nodes[head])
                    {
                        int cTotal = nodes[c].Count;
                        List<int> children = new(nodes[c]);
                        while (children.Count > 0)
                        {
                            cTotal += nodes[children[0]].Count;
                            children.AddRange(nodes[children[0]]);
                            children.RemoveAt(0);
                        }
                        counts.Add((c, cTotal));
                    }
                    counts.Sort((a, b) => (Math.Abs(a.Item2 + combined - (n >> 1)).CompareTo(Math.Abs(b.Item2 + combined - (n >> 1)))));
                    if (head == 0 || Math.Abs(counts[0].Item2 + combined - (n >> 1)) < Math.Abs(combined - (n >> 1)))
                    {
                        trav.Add(head);
                        head = counts[0].Item1;
                        combined += counts.Skip(1).Sum(s=> s.Item2 + 1);
                    }
                    else
                        break;
                }
                int p = head;
                while(trav.Any())
                {
                    var last = trav[trav.Count - 1];
                    nodes[last].Remove(p);
                    nodes[p].Add(last);
                    p = last;
                    trav.Remove(last);
                }
                int sum = 0, level = 1;
                List<int> untraversed = [head];
                while(untraversed.Any())
                {
                    untraversed = untraversed.SelectMany(u => nodes[u]).ToList();
                    sum += untraversed.Count * level;
                    level++;
                }
                output.Add($"{head + 1} {sum}");
            }
        }
    }
}
