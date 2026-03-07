namespace OPSS
{
    /* 3/5
     * 
Barney - geolog amator - skorzystał z okazji, aby fotografować pustkowia Szkocji "z lotu ptaka".
Okazja ta związana była z ćwiczebnym lotem balonem kilku jego przyjaciół - matematyków. W
czasie lotu Barney fotografował w podczerwieni i ultrafiolecie tereny nad którymi przelatywali. Już
w domu, po wywołaniu filmów, stwierdził dziwne formacje skał (czy też innych obiektów)
układające się w krzywoliniowe ścieżki, które wpisywały się w ortogonalną sieć starych dróg,
biegnących wzdłuż południków lub równoleżników.
Większość tych ścieżek układała się w figury podobne do tych na rysunku. Kolega matematyk,
któremu Barney pokazał wywołane zdjęcie, zasugerował, że krzywe, na których układały się
obiekty na zdjęciach, przypominają znane wykresy funkcji "tangens" i "cosinus". Barneya
zainteresowały punkty przecięcia ścieżek, które zobaczył na zdjęciach, bo - jak się spodziewał - są
to ścieżki wydeptane przez celtyckich kapłanów - druidów, co wróżyło zapewne jakąś tajemnicę.
Poprosił matematyka o znalezienie współrzędnych punktów przecięcia ścieżek, aby łatwiej je
można było znaleźć w terenie. Ten oczywiście obiecał pomóc, bo to nie wydawało się trudne, ale
ponieważ zaplanował już z kolegami lot balonem nad Pacyfikiem (ich przygody opisane są w
zadaniu "Dzielni baloniarze"), prosił geologa o cierpliwość. Barney nie może jednak wytrzymać, aż
kolega wróci (zresztą powrót z niebezpiecznej wyprawy wcale nie jest pewny) i szuka pomocy.
Pomóż niecierpliwemu geologowi!
Niebieska ścieżka widoczna na rysunku jest 1/4 fali cosinusoidy stycznej do drogi równoleżnikowej
w punkcie x = 0. Czerwona ścieżka to 1/2 tangensoidy, dla której prawa droga południkowa jest
asymptotą pionową. Krzywa ta przecina północną drogę równoleżnikową w odległości c od
zachodniej drogi południkowej (c < a).
Zadanie
Należy wyznaczyć współrzędną x, określającą położenie punktu przecięcia ścieżek druidów w
jednostkach imperialnych: mile, jardy, stopy, cale.
Wejście
W pierwszym wierszu wejścia podana jest liczba całkowita 0 < N ≤ 1000, równa liczbie zestawów
danych. W kolejnych N wierszach występują trójki liczb całkowitych 0 < a, b, c ≤ 1000,
oznaczające odległości podane w milach angielskich (patrz rysunek).
Wyjście
Dla każdego zestawu danych, w jednym wierszu, należy wypisać cztery liczby całkowite: m, j, s, c
(0 ≤ m, 0 ≤ j < 1760, 0 ≤ s < 3, 0 ≤ c < 12) oddzielone pojedynczymi spacjami określające
współrzędną x (patrz rysunek) punktu przecięcia krzywych (zaokrągloną do najbliższej liczby cali).
Kolejne liczby oznaczają mile, jardy, stopy i cale. Dla porządku przypominamy że: 1 stopa=12 cali,
1 jard=3 stopy, 1 mila=1760 jardów.
     */
    public sealed class Geolog : ProblemBase
    {
        protected override string Input => "2\r\n3 4 2\r\n3 5 1";

        protected override string Output => "1 1101 1 10\r\n0 1594 2 5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int[] coeffs = [1760, 3, 12];
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ').Select(s => double.Parse(s)).ToArray();
                double k = splits[1] / Math.Tan(Math.PI * splits[2] / (2 * splits[0]));
                double kb = k / splits[1];
                double x = 2.0 * splits[0] * Math.Asin((Math.Sqrt(kb * kb + 4) - kb) / 2.0) / Math.PI;
                List<int> format = [];
                for(int j = 0; j < coeffs.Length; j++)
                {
                    format.Add((int)x);
                    x -= (int)x;
                    x *= coeffs[j];
                }
                format.Add((int)Math.Round(x));
                output.Add(string.Join(" ", format));
            }
        }
    }
}
