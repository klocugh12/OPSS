using System.Globalization;

namespace OPSS
{
    /* Difficulty: 1/5
     * Wejście:
W pierwszym wierszu znajduje się liczba zestawów danych 0 < d ≤ 100. Każdy zestaw danych
składa się z trzech wierszy: w pierwszym mamy daną chwilę t w której chcemy wyznaczyć wartość
wielomianu, w drugim stopień wielomianu 0 ≤ N ≤ 100000, zaś w trzecim N + 1 współczynników
wielomianu oddzielonych spacjami poczynając od współczynnika przy najwyższej potędze. Na
przykład, dla wielomianu W(t) = a0 + a1 * t + a2 * t^2 + a3 * t^3 mamy ciąg: a3 a2 a1 a0.
Współczynniki wielomianu są liczbami rzeczywistymi - podanymi z dokładnoscią do 3 miejsc po
przecinku.
Wyjście:
Na wyjściu w kolejnych d wierszach powinny znaleźć się szukane wartości - liczby rzeczywiste -
zaokrąglone do 3 miejsc po przecinku. Elektrokardiogram powinien mieścić się na ekranie
elektrokardiografu - można więc bezpiecznie założyć, że wyniki będą mieściły się w standardowym
typie rzeczywistym
     */
    public sealed class DrJudym : ProblemBase
    {
        protected override string Input => "3\r\n0.100\r\n4\r\n-0.700 3.000 0.000 8.000 -5.000\r\n3.000\r\n3\r\n3.000 0.000 0.000 10000.000\r\n128.000\r\n2\r\n1.000 -1.000 1.000";

        protected override string Output => "-4.197\r\n10081.000\r\n16257.000";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 0; i < N; i++)
            {
                double x = double.Parse(input[3 * i + 1], CultureInfo.InvariantCulture);
                double[] coeffs = input[3 * (i + 1)].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                double y = 0;
                for (int j = 0; j < coeffs.Length; j++)
                {
                    y = y * x + coeffs[j];
                }
                output.Add(y.ToString("#.000").Replace(",", "."));
            }
        }
    }
}
