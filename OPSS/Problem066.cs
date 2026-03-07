namespace OPSS
{
    /* 4/5
     * Grupa programistów OPSS, postanowiła wzbogacić swój system o dobrą wyszukiwarkę tekstową.
W trakcie pracy nad głównym algorytmem programiści zdali sobie sprawę, że dobra wyszukiwarka
powinna nie tylko działać szybko, lecz również pomagać użytkownikom, gdy pomylą się przy
wpisywaniu jakiegoś słowa lub gdy takiego słowa nie ma w słowniku.
Programiści wpadli na pomysł, żeby w momencie wpisania hasła, którego nie znaleziono w
słowniku, zwrócić użytkownikowi listę słów podobnych do wpisanej frazy. Lista powinna być
posortowana od najbardziej do najmniej podobnych.
Podobieństwo hasła H1 do H2 wyznaczamy w ten sposób, że sumujemy minimalną liczbę liter,
które należy wstawić, usunąć lub wymienić w słowie H1 aby uzyskać H2.
Na przykład, aby ze słowa 'ALA' otrzymać 'MAREK' musimy wykonać łącznie 4 operacje:
1.Dodajemy na początek literkę 'M', otrzymujemy 'MALA'
2.Wymieniamy literkę 'L' na 'R', otrzymujemy 'MARA'
3.Wymieniamy drugą literkę 'A' na 'E', otrzymujemy 'MARE'
4.Dodajemy na koniec literkę 'K', otrzymujemy 'MAREK'
Dla hasła 'OLA' wartość podobieństwa do 'MAREK' równa jest 5, gdyż usuwamy literę 'O',
wymieniamy literę 'L' na 'M' i dodajemy na koniec 3 litery 'R', 'E' i 'K'.
Jak widać słowo 'ALA' jest bardziej podobne do hasła 'MAREK' niż 'OLA'. Dlatego, gdy
użytkownik wpisze hasło 'MAREK', którego nie ma w słowniku, system powinien podpowiedzieć
mu wypisując posortowaną rosnąco listę - w tym przypadku byłoby to: 'ALA' 'OLA'
Zadanie
Napisz program, który dla danego słownika S i hasła T, posortuje listę słów S niemalejąco
względem podobieństwa do hasła T (od najbardziej do najmniej podobnych).
Wejście
W pierwszym wierszu znajduje się liczba zestawów danych D, 1 ≤ D ≤ 30. W kolejnych wierszach
znajdują się zestawy danych. W pierwszym wierszu zestawu podane jest hasło wzorcowe T. W
drugim znajduje się liczba N, 1 ≤N ≤20 haseł w liście słownikowej, którą należy posortować. W
kolejnych N wierszach znajdują się hasła. Wszystkie słowa składają się z dużych liter alfabetu
angielskiego. Długość hasła w słowniku jest nie mniejsza niż 1 znak i nie większa niż 200 znaków.
Wyjście
Dla każdego zestawu danych należy wypisać na wyjściu jedną linię, składającą się z N haseł
oddzielonych pojedynczą spacją. Hasła powinny być posortowane względem podobieństwa do
wzorca T ( od najbardziej do najmniej pasującego). W przypadku, gdy dwa słowa są tak samo
podobne do wzorca T, należy je wypisać w takiej kolejności, w jakiej pojawiły się na wejściu
     */
    public sealed class Gugle : ProblemBase
    {
        protected override string Input => "2\r\nMAREK\r\n4\r\nOLA\r\nHELA\r\nALA\r\nKERAM\r\nDAREK\r\n5\r\nFOO\r\nABCDEFGH\r\nDAREEC\r\nFOOBAR\r\nKERAD";

        protected override string Output => "ALA KERAM OLA HELA\r\nDAREEC KERAD FOO FOOBAR ABCDEFGH";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                string pattern = input[j];
                j++;
                int c = int.Parse(input[j]);
                j++;
                Dictionary<string, int> candidates = [];
                for(int k = 0; k < c; k++)
                {
                    candidates.Add(input[j], Math.Max(pattern.Length, input[j].Length));
                    j++;
                }
                foreach(var s in candidates.Keys)
                {
                    List<(int, int, int)> anchors = [];
                    for(int k = 0; k < pattern.Length; k++)
                    {
                        for(int l = 0; l < s.Length; l++)
                        {
                            int length = -1, k2 = k, l2 = l;
                            while (k2 < pattern.Length && l2 < s.Length && pattern[k2] == s[l2])
                            {
                                length++;
                                k2++;
                                l2++;
                            }
                            if (length >= 0)
                            {
                                anchors.Add((k, l, length));
                                l += length;
                            }
                        }
                    }
                    foreach(var anchor in anchors)
                    {
                        int similarity = Math.Max(anchor.Item2, anchor.Item1);
                        similarity += Math.Max(s.Length - (anchor.Item2 + anchor.Item3 + 1), pattern.Length - (anchor.Item1 + anchor.Item3 + 1));
                        candidates[s] = Math.Min(similarity, candidates[s]);
                    }
                }
                output.Add(string.Join(" ", candidates.OrderBy(x => x.Value).Select(s => s.Key)));
            }
        }
    }
}
