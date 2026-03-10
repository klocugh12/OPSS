namespace OPSS
{
    /* Difficulty: 5/5
     * Zarząd znanej na rynku firmy informatycznej MIRACLE, tworzącej specjalistyczne
oprogramowanie bazodanowe, chce pomóc swoim programistom w pokonywaniu drogi z ich
mieszkań do pracy. Po wstępnej analizie problemu Zarząd dokonał niecodziennego odkrycia:
okazało się, że domy programistów leżą na okręgu, którego środkiem jest siedziba firmy!
Firma chce wybudować sieć dróg w taki sposób, aby każdy programista, korzystając z nowych
połączeń, mógł dojechać do jej siedziby.
Przepisy jednoznacznie określają warunki jakie powinna spełniać droga: jedna droga może łączyć
dwa budynki i nie może przebiegać przez żaden inny budynek. Dodatkowo, jedna droga może albo
łączyć bezpośrednio mieszkanie programisty z siedzibą firmy (inaczej mówiąc: łączyć środek
okręgu - siedzibę firmy - z punktem leżącym na okręgu - mieszkaniem programisty) albo łączyć
dwa sąsiadujące ze sobą mieszkania programistów (czyli: należeć do okręgu). Inne sposoby
przeprowadzania dróg są sprzeczne z przepisami.
Zarząd firmy akceptuje wyłącznie takie projekty budowy, w których liczba niezbędnych dróg jest
najmniejsza.
Zadanie
Twoim zadaniem jest wyznaczenie liczby wszystkich możliwych projektów budowy dróg,
spełniających wymagania Zarządu firmy MIRACLE.
Rys. Przykładowy projekt budowy dróg (0 - siedziba firmy, 1-5 - mieszkania programistów).
Wejście
W pierwszej linijce wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 100, oznaczająca liczbę
zestawów danych. W kolejnych C wierszach wejścia znajdują się zestawy danych. Każdy zestaw
składa się z liczby naturalnej n, 3 ≤ n ≤ 1000, oznaczającej liczbę programistów zatrudnionych w
firmie.
Wyjście
Dla każdego zestawu danych, dla liczby n, na wyjściu powinna znaleźć się liczba naturalna, będąca
liczbą możliwych projektów budowy dróg spełniających wymagania Zarządu firmy MIRACLE.
     */
    public sealed class Drogi : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n5";

        protected override string Output => "16\r\n121";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<long> denoms = [1L, 3L];
            List<List<int>> denoms2 = [[1], [3]];
            //Tw. Kirchoffa, wyznacznik macierzy grafowej po eliminacji Gaussa,
            //skrócony do 2 ostatnich elementów
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                while (denoms.Count < a)
                {
                    denoms.Add(3L * denoms[^1] - denoms[^2]);
                    denoms2.Add(Sub(Mul(denoms2[^1], [3]), denoms2[^2]));
                }
                long last = denoms[a - 2] + 1L;
                var last2 = Add([1], denoms2[a - 2]);
                var resx2 = Div(Mul(Sub(denoms2[a - 1], last2), Add(last2, denoms2[a - 1])), denoms2[a - 2]);
                long resx = (denoms[a - 1] - last) * (denoms[a - 1] + last) / denoms[a - 2];
                output.Add(string.Join("", resx2).ToString());
            }
        }

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

        static List<int> Add(List<int> shorter, List<int> longer)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for (k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] += shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a >= 10;
                if (carry)
                    longer[longer.Count - k - 1] %= 10;
            }
            k = longer.Count - shorter.Count - 1;
            while (carry && k >= 0)
            {
                longer[k]++;
                carry = longer[k] >= 10;
                if (carry)
                    longer[k] %= 10;
            }
            if (k < 0 && carry)
                longer.Insert(0, 1);
            return longer;
        }

        static List<int> Sub(List<int> longer, List<int> shorter)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for (k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] -= shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a < 0;
                if (carry)
                    longer[longer.Count - k - 1] += 10;
            }
            k = longer.Count - shorter.Count - 1;
            while (carry && k >= 0)
            {
                longer[k]--;
                carry = longer[k] < 0;
                if (carry)
                    longer[k] += 10;
            }
            if (k < 0 && carry)
                longer.RemoveAt(0);
            return longer;
        }

        static List<int> Div(List<int> num, List<int> den)
        {
            List<int> ret = new(num.Count - den.Count + 1);
            List<(int, List<int>)> patterns = [(1, den)];
            for (int i = 2; i < 10; i++)
                patterns.Add((i, Add(den, patterns[^1].Item2)));
            patterns.Sort((a, b) => a.Item2[0] == b.Item2[0] ?
                a.Item2[1].CompareTo(b.Item2[1]) : a.Item2[0].CompareTo(b.Item2[0]));

            while (num.Count > 0)
            {
                int guess2 = patterns.Count - 1;
                int digit = 0;
                while (digit < patterns[guess2].Item2.Count)
                {
                    while (guess2 >= 0 && num[digit] < patterns[guess2].Item2[digit])
                        guess2--;
                    if (guess2 < 0)
                        break;
                    if (num[digit] == patterns[guess2].Item2[digit])
                        digit++;
                    else
                        break;
                }
                int i = 0;
                if (guess2 < 0)
                {
                    ret.Add(0);
                    i++;
                    guess2 = patterns.Count - 1;
                }
                ret.Add(patterns[guess2].Item1);
                if (patterns[guess2].Item2.Count <= num.Count)
                {
                    for (; i < patterns[guess2].Item2.Count; i++)
                    {
                        num[i] -= patterns[guess2].Item2[i];
                        if (num[i] < 0)
                        {
                            num[i] += 10;
                            int j = i - 1;
                            while (j > 0 && num[j] == 0)
                            {
                                num[j] = 9;
                                j--;
                            }
                            num[j]--;
                        }
                    }
                    int zeros = 0;
                    while (num.Any() && num[0] == 0)
                    {
                        zeros++;
                        if (zeros > patterns[guess2].Item2.Count)
                            ret.Add(0);
                        num.RemoveAt(0);
                    }
                }
                else
                {
                    ret.Add(0);
                    break;
                }
            }
            return ret;
        }
    }
}
