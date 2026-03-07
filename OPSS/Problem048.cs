namespace OPSS
{
    /* 4/5
     * Jesteś jednym z programistów agencji NASA. Pracujecie nad urządzeniem - specjalną tarczą, która
ma służyć porozumiewaniu się z kosmitami za pomocą sygnałów świetlnych. Tarcza emitująca
światło ma postać prostokąta, podzielonego na kwadraty o rozmiarach jednostkowych. Każdy
element tarczy może być w danym momencie zapalony lub zgaszony. Naukowcy mają nadzieję, że
odpowiedni dobór kombinacji zapalonych pól tarczy świetlnej pozwoli komunikować się z
przybyszami z innych planet.
Programistyczny interfejs tarczy świetlnej jest jednak dość osobliwy - wyjaśnimy tylko to co
konieczne, zasłaniając się tajemnicą wojskową. W momencie rozpoczynania tworzenia
wiadomości, wszystkie pola są zgaszone. Wiadomość jest tworzona poprzez umieszczanie na tarczy
prostokątnych szachownic, przy czym pola każdej szachownicy mają wymiary odpowiadające
rozmiarom pól tarczy. Każda szachownica składa się z pól aktywnych i nieaktywnych,
przeplatających się tak jak przeplatają się pola białe i czarne klasycznej szachownicy. W lewym
górnym rogu każdej szachownicy znajduje się zawsze pole aktywne.
Każde pole szachownicy przylega dokładnie do jednego z pól tarczy. Pole tarczy zapala się w
momencie, gdy suma pól aktywnych, które zostały na nie położone, jest nieparzysta.
Projekt jest już prawie gotowy, pozostało tylko opracowanie kontroli poprawności sygnałów. Tobie
przypadło zadanie opracowania algorytmu obliczającego sumę wszystkich zapalonych pól tarczy
świetlnej.
Wejście
W pierwszym wierszu wejścia znajduje się liczba zestawów danych C, 1<=C<=20. W pierwszym
wierszu każdego zestawu danych znajduje się liczba N, 1<=N<=1000, oznaczająca liczbę
szachownic użytych do konstrukcji wiadomości świetlnej. W każdym z kolejnych N wierszy
zestawu znajdują się 4 liczby całkowite, x1,y1,x2,y2, -1000000000<=x1,y1,x2,y2<=1000000000.
Określają one pozycję i rozmiary kolejnych szachownic tworzących wiadomość. Lewy górny róg
szachownicy znajduje się na pozycji (x1,y1) a prawy dolny na pozycji (x2,y2). Układ
współrzędnych jest tak dobrany, że x1<=x2 oraz y1<=y2.
Wyjście
Dla każdego zestawu danych należy wyznaczyć sumę zapalonych pól tarczy świetlnej.
     */
    public sealed class KosmiczneSygnaly : ProblemBase
    {
        protected override string Input => "5\r\n2\r\n0 0 3 3\r\n2 2 5 5\r\n2\r\n0 0 3 3\r\n3 3 6 6\r\n2\r\n0 0 3 3\r\n0 1 3 4\r\n2\r\n0 0 3 3\r\n3 1 6 4\r\n2\r\n1 2 5 5\r\n0 0 3 3";

        protected override string Output => "12\r\n14\r\n16\r\n12\r\n18";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                List<int[]>[] arrays = [[], []];
                int a = int.Parse(input[j]);
                j++;
                for (int k = 0; k < a; k++)
                {
                    var tab = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    arrays[Math.Abs((tab[0] % 2) - (tab[1] % 2))].Add(tab);
                    j++;
                }
                int total = 0;
                foreach (var i2 in Enumerable.Range(0, 2))
                {
                    int sign = 1;
                    List<int[]> intersections;
                    do
                    {
                        intersections = [];
                        var tab = arrays[i2];
                        int sum = 0;
                        for (int k = 0; k < tab.Count; k++)
                        {
                            for (int l = k + 1; l < tab.Count; l++)
                            {
                                int[] mins = new int[4];
                                int[] maxs = new int[4];
                                for (int m = 0; m < 2; m++)
                                {
                                    if (tab[k][m] < tab[l][m])
                                    {
                                        mins[m] = tab[k][m];
                                        maxs[m] = tab[l][m];
                                        mins[m + 2] = tab[k][m + 2];
                                        maxs[m + 2] = tab[l][m + 2];
                                    }
                                    else
                                    {
                                        mins[m] = tab[l][m];
                                        maxs[m] = tab[k][m];
                                        mins[m + 2] = tab[l][m + 2];
                                        maxs[m + 2] = tab[k][m + 2];
                                    }
                                }
                                if (mins[2] >= maxs[0] && mins[0] <= maxs[0] && mins[3] >= maxs[1] && mins[1] <= maxs[1])
                                    intersections.Add([maxs[0], maxs[1], mins[2], mins[3]]);
                            }
                            sum += ((tab[k][2] - tab[k][0] + 1) * (tab[k][3] - tab[k][1] + 1) >> 1);
                            if (tab[k].All(x => x % 2 == tab[k][0] % 2))
                                sum++;
                        }
                        total += sign * (sign == -1 ? (sum << 1) : sum);
                        sign *= -1;
                        arrays[i2] = intersections;
                    }
                    while (arrays[i2].Any());
                }
                output.Add(total.ToString());
            }
        }
    }
}
