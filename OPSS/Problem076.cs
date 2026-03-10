namespace OPSS
{
    /* Difficulty: 4/5
     * Pewna firma świadczy usługi transportowe, dokładniej mówiąc, transportuje kontenery w
Opsslandii. Opsslandia to kraj, w którym jest N miast, połączonych K drogami. Miasta
ponumerowane są kolejnymi liczbami całkowitymi od 1 do N. Każde dwa różne miasta w tym kraju
są połączone co najwyżej jedną drogą. Wszystkie drogi w Opsslandii są dwukierunkowe. Z każdego
miasta da się dotrzeć do dowolnego innego miasta, ale zdarzyć się może, że nie będzie istniała
pomiędzy nimi droga bezpośrednia i konieczny będzie przejazd przez inne miasta pośrednie. Na
każdej drodze obowiązuje dzienne ograniczenie liczby przewożonych kontenerów. Koszt
przewiezienia jednego kontenera pomiędzy dwoma bezpośrednio połączonymi miastami, czyli
inaczej mówiąc: przejazd jednego kontenera jedną drogą, wynosi 1 opssar (opssar jest oficjalną
walutą w Opsslandii). Na każdy kontener nie można wydać więcej niż 1 opssar dziennie (jeden
kontener może być przetransportowany co najwyżej przez jedną drogę dziennie).
Twoje zadanie polega na obliczeniu minimalnej liczby dni potrzebnej na przetransportowanie
wszystkich kontenerów z miasta nr 1 do miasta nr N, mając do dyspozycji określone fundusze
przeznaczone na ten cel.
Wejście
Pierwsza linia zawiera liczbę całkowitą D (1 ≤ D ≤ 20), określającą liczbę zestawów danych. W
następnych liniach opisane są kolejno po sobie zestawy danych. W pierwszej linii zestawu danych
znajdują się dwie liczby całkowite: liczba miast N (1 ≤ N ≤ 100) oraz liczba dróg K. W każdej z
kolejnych K linii zestawu danych znajdują się trzy liczby całkowite A, B i C: A i B (1 ≤ A, B ≤ N) są
numerami miast, pomiędzy którymi istnieje droga, przez którą dziennie nie można
przetransportować więcej niż C kontenerów (1 ≤ C ≤ 100). Ostatnia linia zestawu danych zawiera
dwie liczby całkowite: liczbę T będącą liczbą kontenerów, które należy przetransportować z miasta
nr 1 do miasta nr N oraz liczbę opssarów F przeznaczoną na transport kontenerów (1 ≤ T ≤ 10^7; 1
≤ F ≤ 2^31-1).
Wyjście
W oddzielnych liniach dla każdego zestawu danych należy wypisać minimalną liczbę dni potrzebną
do przetransportowania wszystkich kontenerów wykorzystując przy tym nie więcej niż
przeznaczoną na ten cel liczbę opssarów. Jeśli przeznaczone fundusze są niewystarczające do
przetransportowania wszystkich kontenerów, wówczas należy zamiast minimalnej liczby dni
wypisać -1.
     */
    public sealed class Transport : ProblemBase
    {
        protected override string Input => "1\r\n4 5\r\n1 2 5\r\n1 3 2\r\n2 3 1\r\n3 4 10\r\n2 4 3\r\n11 23";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int a = splits[0], b = splits[1];
                //start end limit length
                List<int[]> routes = [];
                j++;
                for (int k = 0; k < b; k++)
                {
                    splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (splits[0] == 1)
                        routes.Add([splits[0], splits[1], splits[2], 1]);
                    else
                        foreach (var route in routes.Where(r => splits[0] == r[1]).ToArray())
                        {
                            routes.Add([route[0], splits[1], Math.Min(route[2], splits[2]), route[3] + 1]);
                        }
                    j++;
                }
                routes.RemoveAll(r => r[1] != a);
                routes.Sort((a, b) => a[3].CompareTo(b[3]));
                splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int C = splits[0], F = splits[1];
                var groups = routes.GroupBy(r => r[3]).Select(g => (g.Key, g.Sum(r => r[2]))).ToList();
                if(F < C * groups[0].Key)
                {
                    j++;
                    output.Add("-1");
                    continue;
                }
                int days = groups[0].Key;
                do
                {
                    var allowed = groups.Where(g => g.Key <= days).Select(g => (days - g.Key + 1) * g.Item2).ToArray();
                    int x = 0;
                    int k = 0;
                    while(k < allowed.Length && x + allowed[k] <= F && x < C)
                    {
                        x += allowed[k];
                        k++;
                    }
                    if (x >= C)
                        break;
                    days++;
                }
                while (true);
                output.Add(days.ToString());
            }
        }
    }
}
