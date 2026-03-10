namespace OPSS
{
    /* Difficulty: 3/5
     * Wejście
W pierwszym wierszu wejścia znajduje się liczba Z, 1 ≤ Z ≤ 100, oznaczająca liczbę zestawów
danych. Każdy zestaw danych rozpoczyna się linią zawierająca 2 liczby naturalne: S, R, (2 ≤ S+R ≤
500; S, R > 0), gdzie S oznacza liczbę wierszy na liście definiującej stan magazynu, natomiast R
oznacza liczbę rezerwacji.
W kolejnych S wierszach podany jest bieżący stan magazynu. Każdy wiersz stanu zawiera 4 liczby
naturalne: I, A, B, C, (1 ≤ I ≤ 1000; 1 ≤ A, B, C ≤ 10^9), oznaczające odpowiednio ilość towaru oraz
wartości własności A, B, C towaru.
Ostatnie R wierszy zestawu opisuje zbiór złożonych rezerwacji. Każda rezerwacja to jeden wiersz,
zawierający dokładnie 4 liczby całkowite: i, a, b, c, (1 ≤ i ≤ 1000; 0 ≤ a, b, c ≤ 10^9), oznaczające
odpowiednio ilość oraz oczekiwane przez klienta własności towaru. Jeśli klient oczekuje własności
o wartości 0, oznacza to, że dana własność towaru go nie interesuje. W szczególności, jeśli w
rezerwacji wszystkie 3 własności towaru są równe 0, klient zadowoli się dowolnym towarem w
żądanej ilości, i niekoniecznie cały towar odpowiadający rezerwacji musi być pod względem
własności jednorodny.
Liczby występujące w wierszach wejścia oddzielone są od siebie pojedynczą spacją.
Wyjście
Dla każdego zestawu danych należy wydać na standardowe wyjście linię zawierającą słowo "tak"
jeśli magazyn jest w stanie zrealizować wszystkie rezerwacje, a słowo "nie" jeśli jest to niemożliwe.
     */
    public sealed class Rezerwacje : ProblemBase
    {
        protected override string Input => "2\r\n2 2\r\n2 1 1 1\r\n2 2 3 1\r\n2 0 0 0\r\n1 2 0 0\r\n1 1\r\n100 1 2 3\r\n101 0 0 0";

        protected override string Output => "tak\r\nnie";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                List<int[]> stores = [];
                for (int k = 0; k < a; k++)
                {
                    var enumerable = input[j].Split(' ').Select(s => int.Parse(s));
                    stores.Add(enumerable.Concat([enumerable.Count(s => s == 0)]).ToArray());
                    j++;
                }
                List<int[]> conditions = [];
                for (int k = 0; k < b; k++)
                {
                    var enumerable = input[j].Split(' ').Select(s => int.Parse(s));
                    conditions.Add(enumerable.Concat([ enumerable.Count(s => s == 0) ]).ToArray());
                    j++;
                }
                stores.Sort((a, b) => a[4].CompareTo(b[4]));
                conditions.Sort((a, b) => a[4].CompareTo(b[4]));
                while(conditions.Any())
                {
                    var c = conditions[0];
                    int k = 0;
                    while (k < stores.Count)
                    {
                        if ((stores[k][1] == c[1] || c[1] == 0) && (stores[k][2] == c[2] || c[2] == 0) && (stores[k][3] == c[3] || c[3] == 0))
                        {
                            if (c[0] > stores[k][0])
                            {
                                c[0] -= stores[k][0];
                                stores.RemoveAt(k);
                            }
                            else
                            {
                                stores[k][0] -= c[0];
                                c[0] = 0;
                                break;
                            }
                        }
                        else
                            k++;
                    }
                    if (c[0] > 0)
                    {
                        output.Add("nie");
                        break;
                    }
                    else
                        conditions.RemoveAt(0);
                }
                if(!conditions.Any())
                    output.Add("tak");
            }
        }
    }
}
