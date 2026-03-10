namespace OPSS
{
    /* Difficulty: 2/5
     * 
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna k (1≤k≤10000), określająca ilość
wierszy w tabeli badań. W następnych k liniach znajdują się wiersze tabeli. Każdy wiersz składa się
z 11 liczb całkowitych: x1, x2, .., x10, y (0≤xi≤100, 0≤y≤1000). Oznacza on, iż w laboratorium
znajduje się y skał, których pierwszy parametr ma wartość x1, drugi - x2, ..., dziesiąty - x10.
Ponieważ tabelkę tworzyło kilku naukowców, wiersze z takimi samymi parametrami mogą się
powtarzać. W kolejnym wierszu znajduje się liczba naturalna m (1≤m≤10000), oznaczająca liczbę
pytań zadanych przez naukowców. W m następnych liniach wejścia znajdują się poszczególne
pytania. Każde pytanie składa się z warunków, mających postać 10 liczb całkowitych: a1, a2, ..,
a10 (-1≤ai≤100). Naukowców interesuje liczebność skał spełniających wszystkie 10 warunków.
Warunek ai=-1 oznacza, że wartość i-tego parametru może być dowolna. Natomiast ai<>-1 mówi,
że i-ty parametr ma mieć wartość ai.
Wyjście
Dla każdego pytania naukowców, w osobnej linii wyjścia, należy wypisać liczbę skał, które
spełniają warunki zadane przez naukowców.
     */
    public sealed class MarsjanskieSkaly : ProblemBase
    {
        protected override string Input => "5\r\n10 1 1 4 5 6 7 8 9 1 100\r\n1 2 4 4 5 5 5 5 5 1 200\r\n2 2 4 4 6 6 6 8 5 2 300\r\n10 2 1 9 5 5 5 5 5 3 400\r\n10 2 1 9 5 5 5 5 5 3 500\r\n3\r\n10 -1 -1 9 -1 -1 -1 -1 -1 -1\r\n-1 2 -1 -1 5 -1 -1 -1 -1 -1\r\n1 2 4 4 -1 5 5 5 5 -1";

        protected override string Output => "900\r\n1100\r\n200";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int[]> samples = [];
            for(int i = 1; i <= N; i++)
            {
                samples.Add(input[i].Split(' ').Select(s => int.Parse(s)).ToArray());
            }
            N = int.Parse(input[N + 1]);
            for (int i = 0; i < N; i++)
            {
                List<(int, int)> pattern = [];
                int j = 0;
                foreach(var k in input[samples.Count + i + 2].Split(' ').Select(s => int.Parse(s)))
                {
                    if (k >= 0)
                        pattern.Add((j, k));
                    j++;
                }
                int sum = 0;
                foreach(var s in samples)
                {
                    if (pattern.All(p => s[p.Item1] == p.Item2))
                        sum += s[s.Length - 1];
                }
                output.Add(sum.ToString());
            }
        }
    }
}
