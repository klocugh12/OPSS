namespace OPSS
{
    /* 1/5
     * 
Dla zadanej liczby naturalnej należącej do przedziału [1..1000000] wyświetl sumę jej naturalnych dzielników.
Wejście

Pierwsza linia określa ilość zestawów danych (nie więcej niż 1000).

Każdy zestaw danych składa się z jednej liczby naturalnej.
Wyjście

Dla każdego zestawu danych jedna liczba będąca sumą naturalnych dzielników danej liczby.
     */
    public sealed class SumaDzielnikow : ProblemBase
    {
        protected override string Input => "3\r\n5\r\n10\r\n15";

        protected override string Output => "6\r\n18\r\n24";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[i]);
                int result = n + 1;
                int limit = (int)Math.Sqrt(n);
                for (int j = 2; j <= limit; j++)
                    if(n % j == 0)
                        result += j + (n / j);
                output.Add(result.ToString());
            }
        }
    }
}
