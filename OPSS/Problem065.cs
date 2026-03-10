namespace OPSS
{
    /* Difficulty: 4/5
     * Szkrable jest bardzo popularną i niezwykle skomplikowaną grą słowną polegającą na układaniu
słów przez graczy. Jedna z setek zasad tej gry mówi, że słowo jest dopuszczalne do gry jeżeli każdy
jego spójny podciąg o długości trzech liter jest wyrazem ze słownika ustalonego przez graczy na
początku gry. Wyrazy w słowniku (nazywane 'trójkami') nie powtarzają się. Każda 'trójka' składa
się z trzech różnych dużych liter alfabetu angielskiego i ma przyporządkowaną dodatnią liczbę
punktów. Za utworzone słowo gracz otrzymuje liczbę punktów równą sumie punktów za każdą
'trójkę' występującą w tym słowie. Jeśli 'trójka' występuje w słowie więcej niż jeden raz, wówczas
gracz otrzymuje punkty osobno za każde wystąpienie 'trójki' w słowie. Jedna z zasad szczególnych
mówi, że słowo jednoliterowe lub dwuliterowe jest słowem dopuszczalnym jeżeli zawiera się w
którymkolwiek z wyrazów ze słownika. Za utworzone słowa jednoliterowe i dwuliterowe gracz nie
otrzymuje żadnych punktów.
Zadanie
Pan Henryk od niedawna gra w Szkrable. Pragnie szybko stać się dobrym graczem, dlatego
potrzebuje Twojej pomocy. Chciałby wiedzieć, ile może maksymalnie stracić punktów za ułożone
przez siebie dopuszczalne do gry słowo, jeżeli będzie do niego dodawał i/lub usuwał litery tak, żeby
zmodyfikowane słowo wciąż było dopuszczalne do gry, zaczynało się na tę samą literę na którą
zaczyna się oryginalne słowo sprzed modyfikacji i kończyło się na tę samą literę co słowo sprzed
modyfikacji. Napisz program, który mu to ułatwi.
Wejście
W pierwszym wierszu wejścia znajduje się liczba n (1 ≤ n ≤ 1000), określająca liczbę wyrazów w
słowniku. W kolejnych n wierszach opisane są 'trójki' ze słownika, jedna 'trójka' w każdym wierszu.
Każdy wiersz z opisem 'trójki' składa się z wyrazu T oraz liczby całkowitej K, oddzielonych
pojedynczą spacją. Wyraz T jest trzyliterowym wyrazem ze słownika, któremu przyporządkowana
jest liczba punktów K (1 ≤ K ≤ 1000). Następny wiersz wejścia zawiera liczbę q (1 ≤ q ≤ 1000). W
kolejnych q wierszach znajdują się niepuste słowa utworzone przez pana Henryka, po jednym
słowie w każdym wierszu. Długość każdego ze słów nie przekracza 1000 liter.
Wyjście
Dla każdego słowa pana Henryka należy w jednym wierszu wyjścia wypisać dwie liczby
oddzielone spacją:
● liczbę punktów jaką może on otrzymać za ułożone słowo
● maksymalną liczbę punktów o jaką może pomniejszyć swój wynik modyfikując ułożone
słowo tak, żeby dalej było dopuszczalne do gry i żeby zaczynało się i kończyło na te same
litery na które zaczyna się i kończy słowo niezmodyfikowane.
     */
    public sealed class Szkrable : ProblemBase
    {
        protected override string Input => "9\r\nDAB 10\r\nABC 20\r\nBCD 25\r\nCDZ 100\r\nADZ 1000\r\nRCB 40\r\nRCD 1000\r\nCBA 30\r\nCDA 50\r\n3\r\nADZ\r\nRCDABC\r\nRCDZ";

        protected override string Output => "1000 855\r\n1080 1080\r\n1100 0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            Dictionary<string, int> dict = new();
            for (int i = 1; i <= N; i++)
            {
                var s = input[i].Split(' ');
                dict.Add(s[0], int.Parse(s[1]));
            }
            int K = int.Parse(input[N + 1]);
            for (int i = 0; i < K; i++)
            {
                var s = input[N + i + 2];
                int sum1 = 0;
                for (int k = 0; k < s.Length - 2; k++)
                    sum1 += dict[s.Substring(k, 3)];
                var join = string.Join("", s[0], s[s.Length - 1]);
                if (dict.Keys.Any(k => ((s[0] == s[s.Length - 1] && k.Contains(s[0]))) || k.Contains(join)))
                {
                    output.Add($"{sum1} {sum1}");
                    continue;
                }
                var candidates = dict.Keys.Where(k => k.StartsWith(s[0]) && !s.StartsWith(k)).Select(k => (k, dict[k])).ToList();
                for (int j = 0; j < candidates.Count; j++)
                {
                    while (j < candidates.Count && !candidates[j].k.EndsWith(s[s.Length - 1]))
                    {
                        var appends = dict.Keys.Where(k =>
                            k.StartsWith(candidates[j].k.Substring(candidates[j].k.Length - 2, 2)) && !candidates[j].k.StartsWith(k));
                        foreach (var key in appends)
                        {
                            if (candidates[j].Item2 + dict[key] < sum1)
                                candidates.Insert(j + 1, (candidates[j].k + key[2], candidates[j].Item2 + dict[key]));
                            {
                                if (key[2] == s[s.Length - 1])
                                {
                                    candidates.RemoveAll(c2 => c2.Item2 >= candidates[j + 1].Item2 && c2.k != candidates[j + 1].k);
                                }
                            }
                        }
                        candidates.RemoveAt(j);
                    }
                }
                if (!candidates.Any())
                {
                    output.Add($"{sum1} 0");
                    continue;
                }
                output.Add($"{sum1} {Math.Max(sum1 - candidates.Min(c2 => c2.Item2), 0)}");
            }
        }
    }
}
