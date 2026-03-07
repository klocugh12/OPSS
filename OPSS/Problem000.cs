namespace OPSS
{
    /* 1/5
     * Liczby Fibonacciego zdefiniowane są następująco:
F(0)=1,
F(1)=1,
F(n)=F(n-1)+F(n-2), n>1.
Jaka jest wartość F(n)?
Zadanie
Napisz program, który dla każdego zestawu danych:
● Wczyta liczbę n ze standardowego wejścia,
● Obliczy wartość F(n),
● Zapisze wynik na standardowe wyjście.
     */
    public sealed class TreningLiczbyFibonacciego : ProblemBase
    {
        protected override string Input => "3\r\n4\r\n9\r\n14";

        protected override string Output => "5\r\n55\r\n610";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = 1, b = 1, c = 1;
                int N2 = int.Parse(input[i]);
                while(c < N2)
                {
                    int temp = b;
                    b = a + b;
                    a = temp;
                    c++;
                }
                output.Add(b.ToString());
            }
        }
    }
}
