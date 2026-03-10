namespace OPSS
{
    /* Difficulty: 2/5
     * 
Każdego lata szpitale w Opsslandii narzekają na niewystarczającą liczbę osób oddających krew.
Często też brakuje jednostek krwi dla przebywających aktualnie w szpitalu pacjentów
potrzebujących krwi. Problem jest jednak bardziej skomplikowany. Każdy mieszkaniec Opsslandii
posiada jedną z N grup krwi numerowanych od 1 do N. Mówimy, że grupa krwi g1 jest zgodna z
grupą krwi g2 wtedy i tylko wtedy, gdy osoba z grupą krwi g2 może przyjąć krew grupy g1. Każda
grupa krwi jest zgodna sama ze sobą. Fakt zgodności grupy g1 z grupą g2 nie musi oznaczać, że
grupa g2 jest zgodna z grupą g1.
Napisz program, który stwierdzi czy jednostki krwi występujące w szpitalu wystarczą dla
wszystkich potrzebujących krwi pacjentów uwzględniając zgodność grup krwi.
Wejście
Pierwsza linia zawiera liczbę całkowitą C (1 ≤ C ≤ 10) określającą liczbę zestawów danych. W
następnych liniach opisane są kolejno po sobie zestawy danych. W pierwszej linii zestawu danych
znajduje się jedna liczba całkowita N (1 ≤ N ≤ 100) określająca liczbę grup krwi. W drugiej linii
zestawu danych znajduje się N liczb całkowitych di (0 ≤ di ≤ 1000). Liczba i-ta w tej linii (1 ≤ i ≤ N)
określa liczbę dostępnych jednostek krwi grupy o numerze i. W trzeciej linii zestawu danych
znajduje się N liczb całkowitych bi (0 ≤ bi ≤ 1000). Liczba i-ta w tej linii (1 ≤ i ≤ N) określa liczbę
jednostek krwi jakie są potrzebne dla pacjentów z grupą krwi o numerze i. W linii o numerze i+3 (1
≤ i ≤ N) zestawu danych znajduje się liczba całkowita Ci (1 ≤ Ci ≤ N), po której następuje Ci liczb
całkowitych określających numery grup, z którymi zgodna jest grupa o numerze i.
Wyjście
W oddzielnych liniach dla każdego zestawu danych należy wypisać słowo TAK jeśli potrzeby
pacjentów na krew da się zaspokoić przez posiadane jednostki krwi lub NIE w przeciwnym
przypadku.
     */
    public sealed class GrupyKrwi : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n6 2 2 4\r\n5 3 1 5\r\n4 1 2 3 4\r\n3 2 3 4\r\n2 3 4\r\n1 4\r\n2\r\n1 5\r\n2 3\r\n2 1 2\r\n1 2";

        protected override string Output => "TAK\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[j]);
                j++;
                var stored = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                var needed = input[j].Split(' ').Select(s => int.Parse(s));
                stored = stored.Zip(needed, (a, b) => a - b).ToArray();
                for(int k = 0; k < n; k++)
                {
                    j++;
                    if (stored[k] <= 0)
                        continue;
                    var compatible = input[j].Split(' ').Skip(1).Select(s => int.Parse(s) - 1);
                    foreach (var c in compatible.Where(c2 => stored[c2] < 0))
                        stored[c] += stored[k]; 
                }
                output.Add(stored.All(c => c >= 0) ? "TAK" : "NIE");
                j++;
            }
        }
    }
}
