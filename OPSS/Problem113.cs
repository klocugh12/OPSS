namespace OPSS
{
    /* 3/5
     * Wejście
W pierwszym wierszu wejścia znajduje się liczba C określająca liczbę zestawów danych (1 ≤ C ≤
100). W kolejnych C wierszach wejścia znajdują się zestawy danych. Każdy z C zestawów danych
składa się z jednej liczby n (1 ≤ n ≤ 100000).
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać liczbę różnych wyników
iloczynu liczb naturalnych a i b, gdzie 1 ≤ a, b ≤ n.
     */
    public sealed class Iloczyny : ProblemBase
    {
        protected override string Input => "2\r\n5\r\n8";

        protected override string Output => "14\r\n30";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> totals = [1];
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var k = int.Parse(input[i]);
                while(k > totals.Count)
                {
                    int j = totals.Count + 1;
                    int lim = (int)Math.Ceiling(Math.Sqrt(j));
                    int minDiv = 0;
                    int k2 = 2;
                    while (k2 <= lim && minDiv == 0)
                    {
                        if (j % k2 == 0)
                            minDiv = k2;
                        else
                            k2++;
                    }
                    totals.Add(totals[j - 2] + j - (k2 == 0 ? 0 : (j / k2 ) - 1));
                }
                output.Add(totals[k - 1].ToString());
            }
        }
    }
}
