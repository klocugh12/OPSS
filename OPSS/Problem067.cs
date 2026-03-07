namespace OPSS
{
    /* 4/5
     * Firma Miracle dbająca o stały i niczym nie zachwiany rozwój swojej bazy danych, wymyśliła
nowy, super szybki indeks na kolumnach przechowujących liczby składające się co najwyżej z 20
cyfr. Szczegóły dotyczące zasady działania indeksu, są najściślej strzeżoną tajemnicą firmy.
Wiadomo jedynie, że jest związana z liczbą 11.
Grupa testująca otrzymała szereg zapytań o wartości kluczy w tym indeksie. W celu wyznaczenia
wskaźników, które pozwolą sklasyfikować badany indeks w rankingu najlepszych indeksów
Miracle, naukowcy z firmy muszą wiedzieć ile jest możliwych wartości indeksu dla zadanego
wzorca klucza i reszty z dzielenia przez 11. Niestety, sami nie potrafią tego zrobić. Musisz im
pomóc.
Wzorzec klucza jest ciągiem składającym się z symboli 'X','0','1','2','3','4','5','6','7','8',9'. Symbol 'X'
zastępuje jedną cyfrę ze zbioru '0','1','2','3','4','5','6','7','8','9'. Przykładowe wzorce kluczy w indeksie
mogą wyglądać następująco: 'XXX', 'X1X', '2XXX'. Do wzorca można dopasować jedynie liczby -
dopasowania nie mogą zawierać zer znaczących. Na przykład, '012' nie jest poprawnym
dopasowaniem do wzorca 'X12'.
Zadanie
Wyznacz ilość liczb pasujących do wzorca W, które przy dzieleniu przez 11 dają resztę r.
Wejście
W pierwszym wierszu wejścia znajduje się liczba zestawów danych n, 1 ≤ n ≤ 100. W kolejnych
wierszach następują zestawy danych. Każdy zestaw składa się z dwóch linii. W pierwszej jest liczba
cyfr d, 1 ≤ d ≤ 20 we wzorcu i reszta r, 0 ≤ r ≤ 10 z dzielenia przez 11. W drugiej linii zestawu
znajduje się wzorzec W długości d zawierający znaki 'X','0','1','2','3','4','5','6','7','8','9'.
Wyjście
Dla każdego zestawu danych na wyjściu powinna znaleźć się jedna wartość P określająca ilość
liczb pasujących do wzorca W, które przy dzieleniu przez 11 dają resztę r. Możesz założyć, że 0 ≤
P < 2^63-1.
     */
    public sealed class Jedenastka : ProblemBase
    {
        protected override string Input => "3\r\n2 5\r\nXX\r\n1 8\r\nX\r\n1 8\r\n7";

        protected override string Output => "8\r\n1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                int b = int.Parse(input[j].Split(' ')[1]);
                j++;
                string s = input[j];
                j++;
                int c = 0;
                int xxx = 0;
                for(int k = 0; k < s.Length; k++)
                {
                    bool isMod10 = (s.Length - k) % 2 == 1;
                    if (s[k] == 'X')
                    {
                        xxx++;
                    }
                    else
                    {
                        if (isMod10)
                            b = (b + s[k] - '0') % 11;
                        else
                            b = (b + 11 - s[k] + '0') % 11;
                    }
                }
                if (xxx == 0)
                {
                    output.Add(b == 0 ? "1" : "0");
                    continue;
                }
                else
                {
                    c = (int)(9 * Math.Pow(10, xxx - 1) / 11.0);
                    if ((s.Length % 2 == 1) ^ (b == 0 || b == 10))
                        c++;
                }    
                output.Add(c.ToString());
            }
        }
    }
}
