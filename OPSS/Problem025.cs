namespace OPSS
{
    /* 4/5
     * Michał zawsze bardzo lubił matematykę, więc - co zrozumiałe - był pojętnym uczniem.
Rozwiązywał problemy matematyczne znacznie szybciej niż koledzy w klasie.
Pewnego dnia, na lekcji, gdy Michał zrobił już zadania z ćwiczeń, trochę ze znużenia, trochę z
ciekawości zapisał w zeszycie ciąg: 1121231234123451234561234567... - każda cyfra była w
odzielnej kratce.
Zaintrygowany profesor szybko odgadł, w jaki sposób jego uczeń zbudował ciąg i chcąc go
"zagiąć", zapytał, czy potrafi powiedzieć jaka cyfra znajduje się na zadanej pozycji. Michał
odpowiedział dyplomatycznie, że problem wymaga głębszych przemyśleń i obiecał, że na następnej
lekcji przedstawi rozwiązanie.
Po powrocie do domu zabrał się ostro do pracy - najpierw musi zobaczyć jak wyglądają wyniki dla
"małych" liczb z przedziału 1..2^31-1. Michał nie posiada komputera. Pomóż mu znaleźć rozwiązanie
dla "małych" przypadków.
Wejście
Pierwsza liczba C, 1 ≤ C ≤ 100, określa liczbę zestawów danych. W kolejnych C liniach znajdują
się liczby N, 1 ≤ N < 231, określające pozycję cyfry w ciągu.
Wyjście
Na wyjściu powinno znaleźć się C cyfr, każda w osobnej linii, równych odpowiednio cyfrze na N-
tej pozycji w ciągu.
     */
    public sealed class ZagadkowyCiag : ProblemBase
    {
        protected override string Input => "3\r\n3\r\n4\r\n5";

        protected override string Output => "2\r\n1\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> ranges = [0];
            int a1 = 1, n = 9;
            for (int i = 1; i <= 4; i++)
            {
                ranges.Add(ranges[ranges.Count - 1] + (n * ((a1 << 1) + (n - 1) * i) >> 1));
                a1 = (int)Math.Pow(10, i) + 1;
                n *= 10;
            }
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int x = int.Parse(input[i]);
                int j = 1;
                while (j < ranges.Count && ranges[j] < x)
                    j++;
                a1 = (int)(Math.Pow(10, j - 1));
                x -= ranges[j - 1];
                double a = 0.5 * j;
                double b = (a + a1 - 1);
                int sqrt = (int)((Math.Sqrt(b * b + a * (x << 2)) - b) / (2.0 * a));
                x -= sqrt * (a1 - 1) + j * ((sqrt * (sqrt + 1)) >> 1);
                a1 = 9;
                j = 1;
                while(x > a1)
                {
                    x -= a1;
                    j++;
                    a1 *= 10;
                }
                a1 = (int)Math.Ceiling((double)x / j);
                output.Add(x == 0 ? (sqrt % 10).ToString() : a1.ToString()[(x + j - 1) % j].ToString());
            }
        }
    }
}
