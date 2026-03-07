namespace OPSS
{
    /* 3/5
     * Juhasi Jędruś i Bartuś postanowili urozmaicić sobie wieczorne zaganianie owiec wymyśloną przez
siebie zabawą. Każdy z nich z pomocą psa może zagonić do zagrody dowolną liczbę owiec
pasących się na hali w kilku grupach. Ponieważ mają jednego psa, to zapędzanie owiec będą robili
na zmianę: najpierw Jędruś potem Bartuś i potem znów Jędruś itd. aż do zapędzenia ostatniej. Ten
kto zapędza ostatnią owcę przegrywa, bo jego kolega może w tym czasie wziąć jedyną butelkę
piwa, która się chłodzi w potoku.
Ponieważ grupy owiec są od siebie oddalone to w czasie jednego zapędzania można zagonić
dowolną liczbę owiec (minimum jedną, maksimum wszystkie) pod warunkiem, że należą do jednej
grupy. Kolejność wyboru grup jest dowolna.
Po kilku zmianach, Jędruś wziął butelkę piwa z potoku mimo, że jeszcze sporo owiec pasło się na
hali, bo jak powiedział i tak będzie ona jemu się należała bez względu na to co zrobi Bartuś.
Zastanów się czy jest to możliwe?
Zadanie
Napisz program, który wskaże juhasa, który wygra zabawę przy założeniu, że zaczyna zawsze
Jędruś i obaj są dostatecznie sprytni aby wykorzystać wszystkie szanse jakie stwarza im układ
owiec pasących się na hali.
Wejście
W pierwszym wierszu wejścia znajduje się liczba całkowita C, oznaczająca liczbę zestawów
danych, 1 ≤ C ≤ 10. W następnych C wierszach podane są liczby całkowite oznaczające liczbę grup
owiec i ich liczebność. Pierwsza liczba w wierszu oznacza liczbę grup owiec N, 1 ≤ N ≤ 100000, a
następujące po niej N liczb ai, 1 ≤ ai ≤ 2^31-1 dla 1 ≤ i ≤ N oznaczają liczebność każdej grupy.
Wyjście
Na wyjściu, w C wierszach należy wypisać pojedyncze duże litery: J, gdy wygra Jędruś lub B, gdy
wygra Bartuś.
     */
    public sealed class Owce : ProblemBase
    {
        protected override string Input => "2\r\n5 7 9 23 11 17\r\n3 1 2 3";

        protected override string Output => "J\r\nB";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var groups = input[i].Split(' ').Select(s => int.Parse(s)).ToArray();
                output.Add( groups.Select(g => g > 1 ? 2 : 1).Sum() % 2 == 0 ? "J" : "B");
            }
        }
    }
}
