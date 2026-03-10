namespace OPSS
{
    /* Difficulty: 3/5
     * 
Andrzej i Bartek (Alice i Bob jak lubią amerykanie) oglądali film "Ghost Busters", w którym Dr.
Peter Venkman (grany przez Billa Murraya) przeprowadza badania zdolności telepatycznych
studentów. Badanie to polega na pokazywaniu jednemu badanemu tzw. karty Zenera, która zawiera
jeden z pięciu symboli: gwiazda, fale, krzyż, koło, kwadrat. Badany "nadawca" stara się przekazać
jej obraz telepatycznie drugiemu badanemu ("odbiorcy"), który wskazuje jedną z pięciu kart.
Chłopcy postanowili powtórzyć tę zabawę przy pomocy komputerów. Po zastanowieniu doszli do
wniosku, że na początek, kiedy nie mają jeszcze dużej wprawy, karty Zenera są zbyt
skomplikowane i wymyślili swoje symbole: elipsę, prostokąt, trójkąt. Ustalili też, że osie elipsy i
boki prostokąta będą zawsze równoległe do krawędzi obrazu, żaden z kątów trójkąta nie będzie
rozwarty, co najmniej jeden bok trójkąta będzie równoległy do krawędzi obrazu. Andrzej
namalował kilka takich figur czarnym pisakiem, i przesłał ich skanowane obrazy do Bartka, którego
zadaniem było odgadnięcie jaki obraz został przesłany. Aby zagadka nie była zbyt prosta, pliki ze
skanera zostały pozbawione nagłówków i zakodowane. Rozwiązanie utrudniała też niewielka (64
kB) pamięć komputerów, którymi dysponowali chłopcy (to był początek lat 80-tych!).
Spróbuj czy ty też jesteś dobrym komputerowym "telepatą" i czy odgadniesz, jaką figurę skanował
Andrzej. Masz do dyspozycji znacznie więcej pamięci na przechowanie danych, bo aż 512 kB.
Zadanie
Należy podać numer identyfikujący figurę: 1 - prostokąt, 2 - elipsę, 3 - trójkąt.
Wejście
W pierwszym wierszu wejścia znajduje się liczba D, określająca ilość zestawów danych, 1 ≤ D ≤
10. Każdy z D zestawów danych składa się z dwóch wierszy. W pierwszym wierszu zestawu
znajdują się dwie liczby całkowite H i B oddzielone jedną spacją. H oznacza wysokość obrazu
liczoną w pikselach: 9 ≤ H ≤ 2000, B liczbę znaków szesnastkowych przypadającą na jedną linię
obrazu, 3 ≤ B ≤ 500. Szerokość obrazu liczona w pikselach (L) jest zatem równa: L=4B. W drugim
wierszu znajduje się HxB znaków ze zbioru: [0,1,2,..,9,A,B,C,D,E,F], oznaczających cyfry
szesnastkowe, którymi został zakodowany skanowany rysunek.
Każda cyfra szesnastkowa oznacza 4 punkty obrazu (piksele). W jej rozwinięciu binarnym 1
oznacza kolor czarny, 0 oznacza białe tło. Pierwsza cyfra szesnastkowa opisuje lewy dolny
narożnik obrazu.
Do konwersji obrazów z formatu .BMP (monochromatyczna bitmapa) można posłużyć się
zamieszczonym programem.
Rys. Przykładowy obraz elipsy i trójkąta.
Wyjście
Na wyjściu, dla każdego zestawu, należy wypisać liczbę 1, 2 lub 3, która oznacza identyfikator
figury.
     */
    public sealed class KomputerowaTelepatia : ProblemBase
    {
        protected override string Input => "2\r\n13 4\r\n000003C01FF83FFC3FFC7FFE7FFE7FFE3FFC3FFC1FF803C00000\r\n11 3\r\n00000703F1FFFFF3FF0FF03F00F003000";

        protected override string Output => "2\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                List<string> lines = [];
                string zeros = new string('0', b);
                int k = 0;
                while (k < input[j].Length)
                {
                    string s = input[j].Substring(k, b);
                    if (s != zeros)
                        lines.Add(s);
                    k += b;
                }
                if (lines[0] != lines[lines.Count - 1])
                {
                    output.Add("3");
                }
                else if(lines.All(l => l == lines[0]))
                {
                    output.Add("1");
                }
                else
                {
                    output.Add("2");
                }
                j++;
            }
        }
    }
}
