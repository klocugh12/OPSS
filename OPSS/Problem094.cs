namespace OPSS
{
    /* Difficulty: 2/5
     * Prawie każdy, kto instalował oprogramowanie na uniksowych systemach operacyjnych miał do
czynienia z zależnościami pomiędzy różnymi pakietami.
Zależność pomiędzy pakietami jest opisana w postaci par liczb (numerów pakietów) oddzielonych
od siebie pojedynczą spacją. Para A B (gdzie A, B są różnymi numerami pakietów) oznacza, że
pakiet A powinien być zainstalowany przed pakietem B.
Dla przykładu, zależności opisane przez następujące pary:
A B
B C
B D
mogą być instalowane w systemie w następującej kolejności:
A B C D
lub
A B D C.
Wejście
W pierwszej linii wejścia znajduje się liczba zestawów danych D, 1 ≤ D ≤ 10. W kolejnych liniach
znajdują się zestawy danych. Pierwsza linia zestawu zawiera dwie liczby N, 1 ≤ N ≤ 100000, oraz
Z, 0 ≤ Z ≤ 100000. Liczba N oznacza liczbę pakietów do zainstalowania (pakiety numerowane są
kolejnymi liczbami od 1 do N), natomiast liczba Z - liczbę zależności. W kolejnych Z liniach
zestawu znajduje się opis zależności w postaci par A, B (dwóch numerów pakietów oddzielonych
pojedynczą spacją), 1 ≤ A, B ≤ N.
Wszystkie zestawy danych są poprawne, co oznacza, że nie może dojść do zapętleń, np. A B (pakiet
B zależy od A), B C (pakiet C zależy od B), C A (pakiet A zależy od C).
Wyjście
Dla każdego zestawu danych program powinien wypisać listę numerów pakietów (oddzielonych
pojedynczą spacją) w kolejności w jakiej powinny być instalowane. Jeśli jest więcej niż jedno
rozwiązanie, program powinien wypisać pierwsze (najmniejsze) z nich w porządku
leksykograficznym.
     */
    public sealed class Zaleznosci : ProblemBase
    {
        protected override string Input => "2\r\n5 6\r\n1 2\r\n1 4\r\n3 1\r\n3 4\r\n4 5\r\n2 5\r\n4 3\r\n1 2\r\n2 3\r\n3 4";

        protected override string Output => "3 1 2 4 5\r\n1 2 3 4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                Dictionary<string, List<string>> dependencies = [];
                for (int k = 1; k <= splits[0]; k++)
                    dependencies.Add(k.ToString(), []);
                j++;
                for (int k = 0; k < splits[1]; k++)
                {
                    var splits2 = input[j].Split(' ');
                    dependencies[splits2[1]].Add(splits2[0]);
                    j++;
                }
                List<string> order = [];
                while(dependencies.Count > 0)
                {
                    string toRemove = dependencies.Keys.First(t => dependencies[t].Count == 0);
                    foreach (var dep in dependencies.Values.Where(l => l.Contains(toRemove)))
                        dep.Remove(toRemove);
                    dependencies.Remove(toRemove);
                    order.Add(toRemove);
                }
                output.Add(string.Join(" ", order));
            }
        }
    }
}
