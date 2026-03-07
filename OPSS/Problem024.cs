namespace OPSS
{
    /* 3/5
     * Jeśli zdarzyło Ci się zawieszać firanki, być może zauważyłeś, że bardzo dobrym pomysłem na
równomierne zaczepienie firanki za pomocą żabek jest przypięcie końców firanki do skrajnych
żabek, a następnie wyznaczenie środkowej żabki i przypięcie jej na środku firanki. Powstają w ten
sposób 2 nieprzypięte obszary firanki, które przypinamy analogicznie (rekursywnie) - wyznaczamy
ponownie środkową żabkę i przypinamy ją na środku firanki, itd.
Jednak nie zawsze możemy wyznaczyć środkową żabkę, zwłaszcza wtedy gdy musimy ją wybrać z
parzystej liczby żabek. Chcąc przyczepić ładnie firankę bierzemy linijkę i wyznaczamy punkty
zaczepienia 2 "środkowych" żabek. Cały czas staramy się, o ile jest to możliwe, wyznaczać jedną
środkową żabkę.
Zadanie
Twoim celem będzie wyznaczenie dla zadanej liczby żabek, ile razy będziemy zmuszeni wziąć do
ręki linijkę.
Wejście
W pierwszym wierszu wejścia znajduje się liczba całkowita d, 1 ≤ d ≤ 500000. W kolejnych d
wierszach znajdują się liczby żabek (n, 3 ≤ n < 2^31), dla których należy wyznaczyć ilość pomiarów
potrzebnych do równomiernego rozmieszczenia żabek.
Wyjście
W d liniach wyjścia należy podać dla każdej zadanej na wejściu liczby żabek, liczbę potrzebnych
pomiarów linijką.
     */
    public sealed class Firanki : ProblemBase
    {
        protected override string Input => "2\r\n18\r\n15";

        protected override string Output => "1\r\n6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]) - 2;
                int c = 0, d = 1;
                while(a > 0)
                {
                    if((a / d) % 2== 0)
                    {
                        a -= (d << 1);
                        c += d;
                    }
                    else
                    {
                        a -= d;  
                    }
                    d <<= 1;
                }
                output.Add(c.ToString());
            }
        }
    }
}
