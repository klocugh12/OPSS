namespace OPSS
{
    /* 3/5
     * Pewna firma zajmująca się tworzeniem oprogramowania spostrzegła niedawno, że jej system łat
jest bardzo skomplikowany. Programy przez nią produkowane składają się z K komponentów
ponumerowanych kolejnymi liczbami naturalnymi od 1 do K. Każda łata uaktualnia część
komponentów programu do pewnej wersji. Łatę uaktualniającą do wersji v+1 można użyć tylko
wtedy, gdy wszystkie komponenty uaktualniane przez tę łatę mają wersję v.
Zadanie
Napisz program, który sprawdzi, czy przy zadanych łatach da się uaktualnić cały program, czyli
wszystkie jego komponenty, z wersji 1 do wersji zadanej w pliku wejściowym. Jeśli da się
uaktualnić cały program, wówczas należy wypisać numery łat, z których trzeba skorzystać.
Wejście
Pierwsza linia wejścia zawiera liczbę zestawów danych D, 1 ≤ D ≤ 10. W następnych liniach
znajdują się kolejno po sobie opisy D zestawów danych. Pierwsza linia zestawu danych zawiera
trzy liczby naturalne oddzielone pojedynczymi spacjami: K, L oraz V, 1 ≤ K ≤ 10, 1 ≤ L ≤ 10000, 2
≤ V ≤ 1000. K oznacza liczbę komponentów, z których składa się program, L - liczbę łat, którymi
można uaktualniać program, natomiast V - wersję, do której należy uaktualnić wszystkie
komponenty programu. W kolejnych liniach zestawu danych znajdują się opisy L łat. Opis łaty
składa się z dwóch linii. W pierwszej linii opisu łaty znajdują się dwie liczby naturalne oddzielone
spacją: VL, oznaczająca wersję, do której łata uaktualnia komponenty oraz KL, oznaczająca liczbę
komponentów uaktualnianych przez łatę (2 ≤ VL ≤ V, 1 ≤ KL ≤ K). W drugiej linii opisu łaty
znajduje się KL numerów komponentów, które uaktualnia łata. Sąsiednie numery w pliku
wejściowym oddzielone są od siebie spacją. Numery uaktualnianych komponentów podane są w
kolejności rosnącej. Łaty są ponumerowane kolejnymi liczbami naturalnymi od 1 do L w kolejności
pojawiania się ich opisów w pliku wejściowym.
Wyjście
W oddzielnych liniach dla każdego zestawu danych należy wypisać numery łat w kolejności, w
jakiej powinny być stosowane. Jeśli istnieje wiele rozwiązań, należy wypisać ciąg numerów łat
najmniejszy leksykograficznie. Jeśli nie da się uaktualnić programu do wersji V, wówczas trzeba
wypisać liczbę -1.
     */
    public sealed class Laty : ProblemBase
    {
        protected override string Input => "2\r\n7 10 4\r\n4 2\r\n1 2\r\n3 4\r\n1 2 3 4\r\n2 2\r\n4 5\r\n2 3\r\n1 2 3\r\n3 2\r\n1 7\r\n3 3\r\n5 6 7\r\n2 2\r\n6 7\r\n2 3\r\n3 4 5\r\n2 7\r\n1 2 3 4 5 6 7\r\n4 5\r\n3 4 5 6 7\r\n3 4 3\r\n3 2\r\n1 2\r\n3 2\r\n2 3\r\n2 3\r\n1 2 3\r\n3 2\r\n1 3";

        protected override string Output => "3 4 2 1 7 6 10\r\n-1";

        class Patch
        {
            public int Number;
            public int Version;
            public required int[] Modules;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                List<Patch> patches = [];
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                int[] versions = new int[splits[0]];
                for (int k = 0; k < versions.Length; k++)
                    versions[k] = 1;
                int package = 1;
                List<int> order = [];
                for (int k = 0; k < splits[1]; k++)
                {
                    int version = int.Parse(input[j].Split(' ')[0]);
                    j++;
                    var modules = input[j].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                    if (modules.All(m => versions[m] == version - 1))
                    {
                        foreach (var m in modules)
                            versions[m]++;
                        order.Add(package);
                        var toApply = patches.FirstOrDefault(p => p.Modules.All(m => versions[m] == p.Version - 1));
                        while (toApply != null)
                        {
                            foreach (var m in toApply.Modules)
                                versions[m]++;
                            order.Add(toApply.Number);
                            patches.Remove(toApply);
                            toApply = patches.FirstOrDefault(p => p.Modules.All(m => versions[m] == p.Version - 1));
                        }
                    }
                    else
                        patches.Add(new Patch()
                        {
                            Number = package,
                            Version = version,
                            Modules = modules
                        });
                    package++;
                    j++;
                }
                while(patches.Count > 0)
                {
                    if (patches[0].Modules.All(m => versions[m] == patches[0].Version - 1))
                    {
                        foreach (var m in patches[0].Modules)
                            versions[m]++;
                        order.Add(patches[0].Number);
                    }
                    patches.RemoveAt(0);
                }
                if (versions.All(v => v == splits[2]))
                    output.Add(string.Join(" ", order));
                else
                    output.Add("-1");
            }
        }
    }
}
