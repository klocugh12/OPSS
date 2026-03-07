namespace OPSS
{
    /* 2/5
     * Zadanie
Pomóż Ibn-al-Bannanowi w znalezieniu kwadratu liczby składającej się z samych jedynek.
Wejście
Pierwsza linia zawiera dokładnie jedną liczbę k, 1 ≤ k ≤ 500, będącą liczbą zestawów danych. W k
kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z jednej linii
zawierającej dokładnie jedną liczbę n (0 < n ≤ 200) oznaczającą liczbę jedynek w liczbie, którą
należy podnieść do kwadratu.
Wyjście
Program powinien wypisać na standardowe wyjście k linii. Dla każdego zestawu danych program
powinien wypisać kwadrat wejściowej liczby składającej się z n jedynek.
     */
    public sealed class KwadratJedynek : ProblemBase
    {
        protected override string Input => "1\r\n2";

        protected override string Output => "121";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                byte[] lm1 = new byte[(a << 1) - 1];
                for(int j = 0; j < a; j++)
                {
                    for(int k = 0; k < a; k++)
                    {
                        lm1[lm1.Length - (j + k + 1)]++;
                        int l = lm1.Length - (j + k + 1);
                        while(lm1[l] == 10)
                        {
                            lm1[l - 1]++;
                            lm1[l] = 0;
                            l--;
                        }
                    }
                }
                output.Add(string.Join("", lm1));
            }
        }
    }
}
