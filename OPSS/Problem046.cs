namespace OPSS
{
    /* 3/5
     * 
Zadanie
Napisz program, który dla zadanej kwoty reszty , którą należy wypłacić, oraz dla znanego zestawu
nominałów monet dostępnych w automacie, obliczy optymalną (minimalną) liczbę monet, jaką
powinien wypłacić automat. Bajtocki automat posiada nieskończenie wiele monet o zadanym
nominale.
Wejście
W pierwszym wierszu wejścia znajduje się liczba całkowita C, 0 < C ≤ 100, określająca ilość
zestawów danych. Każdy zestaw składa się z dwóch wierszy. Pierwszy z nich zawiera dwie liczby
całkowite N i K oddzielone pojedynczą spacją, gdzie N to kwota reszty jaką trzeba wydać, 0 ≤ N ≤
1000, zaś K to liczba dostępnych nominałów monet, 0 < K ≤ 100. W drugim wierszu zestawu
znajduje się K dostępnych nominałów monet, oddzielonych pojedynczymi spacjami. Każdy
nominał określony jest przez dodatnią liczbę naturalną nie większą niż 1000. Zakładamy, że żaden
nominał nie występuje więcej niż jeden raz w drugiej linii zestawu.
Wyjście
Dla każdego zestawu danych program powinien wypisać na wyjście minimalną możliwą liczbę
monet jaką powinien wydać automat, aby wydana reszta równa była zadanej kwocie . Jeśli automat
nie może wydać reszty (bo np. nie posiada monet, którymi mógłby wydać żądaną sumę) należy
wypisać 0.
     */
    public sealed class Automat : ProblemBase
    {
        protected override string Input => "3\r\n11 3\r\n5 3 1\r\n1 1\r\n2\r\n1000 10\r\n1 3 5 7 10 20 50 100 200 300";

        protected override string Output => "3\r\n0\r\n4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int c = int.Parse(splits[0]);
                j++;
                var coins = input[j].Split(' ').Select(s => int.Parse(s)).Where(s => s <= c).OrderBy(s => s).ToArray();
                j++;
                if(coins.Length == 0)
                {
                    output.Add("0");
                    continue;
                }
                int[] counts = new int[c + 1];
                foreach(int coin in coins)
                {
                    counts[coin] = 1;
                }
                for(int k = coins[0] + 1; k <= c; k++)
                {
                    foreach (int coin in coins.Where(c2 => c2 < k))
                        counts[k] = counts[k] == 0 ? counts[k - coin] + 1 : Math.Min(counts[k], counts[k - coin] + 1);
                }
                output.Add(counts[c].ToString());
            }
        }
    }
}
