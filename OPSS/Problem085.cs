namespace OPSS
{
    /* 4/5
     * Mały Jasio dostał ostatnio od rodziców prezent w postaci 66 klocków. Każdy klocek zbudowany
jest z dwóch części, z których każda zawiera pewną liczbę oczek, podobnie jak jest to w przypadku
kostek domina. Liczba oczek znajdujących się na każdej połówce klocka wynosi co najmniej jeden
i co najwyżej jedenaście:
1 | 1
1 | 2
..
1 | 11
2 | 2
2 | 3
..
2 | 11
..
11 | 11
Bawiąc się klockami, Jasio wymyślił pewną grę polegającą na układaniu klocków w linii prostej
według następujących zasad:
● Wybrać pewną początkową liczbę oczek P
● Ułożyć obok siebie w linii prostej (klocki można obracać o 180 stopni), od lewej strony,
klocki tak, aby:
● dla każdego klocka, jego prawy sąsiad (klocek) miał na lewej połowie tyle oczek ile
ma ten klocek na swojej prawej połowie
● każda liczba dostępnych oczek, wystąpiła w tym ciągu klocków dokładnie dwa razy
● liczba oczek na lewej połowie pierwszego klocka i prawej połowie ostatniego klocka
były takie same i wynosiły P
Każdemu klockowi Jasio przyporządkował pewną liczbę punktów i chciałby wiedzieć jaka jest
minimalna oraz maksymalna możliwa liczba punktów do zdobycia w grze. Do gry, Jasio może
używać 6, 10, 15, 21, 28, 36, 45, 55 lub 66 klocków, wówczas wykorzystuje klocki z maksymalnie
odpowiednio 3, 4, 5, 6, 7, 8, 9, 10, 11 oczkami na każdej z połówek.
Przykładowo, zestaw 15 klocków przedstawia się następująco:
1 | 1
1 | 2
..
1 | 5
2 | 2
2 | 3
..
2 | 5
..
5 | 5
Zadanie
Znając początkową liczbę oczek P oraz punktację dla kolejnych klocków, wyznacz minimalną i
maksymalną liczbę punktów jaką Jasio może uzyskać w swojej grze.
Wejście
W pierwszym wierszu wejścia znajduje się liczba zestawów danych C, 1 ≤ C ≤ 3. W kolejnych
wierszach wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa się z trzech
wierszy. W pierwszej linii zestawu znajduje się liczba N (możliwe wartości: 6, 10, 15, 21, 28, 36,
45, 55, 66), określająca liczbę klocków, które Jasio wykorzystuje do gry. Drugą linię zestawu
stanowi N oddzielonych pojedynczą spacją liczb całkowitych z przedziału <-1000; 1000>,
określających punktację klocków (klocki w kolejności odpowiednio: 1 | 1, 1 | 2, .. 2 | 2, 2 | 3, ..). W
ostatniej linii zestawu znajduje się wybrana przez Jasia początkowa liczba oczek P, 1 ≤ P ≤ 11.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać minimalną i
maksymalną liczbę punktów jaką może uzyskać Jasio. Liczby powinny być oddzielone pojedynczą
spacją. W przypadku, gdy nie istnieje ciąg klocków spełniający zasad gry należy przyjąć, że
maksimum jest równe minimum i wynosi 0.
     */
    public sealed class Klocki : ProblemBase
    {
        protected override string Input => "1\r\n10\r\n1 2 3 4 5 6 7 8 9 10\r\n3";

        protected override string Output => "20 21";

        static (int, int) ComposeKey(int a, int b) => a < b ? (a, b) : (b, a);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[j]);
                n = (int)Math.Sqrt(n << 1);
                Dictionary<(int, int), int> scores = [];
                j++;
                List<int> splits = input[j].Split(' ').Select(s => int.Parse(s)).ToList();
                j++;
                for (int k = 0; k < n; k++)
                    for (int l = k; l < n; l++)
                    {
                        if (k != l)
                        {
                            scores.Add((k + 1, l + 1), splits[0]);
                        }
                        splits.RemoveAt(0);
                    }
                int start = int.Parse(input[j]);
                List<int> toUse = Enumerable.Range(1, n).ToList();
                List<int> max = [start];
                List<int> min = [start];
                toUse.Remove(start);
                for(int k = 0; k < 2; k++)
                {
                    max.Add(toUse[0]);
                    min.Add(toUse[0]);
                    toUse.RemoveAt(0);
                }
                max.Add(start);
                min.Add(start);
                while(toUse.Any())
                {
                    int next = toUse[0];
                    toUse.RemoveAt(0);
                    int min1 = 0, max1 = 0, deltaMin = int.MaxValue, deltaMax = int.MinValue;
                    for(int k = 1; k < min.Count; k++)
                    {
                        var key1 = ComposeKey(next, min[k]);
                        var key2 = ComposeKey(next, min[k - 1]);
                        var key3 = ComposeKey(min[k], min[k - 1]);
                        int delta = scores[key1] + scores[key2] - scores[key3];
                        if(delta < deltaMin)
                        {
                            min1 = k;
                            deltaMin = delta;
                        }
                        key1 = ComposeKey(next, max[k]);
                        key2 = ComposeKey(next, max[k - 1]);
                        key3 = ComposeKey(max[k], max[k - 1]);
                        delta = scores[key1] + scores[key2] - scores[key3];
                        if (delta > deltaMax)
                        {
                            max1 = k;
                            deltaMax = delta;
                        }
                    }
                    min.Insert(min1, next);
                    max.Insert(max1, next);
                }
                j++;
                int sum1 = 0, sum2 = 0;
                for(int k = 1; k < min.Count; k++)
                {
                    sum1 += scores[ComposeKey(min[k], min[k - 1])];
                    sum2 += scores[ComposeKey(max[k], max[k - 1])];
                }
                output.Add($"{sum1} {sum2}");
            }
        }
    }
}
