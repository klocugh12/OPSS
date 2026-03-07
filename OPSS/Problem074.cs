namespace OPSS
{
    /* 3/5
     * 
Zadanie
Dla każdej zadanej normy wyznacz liczbę próbek skał, które ją spełniają.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, określająca liczbę zestawów danych (1 ≤ C ≤
3). W kolejnych wierszach znajdują się opisy zestawów danych.
W pierwszej linii opisu zestawu znajduje się liczba N, oznaczająca liczbę posiadanych przez
naukowców próbek (1 ≤ N ≤ 100000). W następnej linii zestawu znajduje się N liczb całkowitych:
a1, a2,..., aN, opisujących zawartość cząstek Ni w kolejnych próbkach (0 ≤ ai < 2^31, 1 ≤ i ≤ N). W
trzeciej linii opisu zestawu znajduje się liczba P, oznaczającą liczbę norm, na podstawie których
próbki będą weryfikowane (1 ≤ P ≤ 5000). Kolejne P linii wejścia zawiera opis P norm.
Opis jednej normy składa się z dwóch liczb całkowitych: bk, ck (0 ≤ bk ≤ ck < 2^31, 1 ≤ k ≤ P).
Próbka skały ai spełnia k-tą normę jeśli bk ≤ ai ≤ ck.
Wszystkie liczby podane na wejściu w tym samym wierszu oddzielone są od siebie pojedynczą
spacją.
Wyjście
Dla każdego zestawu, w osobnych liniach wyjścia, należy wypisać P liczb: p1, p2,..., pP, gdzie pk (1
≤ k ≤ P) równa jest liczbie próbek spełniających zadaną k-tą normę zestawu.
     */
    public sealed class Normy : ProblemBase
    {
        protected override string Input => "1\r\n5\r\n123 304 200604 12 3000\r\n3\r\n1 300\r\n300 3000\r\n5 5";

        protected override string Output => "2\r\n2\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 0; i < N; i++)
            {
                j++;
                List<int> samples = input[j].Split(' ').Select(s => int.Parse(s)).ToList();
                samples.Sort();
                j++;
                int c = int.Parse(input[j]);
                j++;
                for(int k = 0; k < c; k++)
                {
                    int[] bounds = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (samples[0] > bounds[1] || samples[samples.Count - 1] < bounds[0])
                    {
                        output.Add("0");
                        j++;
                        continue;
                    }
                    int l = 0, p = samples.Count - 1, m;
                    while(p != l)
                    {
                        m = (l + p) >> 1;
                        if (samples[m] < bounds[0])
                            l = m + 1;
                        else
                            p = m;
                    }
                    int first = l;
                    l = 0; 
                    p = samples.Count - 1;
                    while (p != l)
                    {
                        m = (l + p) >> 1;
                        if (samples[m] > bounds[1])
                            p = m - 1;
                        else
                        {
                            if (samples[m + 1] > bounds[1])
                                p = m;
                            else
                                l = m + 1;
                        }
                    }
                    int second = l;
                    output.Add((second - first + 1).ToString());
                    j++;
                }      
            }
        }
    }
}
