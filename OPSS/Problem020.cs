namespace OPSS
{
    /* 1/5
     * 
Zadanie
Napisz program, który dla każdego zestawu danych:
● wczyta liczbę n ze standardowego wejścia,
● wyznaczy cyfrę jedności liczby 3^n,
● zapisze wynik na standardowe wyjście.
Wejście
Pierwsza linijka wejścia zawiera dokładnie jedną liczbę całkowitą d, 1 ≤ d ≤ 10, określającą ilość
zestawów danych. W kolejnych d liniach wejścia znajdują się zestawy danych. Każdy zestaw
danych składa się z jednej linii i zawiera jedną nieujemną liczbę całkowitą n, 0 ≤ n < 10200.
Wyjście
Twój program powinien wypisać d liczb, każdą w osobnym wierszu. Liczba w wierszu i powinna
byc odpowiedzią dla i-tego zestawu danych.
     */
    public sealed class Potega : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n3\r\n2005";

        protected override string Output => "9\r\n7\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string[] digits = ["1", "3", "9", "7"];
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                output.Add(digits[a % 4]);
            }
        }
    }
}
