using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * Kasia niedawno poznała wszystkie literki w szkole. Z wielką pasją potrafiła całe dnie spędzać na
pisaniu długich słów swoim ulubionym flamastrem. Pisała i pisała "tasiemce" tak długo, aż
flamaster wypisał się. Kasia posmutniała. Z trudem, ale udało jej się uprosić swoją mamę, aby
kupiła jej nowy pisak. Musiała jednak obiecać, że tym razem będzie bardziej oszczędna przy jego
używaniu żeby wystarczył na dłużej. Kasia zaczęła zastanawiać się w jaki sposób będzie mogła
zrealizować obietnicę daną mamie.
Postanowiła, że aby zaoszczędzić wkład flamastra będzie wypisywała skróconą wersję
wymyślanych wyrazów. Jeśli miała zamiar napisać więcej niż dwie takie same literki obok siebie w
wyrazie, to teraz napisze literkę a następnie liczbę, określającą ilość wystąpień tej literki.
Zadanie
Twoim zadaniem jest dla zadanego wyrazu, który wymyśliła Kasia, podanie skróconej wersji tego
wyrazu.
Wejście
W pierwszej linijce wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 50, oznaczająca ilość zestawów
danych. W kolejnych C wierszach wejścia znajdują się zestawy danych. Każdy zestaw składa się z
niepustego wyrazu złożonego z samych dużych liter alfabetu amerykańskiego. Długość wyrazu nie
przekracza 200 znaków.
Wyjście
Dla każdego zestawu danych, dla zadanego wyrazu, na wyjściu powinna znaleźć się jego skrócona
wersja.
     */
    public sealed class Flamaster : ProblemBase
    {
        protected override string Input => "4\r\nOPSS\r\nABCDEF\r\nABBCCCDDDDEEEEEFGGHIIJKKKL\r\nAAAAAAAAAABBBBBBBBBBBBBBBB";

        protected override string Output => "OPSS\r\nABCDEF\r\nABBC3D4E5FGGHIIJK3L\r\nA10B16";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                StringBuilder result = new();
                int count = 1;
                for (int j = 1; j <= input[i].Length; j++)
                {
                    if (j < input[i].Length && input[i][j] == input[i][j - 1])
                        count++;
                    else
                    {
                        if (count == 1)
                        {
                            result.Append(input[i][j - 1]);
                        }
                        else if (count == 2)
                        {
                            result.Append(input[i][j - 1]);
                            result.Append(input[i][j - 1]);
                        }
                        else
                        {
                            result.Append(input[i][j - 1]);
                            result.Append(count);
                        }
                        count = 1;
                    }
                }
                output.Add(result.ToString());
            }
        }
    }
}
