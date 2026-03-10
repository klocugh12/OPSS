namespace OPSS
{
    /* Difficulty: 3/5
     * 
Zdefiniujmy AB-drzewo jako pełne drzewo, którego węzłami będą słowa nad alfabetem {a, b},
korzeniem będzie słowo puste "0", a dla dowolnego słowa w, jego synami będą słowa: {xw: x
należy do {a, b}}. Synami słowa w są 2 słowa powstałe w wyniku doklejenia jednej z liter {a, b} na
początku słowa w. Wszystkie słowa w drzewie są różne. Wszelkie wątpliwości powinien wyjaśnić
poniższy schemat:
Pełnym AB-drzewem o wysokości h nazywamy AB-drzewo w którym wszystkie liście są słowami
o długości h. Z pełnego AB-drzewa o zadanej wysokości (równej długości słowa znajdującego się
w liściu), będziemy usuwać pewne słowa, wraz z potomkami (poddrzewami). Twoim zadaniem
będzie wyznaczenie liczby słów które pozostaną w drzewie po usunięciu zadanych słów wraz z
potomkami.
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 10, oznaczająca liczbę
zestawów danych. W kolejnych wierszach znajdują się zestawy danych. W pierwszym wierszu
każdego zestawu danych znajduje się liczba naturalna H, 1 ≤ H ≤ 30 - jest to wysokość AB-drzewa.
W drugim wierszu każdego zestawu danych znajduje się liczba naturalna N, 0 ≤ N ≤ 50000,
oznaczająca ilość słów które chcemy usunąć z drzewa wraz z ich poddrzewami. W kolejnych N
wierszach znajdują się słowa które będziemy usuwać. Wszystkie słowa znajdują się w pełnym AB-
drzewie o wysokości H, i nie ma wśród nich słowa pustego (korzenia).
Wyjście
W kolejnych liniach wyjścia powinny znaleźć się liczby słów które pozostały w drzewach z
kolejnych zestawów danych.
     */
    public sealed class ABDrzewo : ProblemBase
    {
        protected override string Input => "1\r\n2\r\n3\r\nb\r\nab\r\naa";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[j]);
                int c = (1 << (n + 1)) - 1;
                j++;
                int n2 = int.Parse(input[j]);
                j++;
                List<string> strings = [];
                for(int k = 0; k < n2; k++)
                {
                    if (!strings.Any(s => input[j].EndsWith(s)))
                        strings.Add(input[j]);
                    j++;
                }
                strings.Sort((a, b) => a.Length.CompareTo(b.Length));
                for(int k = 0; k < strings.Count; k++)
                {
                    for (int l = k + 1; l < strings.Count; l++)
                        if (strings[l].EndsWith(strings[k]))
                        {
                            strings.RemoveAt(l);
                            l--;
                        }
                }
                foreach (var s in strings)
                {
                    c -= (1 << (n - s.Length + 1)) - 1;
                }
                output.Add(c.ToString());
            }
        }
    }
}
