namespace OPSS
{
    /* 4/5
     * Liczbę całkowitą dodatnią L nazwiemy samoopisującą się liczbą, jeżeli jej pierwsza cyfra w zapisie
dziesiętnym (najbardziej znacząca cyfra - pierwsza cyfra od lewej) oznacza liczbę wystąpień cyfry
'0' w zapisie tej liczby, druga cyfra oznacza liczbę wystąpień '1' w zapisie tej liczby, itd...
Zadanie
Dla zadanego n należy wyznaczyć największą liczbę samoopisującą nie większą od n.
Wejście
W pierwszym wierszu wejścia znajduje się liczba całkowita D, 1 ≤ D ≤ 100, oznaczająca liczbę
zestawów danych. W każdym z D kolejnych wierszy znajduje się liczba całkowita n, 0 ≤ n < 1010,
po jednej dla każdego zestawu.
Wyjście
Na wyjściu, w jednym wierszu dla każdego zestawu, należy wypisać największą liczbę
samoopisującą nie większą od zadanej liczby n. W przypadku braku rozwiązania należy wypisać -1.
     */
    public sealed class SamoopisujaceSieLiczby : ProblemBase
    {
        protected override string Input => "1\r\n2000";

        protected override string Output => "1210";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<long> results = [];
            int len = 4;
            while (len <= 10)
            {
                foreach (int zeros in Enumerable.Range((len - (len > 5 ? 4 : 3)), 2))
                {
                    int[] test = new int[len];
                    test[0] = zeros;
                    int toReplace = test.Length - zeros - 1;
                    int remaining = len - zeros;
                    List<int> digits = [zeros];
                    while (remaining > 0)
                    {
                        if (remaining == toReplace)
                        {
                            digits.Add(1);
                            remaining--;
                        }
                        else
                        {
                            digits.Add(2);
                            remaining -= 2;
                        }
                        toReplace--;
                    }
                    int index = 1;
                    while (index < test.Length - 1)
                    {
                        test[index] = digits.Count(i2 => i2 == index);
                        index++;
                    }
                    if (Enumerable.Range(0, test.Length).All(t => test[t] == test.Count(t2 => t2 == t)))
                    {
                        long val = 0;
                        foreach (var x in test)
                            val = val * 10 + x;
                        results.Add(val);
                    }
                }
                len++;
                
            }
            for (int k = 0; k < N; k++)
            {
                int c = int.Parse(input[k + 1]);
                int j = 0;
                while (j < results.Count && c >= results[j])
                    j++;
                output.Add(j == 0 ? "-1" : results[j - 1].ToString());
            }
        }
    }
}
