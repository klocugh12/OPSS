namespace OPSS
{
    /* 3/5
     * 
Pantofelek sprawiedliwy (Paramecium iustus) to bardzo specyficzny gatunek pantofelka. Prowadzi
stadny tryb życia i odżywia się bakteriami, które wchłania tylko wtedy, gdy może się nimi podzielić
sprawiedliwie z innymi osobnikami w stadzie. Pantofelki sprawiedliwe jeśli zaczynają jeść, to
zawsze wchłaniają wszystkie okoliczne bakterie, a następnie przemieszczają się w inne miejsce, w
którym jest pokarm. O sprawiedliwym podziale bakterii mówimy wtedy, gdy wszystkie okoliczne
bakterie można podzielić po równo pomiędzy wszystkie pantofelki. Wówczas każdy pantofelek
wchłonie tę samą liczbę okolicznych bakterii i w okolicy nie będzie już żadnej. Zarówno pantofelki,
jak i bakterie, rozmnażają się szybko i w bardzo specyficzny sposób. Co pewien czas, nagle u
wszystkich pantofelków dochodzi do podziału komórkowego, w wyniku którego każdy osobnik
dzieli się na X osobników potomnych (liczebność populacji wzrasta X razy). Okoliczne bakterie
rozmnażają się w taki sam sposób.
Na podstawie historii rozmnożeń jednego pantofelka i jednej bakterii sprawdź, czy aktualny stan ich
liczebności pozwala na sprawiedliwy podział. Jeśli taki podział istnieje, oblicz ile bakterii przypada
na jednego pantofelka.
Wejście
Pierwsza linia zawiera liczbę zestawów danych D (1 ≤ D ≤ 10). W następnych liniach znajdują się
kolejno po sobie opisy D zestawów danych. Pierwsza linia zestawu danych zawiera liczbę
rozmnożeń N (1 ≤ N ≤ 100000). Każda z kolejnych N linii zestawu zawiera parę symbol-liczba,
będącą opisem jednego rozmnożenia. Symbolem jest jedna z małych liter 'b' lub 'p' oznaczająca
odpowiednio rozmnożenie się bakterii lub rozmnożenie się pantofelka. Liczba naturalna X w parze
symbol-liczba oznacza, że liczebność osobników określonych przez symbol wzrasta X razy (1 ≤ X ≤
100000). Symbol w parze jest oddzielony od liczby pojedynczą spacją.
Wyjście
Dla każdego zestawu danych, w osobnych liniach, wypisz w przypadku sprawiedliwego podziału
liczbę bakterii przypadających na jednego pantofelka (liczba ta będzie zawsze mniejsza od 2^31)
lub -1 jeśli bakterii nie da się podzielić sprawiedliwie.
     */
    public sealed class Pantofelek : ProblemBase
    {
        protected override string Input => "3\r\n4\r\nb 12\r\nb 5\r\np 20\r\nb 7\r\n5\r\nb 8\r\np 13\r\nb 5\r\nb 39\r\np 2\r\n5\r\nb 6\r\nb 48\r\np 9\r\nb 11\r\np 3";

        protected override string Output => "21\r\n60\r\n-1";

        int gcd(int m, int n)
        {
            int temp;
            if(m > n)
            {
                temp = m;
                m = n;
                n = temp;
            }
            while(m > 0)
            {
                temp = m;
                m = n % m;
                n = temp;
            }
            return n;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int c = int.Parse(input[j]);
                j++;
                int b = 1, p = 1;
                for (int k = 0; k < c; k++)
                {
                    var s = input[j].Split(' ');
                    int x = int.Parse(s[1]);
                    if (s[0] == "p")
                        p *= x;
                    else
                        b *= x;
                    x = gcd(b, p);
                    b /= x;
                    p /= x;
                    j++;
                }
                output.Add(b % p == 0 ? (b / p).ToString() : "-1");
            }
        }
    }
}
