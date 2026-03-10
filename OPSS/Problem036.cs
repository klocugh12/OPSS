namespace OPSS
{
    /* Difficulty: 3/5
     * W pewnym nowoczesnym mieście zbudowano ronda według nowej technologii. Każde rondo ma n
lamp. Przed zmierzchem wypuszczany jest robot, którego zadaniem jest włączenie wszystkich
lamp. Jednak programista dokonał pewnych błędów w programie robotów i wyprodukowano serię n
robotów, które zamiast podchodzić do wszystkich lamp podchodziły tylko do k pierwszych (k -
numer seryjny robota). Na dodatek, po podejściu do lampy robot przełącza wyłącznik (jeżeli był
włączony to wyłącza, jeśli wyłączony to włącza). Następnie roboty podchodzą do k następnych
lamp i tak w kółko, aż nie zostanie im wydany rozkaz zakończenia prac, który jest realizowany po
zakończeniu przełączania aktualnych k lamp. Projektanci ronda załamali ręce. Jednak zauważyli, że
część robotów jest w stanie zapalić wszystkie lampy (przykładowo dla n=4 wszystkie roboty są w
stanie tego dokonać; dla n=3 tylko roboty o numerze seryjnym 1 i 3). Wszyscy wielce uradowaniu
już zabrali się do selekcji odpowiednich robotów, gdy Urząd Miasta i Gminy zarządził wielkie
oszczędności - na każdym rondzie może palić się tylko jedna lampa. To doszczętnie dobiło zespół.
Jednak Ty, młody, inteligentny informatyk wiesz, że niektóre roboty potrafią tego dokonać. Dlatego
też postanowiłeś napisać program, który policzy ile z serii n robotów mogłoby obsłużyć rondo o n
lampach.
Wejście
W pierwszej linii znajduje się liczba naturalna D, 1 ≤ D ≤ 100, oznaczająca liczbę zestawów
danych. Każdy zestaw składa się z jednej linii zawierającej liczbę całkowitą n, 1 ≤ n ≤ 10^9.
Wyjście
Dla każdego zestawu danych w osobnej linii wypisz liczbę robotów, które mogą obsłużyć rondo o n
lampach.
     */
    public sealed class Robot : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n6";

        protected override string Output => "2\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                int c = a, k = 2, sqrt = (int)Math.Ceiling(Math.Sqrt(a));
                List<int> divisors = [];
                while(k <= sqrt)
                {
                    while(c % k == 0)
                    {
                        if (!divisors.Contains(k))
                            divisors.Add(k);
                        c /= k;
                    }
                    k++;
                }
                c = a;
                if (divisors.Count == 0)
                    c--;
                else
                {
                    int prod = 1;
                    for (int j = 0; j < divisors.Count; j++)
                    {
                        prod *= divisors[j];
                        c -= a / prod;
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
