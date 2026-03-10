using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * 
Wejście
Pierwsza linia wejścia zawiera jedną liczbę naturalną N (1 ≤ N ≤ 1000000), określającą liczbę cyfr
wejściowych liczb. (aby długości liczb były takie same, mogą być dodane na początku nieznaczące
zera). Następnie na wejściu podane są te dwie liczby - wypisane są one w dwóch kolumnach,
oddzielonych jedną spacją. Każda kolumna określą jedną liczbę. Obie liczby są większe od 0, a
długość ich sumy nie przekracza N.
Wyjście
Wyjście powinno zawierać dokładnie N cyfr w jednej linii, reprezentujących sumę tych dwóch
liczb.
     */
    public sealed class SuperdlugaSuma : ProblemBase
    {
        protected override string Input => "4\r\n0 4\r\n4 2\r\n6 8\r\n3 7";

        protected override string Output => "4750";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int current = 0, prev = 0;
            bool nonZero = false;
            StringBuilder result = new();
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                prev = current;
                current = int.Parse(splits[0]) + int.Parse(splits[1]);
                if (current > 9)
                {
                    result.Append(prev + 1);
                    current -= 10;
                }
                else
                {
                    if (nonZero)
                        result.Append(prev);
                    else
                        nonZero = current > 0;                    
                }    
            }
            result.Append(current);
            output.Add(result.ToString());
        }
    }
}
