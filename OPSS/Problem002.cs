namespace OPSS
{
    /* 1/5
     * 
Zadanie
Napisz program, który dla każdego zestawu danych:
● Wczytuje liczby a,b ze standardowego wejścia,
● Oblicza wartość a^b,
● Wypisuje wynik na standardowe wyjście.
Wejście
Pierwsza linia zawiera dokładnie jedna liczbę n, 1<=n<=200000, będąca liczbą zestawów danych.
W n kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z dwóch
liczb a,b, 1=<a<=5, 1<=b<=10, oddzielonych pojedyncza spacja..
Wyjście
Program powinien wypisać na standardowe wyjście n linii. I-ta linia powinna zawierać dokładnie
jedną liczbę naturalna, będącą wartością wyrażenia a^b, gdzie a,b to liczby występujące w i-tym
zestawie danych
     */
    public sealed class WenusjanskieDzialki : ProblemBase
    {
        protected override string Input => "3\r\n2 6\r\n3 3\r\n1 10";

        protected override string Output => "64\r\n27\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = 1;
                while(b > 0)
                {
                    if (b % 2 == 1)
                    {
                        c *= a;
                        b--;
                    }
                    else
                    {
                        a *= a;
                        b >>= 1;
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
