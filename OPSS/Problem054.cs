namespace OPSS
{
    /* Difficulty: 3/5
     * 
Nieujemną liczbę całkowitą H nazwiemy HEX-palindromiczną jeśli istnieje liczba naturalna k > 1
taka że, odwrócony zapis szesnastkowy liczby H jest taki sam jak zapis szesnastkowy liczby H*k
(rozpatrujemy wyłącznie zapisy szesnastkowe bez wiodących zer na początku).
Np. Liczba 17340 (szesnastkowo: 43BC) jest liczbą HEX-palindromiczną (17340*3=52020,
szesnastkowo: 43BC*3=CB34).
Zadanie
Twoim zadaniem będzie wyznaczenie największej liczby HEX-palindromicznej mniejszej od
zadanej liczby N.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, określająca ilość zestawów danych, 1 ≤ C ≤
1000. W kolejnych liniach wejścia znajdują się zestawy danych. Każdy z C zestawów danych
składa się z jednego wiersza zawierającego zapis szesnastkowy liczby N, N ≥ 0. Zapis szesnastkowy
liczby N zawiera co najwyżej 10 znaków (nie występują w nim zera wiodące). Dozwolone znaki
systemu szesnastkowego to cyfry 0-9, duże litery A,B,C,D,E,F.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać zapis szesnastkowy
największej liczby HEX-palindromicznej mniejszej od zadanej liczby N. W przypadku, gdy taka
liczba nie istnieje, należy wypisać liczbę 0.
     */
    public sealed class LiczbyHEXPalindromiczne : ProblemBase
    {
        protected override string Input => "3\r\n2000\r\nF533\r\n409F0";

        protected override string Output => "10EF\r\n43BC\r\n21FDE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var s = input[i];
                if(s.Length < 4 || (s.Length == 4 && s.CompareTo("10EF") < 0))
                {
                    output.Add("0");
                    continue;
                }
                string first = $"10{new string('0', s.Length - 4)}EF";
                string second = $"21{new string('0', s.Length - 4)}DE";
                string third = $"43{new string('0', s.Length - 4)}BC";
                if (s.CompareTo(first) < 0)
                    output.Add($"43{new string('F', s.Length - 5)}BC");
                else if(s.CompareTo(second) < 0)
                    output.Add($"10{Middle(s, first)}EF");
                else if (s.CompareTo(third) < 0)
                    output.Add($"21{Middle(s, second)}DE");
                else
                    output.Add($"43{Middle(s, third)}BC");
            }
        }

        static string Middle(string toEdit, string pattern)
        {
            if (toEdit[0..1].CompareTo(pattern[0..1]) > 0)
                return new('F', toEdit.Length - 4);
            var result = toEdit[2 .. (toEdit.Length - 3)];
            if (toEdit.Substring(toEdit.Length - 2, 2).CompareTo(pattern.Substring(pattern.Length - 2, 2)) > 0)
                return result;
            var num = result.Select(c => (c >= '0' && c <= '9' ? c - '0' : (c - 'A' + 10))).ToArray();
            int k = num.Length - 1;
            num[k]--;
            while (num[k] < 0)
            {
                num[k] += 16;
                k--;
                num[k]--;
            }
            return string.Join("", num.Select(c => c < 10 ? (char)(c + '0') : (char)(c - 10 + 'A')));
        }
    }
}
