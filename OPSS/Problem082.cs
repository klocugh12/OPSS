namespace OPSS
{
    /* 2/5
     * 
Wejście
W pierwszym wierszu wejścia znajduje się jedna liczba całkowita C, określająca liczbę zestawów
danych (1 ≤ C ≤ 100). W i+1 wierszu (i = 1, 2, ..., C) znajduje się liczba b zapisana w systemie
dwójkowym, składająca się z co najmniej 1 cyfry i co najwyżej 255 cyfr. Pierwszą (najbardziej
znaczącą) cyfrą w zapisie liczby b jest zawsze 1.
Wyjście
Dla każdego zestawu danych, program powinien wypisać na wyjściu najmniej znaczącą (ostatnią)
cyfrę w zapisie dwójkowym liczby fib(b).
     */
    public sealed class StarozytnaMaszyna : ProblemBase
    {
        protected override string Input => "2\r\n111\r\n1001";

        protected override string Output => "1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                bool odd = true;
                int mod = 0;
                for(int j = input[i].Length - 1; j >= 0; j--)
                {
                    if (input[i][j] == '1')
                    {
                        mod = (mod + (odd ? 1 : 2)) % 3;
                    }
                    odd = !odd;
                }
                output.Add(mod == 0 ? "0" : "1");
            }
        }
    }
}
