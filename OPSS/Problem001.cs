namespace OPSS
{
    /* 1/5
     * 
Zadanie
Napisz program, który dla każdego zestawu danych:
● Wczytuje liczby a,b ze standardowego wejścia,
● Oblicza wartość NWD(a,b),
● Wypisuje wynik na standardowe wyjście.
Wejście
Pierwsza linia zawiera dokładnie jedna liczbę n, 1 ≤ n ≤ 50000, będąca liczba zestawów danych. W
n kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z dwóch
liczb a,b, 1 ≤ a,b ≤ 100000000, oddzielonych pojedynczą spacją.
Wyjście
Program powinien wypisać na standardowe wyjście n linii. I-ta linia powinna zawierać dokładnie
jedna liczbę naturalna, będącą największym wspólnym dzielnikiem liczb występujących w i-tym
zestawie danych.
     */
    public sealed class ProblemEuklidesa : ProblemBase
    {
        protected override string Input => "3\r\n2 6\r\n4 14\r\n5 7";

        protected override string Output => "2\r\n2\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                if(a > b)
                {
                    int temp = a;
                    a = b; 
                    b = temp;
                }
                while(a > 1)
                {
                    int temp = a;
                    a = b % a;
                    b = temp;
                }
                output.Add($"{(a == 0 ? b : a)}");
            }
        }
    }
}
