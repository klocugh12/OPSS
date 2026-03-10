using System.Globalization;

namespace OPSS
{
    /* Difficulty: 5/5
     * Zadanie
Należy wyznaczyć liczbę różnych konfiguracji, które powstają przy trącaniu się kieliszkami
parzystej liczby osób siedzących przy okrągłym stole. Stuknięcia kieliszkami w jednej konfiguracji
odbywają się w tej samej chwili a ręce biesiadników nie mogą się krzyżować.
Wejście
W pierwszym wierszu wejścia podana jest liczba całkowita D (0<D≤1000), oznaczająca liczbę
zestawów danych. W kolejnych D wierszach występują wartości Ni (0<Ni≤8000), oznaczające
liczby par biesiadników.
Wyjście
Na wyjściu, w jednym wierszu dla każdej danej Ni, należy wypisać dwie liczby całkowite: Ki -
liczbę konfiguracji stuknięć kieliszkami i Ci - ilość cyfr liczby Ki. Jeżeli liczba Ki jest większa lub
równa 10^9 to należy wypisać tylko 9 jej pierwszych cyfr.
     */
    public sealed class WeselneToasty : ProblemBase
    {
        protected override string Input => "2\r\n5\r\n100";

        protected override string Output => "42 2\r\n896519947 57";

        static List<int> Mul(List<int> target, List<int> factor)
        {
            List<int> res = new(target.Count + factor.Count);
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

        static List<int> primes = [2, 3];

        static void Factorize(Dictionary<int, int> dic, int n)
        {
            int limit = (int)Math.Sqrt(n);
            int i = 0;
            while (primes[i] <= limit && n > 1)
            {
                int c = primes[i];
                while (n % primes[i] == 0)
                {
                    if (!dic.TryAdd(c, 1))
                        dic[c]++;
                    n /= c;
                }
                i++;
            }
            if (n > 1)
            {
                if(n > primes[^1])
                    primes.Add(n);
                if (!dic.TryAdd(n, 1))
                    dic[n]++;
            }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<Dictionary<int, int>> catalan = [new() { { 1, 1 } }, new() { { 1, 1 } }, new() { { 2, 1 } }];
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                while (catalan.Count <= a)
                {
                    Dictionary<int, int> factors = new(catalan[^1]);
                    Factorize(factors, (catalan.Count << 1) - 1);
                    if (!factors.TryAdd(2, 1))
                        factors[2]++;
                    Dictionary<int, int> dens = new();
                    Factorize(dens, catalan.Count + 1);
                    foreach (var c in dens.Keys)
                    {
                        factors[c] -= dens[c];
                        if (factors[c] == 0)
                            factors.Remove(c);
                    }
                    catalan.Add(factors);
                }                
                List<int> res = [1];
                foreach (var c in catalan[a].Keys)
                {
                    int k = c;
                    for (int j = 1; j < catalan[a][c]; j++)
                        k *= c;
                    List<int> fac = [];
                    while (k > 0)
                    {
                        fac.Insert(0, k % 10);
                        k /= 10;
                    }
                    res = Mul(res, fac);
                }
                output.Add($"{string.Join("", res.Take(Math.Min(res.Count, 9)))} {res.Count}");
            }
        }
    }
}
