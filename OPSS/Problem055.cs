namespace OPSS
{
    /* Difficulty: 4/5
     * Jedna z wiodących na rynku sieci telefonii komórkowej wykonała szereg analiz komunikacji
pomiędzy nadajnikami rozmieszczonymi na terenie całego kraju. W wyniku badań ustalono, że
połączenie zostanie tak skonfigurowane, iż każdy nadajnik będzie komunikował się z każdym
innym albo bezpośrednio, albo za pomocą tylko jednego nadajnika pośredniczącego. Aby zapewnić
maksymalny komfort usług postanowiono, że w przypadku wyłączenia lub awarii pewnej liczby
nadajników, komunikacja pomiędzy pozostałymi nadajnikami (o ile jest w ogóle możliwa) będzie
wymagała także co najwyżej jednego nadajnika pośredniczącego. W celu ograniczenia liczby
możliwych konfiguracji do sprawdzenia wykluczono połączenia typu "każdy z każdym".
Firma telekomunikacyjna rozpatrzyła efektywność i koszty każdej możliwej konfiguracji
spełniającej powyższe założenia. Jednak ostatnio skradziono z firmy pewne dokumenty i nie
wiadomo, czy były wśród nich wyniki badań sieci. Twoim zadaniem będzie wyznaczenie liczby
możliwych konfiguracji sieci, aby można było określić, czy zaginęły wyniki badań (firma
podejrzewa, że może być w to zamieszana konkurencja). Musisz wiedzieć, że w trakcie analiz
nadajniki sieci komórkowej traktowano jako nierozróżnialne.
Wejście
W pierwszym wierszu wejścia znajduje się liczba zestawów danych C, 1 ≤C ≤ 50. Każdy z C
zestawów danych składa się z jednego wiersza zawierającego liczbę naturalną N, 2 ≤ N ≤ 50,
oznaczającą całkowitą liczbę nadajników w sieci.
Wyjście
Dla każdego zestawu danych należy wyznaczyć liczbę możliwych konfiguracji komunikacji
pomiędzy nadajnikami spełniających warunki zadania, przy założeniu, że nie rozróżniamy
nadajników.
     */
    public sealed class Nadajniki : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n3\r\n4";

        protected override string Output => "0\r\n1\r\n4";

        static List<int> Mul(List<int> target, List<int> factor)
        {
            List<int> res = [];
            for (int k = 0; k < target.Count + factor.Count - 1; k++)
                res.Add(0);
            for (int k = 0; k < target.Count; k++)
            {
                for (int l = 0; l < factor.Count; l++)
                {
                    res[k + l] += target[k] * factor[l];
                }
            }
            int l2 = res.Count - 1;
            while (l2 >= 0)
            {
                if (res[l2] < 10)
                {
                    l2--;
                    continue;
                }
                if (l2 > 0)
                    res[l2 - 1] += res[l2] / 10;
                else
                {
                    res.Insert(0, res[l2] / 10);
                    l2++;
                }
                res[l2] %= 10;
                l2--;
            }
            return res;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                if(a < 4)
                {
                    output.Add((a - 2).ToString());
                    continue;
                }
                int b = ((a - 2) * (a - 3) >> 1) + 1;
                List<int> res = [1];
                List<int> p2 = [2];
                while (b > 0)
                {
                    if (b % 2 == 0)
                    {
                        p2 = Mul(p2, p2);
                        b >>= 1;
                    }
                    else
                    {
                        res = Mul(res, p2);
                        b--;
                    }
                }
                output.Add(string.Join("", res));
            }
        }
    }
}
