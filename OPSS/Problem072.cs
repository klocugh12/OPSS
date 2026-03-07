namespace OPSS
{
    /* 4/5
     * Wejście
W pierwszej linii wejścia znajduje się liczba C, określająca liczbę zestawów danych, 1 ≤ C ≤ 10. W
kolejnych wierszach znajdują się zestawy danych. Każdy z C zestawów danych składa się z 2 linii.
Pierwsza linia zawiera dwie liczby całkowite N, S, oddzielone pojedynczą spacją, gdzie N oznacza
ile liczb bierze udział w sumowaniu, zaś S to suma jakiej oczekujemy (1 ≤ N ≤ 500, -2^31 < S <
2^31). W drugim wierszu zestawu znajduje się N liczb całkowitych, oddzielonych pojedynczą
spacją: a1, a2, ..., an, -500 ≤ ai ≤ 500, dla i: 1 ≤ i ≤ N.
Wyjście
Dla każdego zestawu danych, w osobnej linii wyjścia, należy wypisać słowo TAK, jeśli istnieje
niepusty podciąg ciągu liczb a1, a2, ..., an, który daje zadaną sumę S, w przeciwnym razie należy
wypisać słowo NIE.
     */
    public sealed class Optymalizator : ProblemBase
    {
        protected override string Input => "2\r\n5 32\r\n1 2 5 10 25\r\n5 -16\r\n1 2 5 -10 -10";

        protected override string Output => "TAK\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[(i << 1) - 1].Split(' ');
                int sum = int.Parse(splits[1]);
                var list = input[i << 1].Split(' ').Select(s => int.Parse(s)).ToArray();
                int min = 0, max = 0;
                foreach(var l in list)
                {
                    if (l > 0)
                        max += l;
                    else
                        min += l;
                }
                if(sum > max || sum < min)
                {
                    output.Add("NIE");
                    continue;
                }
                bool[] possibles = new bool[max - min + 1];
                foreach(var p in list)
                {
                    if (p > 0)
                    {
                        int k = max - p;
                        while (k > 0)
                        {
                            if (possibles[k])
                                possibles[p + k] = true;
                            k--;
                        }
                    }
                    else
                    {
                        int k = p - min;
                        while (k < possibles.Length)
                        {
                            if (possibles[k])
                                possibles[p + k] = true;
                            k++;
                        }
                    }
                    possibles[p - min] = true;
                }
                output.Add(possibles[sum - min] ? "TAK" : "NIE");
            }
        }
    }
}
