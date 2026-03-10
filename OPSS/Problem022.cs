namespace OPSS
{
    /* Difficulty: 2/5
     * 
Pewien matematyk Dobromir oraz pewien fizyk Albert z zamiłowania zajmują się układaniem
specyficznych układanek z drobnych kamyków. Ich układanki posiadają ciekawą właściwość:
składają się z ciągu kolumn ułożonych z kamyków tak, że każda następna kolumna (licząc od lewej
strony) zawiera nie więcej kamyków niż poprzednia. Wszystkie kolumny zaś są "wyrównane" do
dołu (zaczynają się od najniższego wiersza układanki).
Swoje układanki Dobromir zwykł opisywać, jak na matematyka przystało, jako ciąg liczb,
używając następującej konwencji (nazwijmy ją umownie konwencją M): pierwsza liczba oznacza
ilość wszystkich kamyków w całej układance, zaś kolejne liczby oznaczają ilość kamyków w
kolejnych kolumnach, rozpoczynając od kolumny w której jest najwięcej kamyków. Albert, nie
chcąc być gorszym od Dobromira, wymyślił swój, nieco inny sposób opisywania układanek
(nazwijmy go umownie konwencją F). Używał w tym celu również ciągu liczb, ale takiego, w
którym kolejne liczby oznaczają ilość kamyków w poszczególnych wierszach układanki
(rozpoczynając od wiersza znajdującego się na samym dole układanki, w którym jest najwięcej
kamyków).
Rys. Przykładowy układ kamyków (konwencja M: 24 8 6 4 4 2; konwencja F: 5 5 4 4 2 2 1 1).
Pewnego dnia obaj uczeni spotkali się, aby porównać kolekcje swoich układanek, okazało się
bowiem, że wiele z nich znajduje się w zbiorach obydwu panów. Chcieli takie egzemplarze
wyodrębnić, jednakże metoda wizualna porównywania układanek "na oko" okazała się bardzo
żmudna i mało skuteczna. Dobromir i Albert postanowili zatem porównywać swoje dzieła wg
opisów. Pojawiła się jednak kolejna przeszkoda: przecież opisy układanek dokonywane są w
różnych konwencjach! Podjęli wspólnie decyzję, że będą "przepisywać" opisy Dobromira do
formatu, w jakim dokonywał swoich opisów Albert. Pomóż uczonym przebrnąć przez ogromną
ilość konwersji opisów układanek z konwencji M do konwencji F...
Wejście:
W pierwszym wierszu wejścia znajduję się liczba całkowita C, 1 ≤ C ≤ 100, oznaczająca liczbę
zestawów danych. W kolejnych wierszach znajdują się zestawy danych, z których każdy zawiera
dokładnie 1 wiersz, będący opisem pewnej układanki w konwencji M. W wierszu każdego zestawu
znajduje się liczba N, oznaczająca ilość wszystkich kamyków w układance, 1 ≤ N ≤ 1000000, oraz
po spacji oddzielone pojedynczymi spacjami liczby a1, a2, ..., ai (1 ≤ i ≤ 1000; 1 ≤ ai ≤ 1000)
określające ilości kamyków w kolejnych kolumnach układanki.
Wyjście:
W C wierszach wyjścia należy podać wyznaczony dla każdego zestawu danych opis układanki w
konwencji F. Kolejne liczby stanowiące opis powinny być oddzielone pojedynczymi spacjami.
     */
    public sealed class Kamyki : ProblemBase
    {
        protected override string Input => "2\r\n24 8 6 4 4 2\r\n19 5 3 3 3 2 1 1 1";

        protected override string Output => "5 5 4 4 2 2 1 1\r\n8 5 4 1 1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int[] tab = input[i].Split(' ').Select(s => int.Parse(s)).Skip(1).ToArray();
                List<int> result = [];
                int index = tab.Length - 1;
                for (int j = 1; j <= tab[0]; j++)
                {
                    while (tab[index] < j)
                        index--;
                    result.Add(index + 1);
                }
                output.Add(string.Join(" ", result));
            }
        }
    }
}
