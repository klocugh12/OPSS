namespace OPSS
{
    /* 3/5
     * 
Agencja wywiadu, która zleca Ci od czasu do czasu tajne zadania, ma dla Ciebie kolejny problem
do rozwiązania. W jej OPeracyjnej Sieci Szyfrów (OPSS) pojawił się kolejny szyfr. Według
informacji agentów jest on wykorzystywany przez terrorystów do szyfrowania informacji. Na
szczęście udało się znaleźć algorytm, którym kodowane są wiadomości. Twoim zadaniem jest
napisanie programu, który odkoduje informację, a zatem będzie działał odwrotnie niż znaleziony
algorytm. Algorytm wygląda następująco:
C/C++:
int foo ( int n )
{
return n ^ (n >> 1);
}
Pascal:
function foo ( n : longint ) : longint;
begin
foo := n xor (n shr 1);
end;
Jeśli Twoja funkcja będzie nazywać się oof wówczas wynik wywołania oof(foo(n)) powinien dać
liczbę n.
Zadanie
Napisz program, który dla zadanej liczby całkowitej n obliczy wartość foo(n) oraz oof(n).
Wejście
Każda linia wejścia zawiera dokładnie jedną liczbę całkowitą n, 0 ≤ n ≤ 2^31 - 1. Wczytywanie liczb z
wejścia należy zakończyć gdy n będzie równe 0 - dla tego wiersza Twój program nie powinien nic
wypisywać na standardowym wyjściu.
Wyjście
I-ta linia wyjścia powinna zawierać dokładnie dwie wartości: foo(n), (n) oddzielone pojedyńczą
spacją, gdzie n jest I-tą wczytaną liczbą, foo(n) to wynik szyfrowania liczby n, zaś oof(n) to wynik
deszyfrowania liczby n.
     */
    public sealed class Szyfr : ProblemBase
    {
        protected override string Input => "1\r\n123\r\n1001\r\n689\r\n0";

        protected override string Output => "1 1\r\n70 82\r\n541 689\r\n1001 801";

        void setb(ref int x, int c) => x |= (1 << c);

        bool checkb(int x, int c) => (x & (1 << c)) > 0;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int i = 0;
            while (input[i] != "0")
            {
                int x = int.Parse(input[i]);
                int foo = x ^ (x >> 1);
                int oof = 0;
                int c = -1, y = x;
                while(y > 0)
                {
                    c++;
                    y >>= 1;
                }
                setb(ref oof, c);
                while(c > 0)
                {
                    if (checkb(oof, c) != checkb(x, c - 1))
                        setb(ref oof, c - 1);
                    c--;
                }
                output.Add($"{foo} {oof}");
                i++;
            }
        }
    }
}
