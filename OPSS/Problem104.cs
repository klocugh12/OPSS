namespace OPSS
{
    /* 2/5
     * 
Zadanie
Napisz program, który pomoże nie tylko Agatce, ale również innym potencjalnym hodowcom
rybek akwariowych określić, jaką temperaturę powinna mieć woda w akwarium, aby mogły w niej
przeżyć rybki zadanych gatunków.
Wejście
Pierwsza linia wejścia zawiera liczbę naturalną N określającą liczbę gatunków rybek (1 ≤ N ≤ 50).
Kolejne N wierszy zawiera informacje dotyczące warunków temperaturowych dla kolejnych
gatunków rybek (gatunki rybek ponumerowane są kolejnymi liczbami całkowitymi od 1 do N).
Każdy z warunków składa się z wiersza zawierającego dwie liczby naturalne: tmin i tmax,
oddzielone pojedynczą spacją, oznaczające, że dany gatunek jest w stanie przetrwać w temperaturze
większej lub równej tmin i mniejszej lub równej tmax (3 ≤ tmin ≤ tmax ≤ 38). Kolejny wiersz wejścia
zawiera liczbę naturalną K oznaczającą liczbę zapytań (1 ≤ K ≤ 1000). W następnych K wierszach
znajdują się kolejne zapytania. Każde zapytanie składa się z jednej linii, w której znajdują się liczby
naturalne: m, a1, ..., am, oddzielone pojedynczą spacją (1 ≤ m ≤ N; 1 ≤ ai ≤ N, dla i = 1, 2, ..., m).
Liczba m oznacza liczbę gatunków, które hodowca chce hodować. Kolejne m różnych od siebie
liczb: a1, ..., am, określa numery gatunków rybek, które mają być hodowane.
Wyjście
Dla każdego zapytania, w kolejnych liniach wyjścia, należy wypisać dwie liczby t1 i t2 oddzielone
pojedynczą spacją, gdzie t1 ≤ t2, określające końce maksymalnego (różnica t2 - t1 jest
maksymalna) przedziału domkniętego temperatur, w którym dane gatunki rybek są w stanie
przetrwać. Jeśli taki przedział nie istnieje, należy wypisać słowo NIE.
     */
    public sealed class Rybki : ProblemBase
    {
        protected override string Input => "4\r\n22 26\r\n18 28\r\n20 28\r\n8 20\r\n2\r\n3 1 2 3\r\n2 1 4";

        protected override string Output => "22 26\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int j = 1;
            List<int[]> ranges = [];
            int N = int.Parse(input[0]);
            for (int i = 0; i < N; i++)
            {
                ranges.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                j++;
            }
            N = int.Parse(input[j]);
            j++;
            for (int i = 0; i < N; i++)
            {
                var fishes = input[j].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                int min = 3, max = 38;
                foreach(var f in fishes)
                {
                    min = Math.Max(min, ranges[f - 1][0]);
                    max = Math.Min(max, ranges[f - 1][1]);
                }
                output.Add(min <= max ? $"{min} {max}" : "NIE");
                j++;
            }
        }
    }
}
