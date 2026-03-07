namespace OPSS
{
    /* 2/5
     * Małgosia przygotowywała urodzinowe przyjęcie. Upiekła tort i zajęła się układaniem listy gości.
Liczbę gości N wyznaczała następująco: wybrała pewną liczbę cięć K, a następnie podzieliła tort K
prostymi na maksymalną możliwą liczbę kawałków N. Każdy zaproszony gość dostałby więc
dokładnie jeden kawałek tortu.
Małgosia nie mogła się zdecydować jak dobrać liczbę cięć K. Zapisywała więc liczby cięć i
odpowiadające im liczby gości w prototypowej bazie danych Miracle 13k, nad którą pracował jej
ojciec. Wersje testowe (i nie tylko testowe) tej bazy mają to do siebie, że ulegają awarii w
najbardziej nieodpowiednich momentach. Nie inaczej było tym razem. Pech chciał, że część danych
uległa zniszczeniu i udało się odzyskać tylko liczby gości.
Jak teraz Małgosia ma podzielić tort? Los przyjęcia spoczął w rękach ojca Małgosi. Jak wiadomo -
nieszczęścia chodzą parami - rodzic Małgosi został wezwany na niezwykle ważne konsultacje do
siedziby firmy Miracle i wróci dopiero na przyjęcie. Kolejny już raz cała nadzieja spoczęła w
Twoich rękach. Pomóż Małgosi podzielić tort.
Wejście
Na wejściu znajduje się liczba zestawów danych C, 1 ≤ C ≤ 65535. W kolejnych C wierszach
znajdują się liczby N, 1 ≤ N < 2^31, określające maksymalną ilość gości zaproszonych na urodziny.
Wyjście
Dla każdej liczby N, na wyjściu powinna się znaleźć liczba cięć K jakie wykonać powinna
Małgosia.
     */
    public sealed class Tort : ProblemBase
    {
        protected override string Input => "4\r\n1\r\n2\r\n4\r\n7";

        protected override string Output => "0\r\n1\r\n2\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                double c = (double)int.Parse(input[i]);
                output.Add(Math.Ceiling((Math.Sqrt(((c - 1) * 8.0) + 1) - 1.0) / 2.0).ToString());
            }
        }
    }
}
