namespace OPSS
{
    /* Difficulty: 2/5
     * 
Zadanie
Napisz program, który dla zadanej listy czynników, wyznaczy liczbę cyfr w zapisie dziesiętnym
iloczynu.
Wejście
W pierwszym wierszu wejścia znajduje się liczba zestawów danych C, 1 ≤ C ≤ 5000. Każdy zestaw
danych składa się z dwóch wierszy: w pierwszym znajduje się liczba naturalna N, oznaczająca
liczbę czynników badanego iloczynu, 1 ≤ N < 1000, natomiast w drugim podanych jest N liczb
całkowitych, oddzielonych pojedynczą spacją, będących czynnikami iloczynu. Każdy czynnik jest
liczbą składającą się z co najwyżej trzech cyfr.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać liczbę cyfr, z których
składa się zapis dziesiętny iloczynu.
     */
    public sealed class Multyplikator : ProblemBase
    {
        protected override string Input => "2\r\n2\r\n2 5\r\n5\r\n10 10 10 10 5";

        protected override string Output => "2\r\n5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                output.Add(Math.Floor(input[i << 1].Split(' ').Select(s => Math.Log10(int.Parse(s))).Sum() + 1.0).ToString());
            }
        }
    }
}
