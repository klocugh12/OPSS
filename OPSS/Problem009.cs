namespace OPSS
{
    /* 1/5
     * 
     * Zadanie
Twoim zadaniem jest napisać program, który wyliczy sumę wszystkich liczb całkowitych leżących
pomiędzy 1 a N (włącznie).
Wejście
Pierwsza linia zawiera dokładnie jedną liczbę naturalną n, 1<=n<=200000, będącą liczbą zestawów
danych. W n kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z
jednej liczby całkowitej N.
Wyjście
Program powinien wypisać na standardowe wyjście n linii. I-ta linia powinna zawierać sumę
wszystkich liczb całkowitych leżących pomiędzy 1 a N. Gwarantujemy, że wartość bezwzględna
sumy nie przekroczy 2^31
     */
    public sealed class Suma : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n2\r\n5\r\n7";

        protected override string Output => "6\r\n3\r\n15\r\n28";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                uint a = uint.Parse(input[i]);
                output.Add(((a * (a + 1)) >> 1).ToString());
            }
        }
    }
}
