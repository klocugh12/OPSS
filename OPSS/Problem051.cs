namespace OPSS
{
    /* 3/5
     * 
Wejście
W pierwszym wierszu wejścia znajduje się liczba D, określająca ilość zestawów danych, 1 ≤ D ≤
5000. W kolejnych wierszach wejścia znajdują się zestawy danych. Każdy z D zestawów danych
składa się z jednej linii zawierającej, oddzielone spacjami, 4 liczby całkowite Sx, Sy, Kx, Ky,
-1000000 ≤ Sx, Sy, Kx, Ky ≤ 1000000. Sx, Sy określają współrzędne startowe, Kx, Ky - współrzędne
końcowe skoczka na nieskończonej szachownicy.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać minimalną liczbę
ruchów potrzebną do przemieszczenia skoczka z pola (Sx,Sy) do pola (Kx,Ky).
     */
    public sealed class Skoczek : ProblemBase
    {
        protected override string Input => "3\r\n2 2 5 7\r\n3 2 4 6\r\n0 0 2 2";

        protected override string Output => "4\r\n3\r\n4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var tab = input[i].Split(' ').Select(s => int.Parse(s)).ToArray();
                int x = Math.Abs(tab[0] - tab[2]);
                int y = Math.Abs(tab[1] - tab[3]);
                int c = (Math.Min(x ,y) / 3) * 3;
                if(c > 0)
                {
                    x -= c;
                    y -= c;
                    c = (c / 3) << 1;
                }
                int d = (Math.Max(x, y) >> 2) << 2;
                if (x > y)
                    x -= d;
                else
                    y -= d;
                c += (d >> 1);
                d = x * y;
                switch(d)
                {
                    case 0:
                        switch(x+y)
                        {
                            case 0:
                                break;

                            case 1:
                                c = (c == 0) ? 3 : c + 1;
                                break;

                            case 2:
                                c += 2;
                                break;

                            case 3:
                                c += 3;
                                break;
                        }
                        break;

                    case 1:
                    case 3:
                        c += 2;
                        break;

                    case 2:
                        c++;
                        break;

                    case 4:
                        if (x + y == 5)
                            c += 3;
                        else
                            c += 4;
                            break;

                    default:
                        break;
                }
                output.Add(c.ToString());
            }
        }
    }
}
