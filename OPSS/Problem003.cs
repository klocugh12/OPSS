namespace OPSS
{
    /* 1/5
     * 
Zadanie
Napisz program, który dla każdego zestawu danych:
● Wczytuje liczby n, a1,...,an ze standardowego wejścia,
● Oblicza wartość a1+a2+...+an,
● Wypisuje wynik na standardowe wyjście.
Wejście
Pierwsza linia zawiera dokładnie jedną liczbę naturalną C, 1<=C<=200000, będącą liczbą
zestawów danych. W C kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw
składa się z liczby n, 1=<n<=100000, oraz następujących po niej n liczb ai,0<=ai<=1000, 1<=i<=n,
oddzielonych pojedyncza spacja.
Wyjście
Program powinien wypisać na standardowe wyjście C linii. I-ta linia powinna
zawierać dokładnie jedną liczbę naturalną, będącą sumą liczb a1,..,an, gdzie n
oraz ai, 1<=i<=n to liczby występujące w i-tym zestawie danych.
     */
    public sealed class LaboratoryjneRozwazania : ProblemBase
    {
        protected override string Input => "2\r\n3 1 2 3\r\n4 0 2 0 0";

        protected override string Output => "6\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int N2 = int.Parse(splits[0]);
                int sum = 0;
                for (int j = 1; j <= N2; j++)
                    sum += int.Parse(splits[j]);
                output.Add(sum.ToString());
            }
        }
    }
}
