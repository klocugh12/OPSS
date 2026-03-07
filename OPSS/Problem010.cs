namespace OPSS
{
    /* 2/5
     * 
Zadanie
Twoim zadaniem jest wyznaczyć liczbę M = 11^n.
Wejście
Pierwsza linijka wejścia określa liczbę zestawów danych (0 < i ≤ 500). Każdy zestaw danych składa
się z jednej linijki, w której pojawia się liczba n (0 ≤ n ≤ 200).
Wyjście
Dla każdego zestawu danych odpowiedz powinna składać się z jednej liczby M (M = 11^n).
Uwaga. Liczba cyfr liczby M nie przekroczy 256.
     */
    public sealed class DziwneWlasnosciJedenastu : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n10";

        protected override string Output => "1331\r\n25937424601";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                byte[] lm1 = new byte[a + 1];
                lm1[lm1.Length - 1] = 1;
                for (int j = 0; j < a; j++)
                {
                    for (int k = 0; k <= j; k++)
                    {
                        lm1[lm1.Length - (j + 2) + k] += lm1[lm1.Length - (j + 1) + k];
                        int l = lm1.Length - (j + 2) + k;
                        while (lm1[l] > 9)
                        {
                            lm1[l - 1]++;
                            lm1[l] %= 10;
                            l--;
                        }
                    }
                }
                output.Add(string.Join("", lm1));
            }
        }
    }
}
