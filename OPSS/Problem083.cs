namespace OPSS
{
    /* 3/5
     * Dwóch matematyków Pi i Sigma spotkało się wieczorem na partyjce pokera. Ponieważ nie mieli
pieniędzy, postanowili grać "na zapałki". Pi miał przy sobie tylko niebieskie zapałki, a Sigma tylko
zielone. Przed przystąpieniem do licytacji Pi wyłożył a swoich niebieskich zapałek, zaś Sigma
dołożył b swoich zielonych. Pi zaczyna licytację. Licytują na zmianę. W każdym kroku licytacji
zawodnik dorzuca tyle swoich zapałek, ile aktualnie jest zapałek przeciwnika w puli. Licytacja
trwała dość długo i gracze nie mogli się doliczyć aktualnej liczby zapałek w puli. Wiedzieli jednak,
ile razy każdy z nich dorzucał zapałki podczas licytacji.
Zadanie
Pomóż matematykom określić, ile zapałek jest w puli po n krokach licytacji. Ponieważ to
matematycy, zadowoli ich znajomość reszty z dzielenia tej liczby (liczby zapałek po n krokach)
przez ustaloną przez nich "magiczną" liczbę m.
Wejście
Pierwsza linia wejścia zawiera liczbę zestawów danych C (1 ≤ C ≤ 1000). W kolejnych wierszach
wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa się z jednego wiersza
zawierającego liczby całkowite a, b, n, m, oddzielone pojedynczą spacją (0 < a, b < 2^31, 0 ≤ n <
2^31, 2 ≤ m < 2^31). Liczby a, b określają kolejno liczbę niebieskich zapałek Pi oraz zielonych
Sigmy znajdujących się w puli przed rozpoczęciem licytacji. Liczba n określa liczbę kroków
licytacji. Liczba m to ustalona przez graczy "magiczna" liczba.
Wyjście
Dla każdego zestawu danych, wynikiem jest linia zawierająca jedną liczbę - resztę z dzielenia przez
m liczby zapałek po n krokach licytacji, przy założeniu, że przed przystąpieniem do licytacji w puli
znajduje się a niebieskich zapałek Pi oraz b zielonych zapałek Sigmy.
     */
    public sealed class Licytacja : ProblemBase
    {
        protected override string Input => "3\r\n1 1 4 1000\r\n5 6 7 1000\r\n4 101 23 999";

        protected override string Output => "13\r\n309\r\n767";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]), n = int.Parse(splits[2]), m = int.Parse(splits[3]);
                List<int> res = [];
                int fib1 = 1, fib2 = 1;
                int steps = 0;
                while(steps < n && (res.Count < 4 || fib1 != res[0] || fib2 != res[1]))
                {
                    int temp = fib1;
                    fib1 = fib2 % m;
                    fib2 = (fib2 + temp) % m;
                    res.Add(steps % 2 == 0 ? (fib1 * a + fib2 * b) % m : (fib2 * a + fib1 * b) % m);
                    steps++;
                }
                if (steps < n)
                    res.RemoveRange(res.Count - 3, 3);
                output.Add(res[n <= res.Count ? n - 1 : n % res.Count].ToString());
            }
        }
    }
}
