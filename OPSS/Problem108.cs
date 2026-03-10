namespace OPSS
{
    /* Difficulty: 4/5
     * Przy głównej ulicy w stolicy Opsslandii po jednej stronie mieści się pałac królewski, a po drugiej
stronie, wzdłuż ulicy, w rzędzie, w równych odstępach, na N specjalnie przygotowanych miejscach
pomnikowych ponumerowanych kolejnymi liczbami całkowitymi od 1 do N, stoją pomniki
wszystkich N dawnych władców kraju. Zgodnie z tradycją każdy rok ma swojego patrona
wybieranego co roku spośród dawnych władców Opsslandii. Wraz z nadejściem nowego roku
pomnik nowego patrona, w celu uhonorowania patrona, przenoszony jest naprzeciw pałacu
królewskiego na miejsce pomnika poprzedniego patrona. Nakłady pracy potrzebne do przeniesienia
pomnika zależą od ciężaru pomnika i od odległości, na jaką trzeba go przenieść. Dokładniej:
przeniesienie pomnika o ciężarze C z miejsca pomnikowego o numerze A na miejsce pomnikowe o
numerze B wymaga C * abs(A - B) jednostek pracy, gdzie abs(x) oznacza wartość bezwzględną
liczby x. Miejsce naprzeciwko pałacu musi zostać zwolnione, więc pomnik poprzedniego patrona
trzeba przenieść na inne miejsce. Jednak nie zawsze zwykła zamiana miejsc pomników
poprzedniego patrona i nowego patrona będzie optymalna pod względem ilości wykonanej pracy.
Przeniesienie pomnika poprzedniego patrona na miejsce innego lżejszego pomnika, który stoi dość
blisko, a następnie przeniesienie tego lżejszego pomnika na puste miejsce pozostawione przez
pomnik nowego patrona może wymagać wykonania mniejszej pracy. Jeszcze bardziej opłacalne
może być przeniesienie tego lżejszego pomnika na miejsce innego jeszcze lżejszego pomnika, itd.,
aż ostatni przenoszony pomnik stanie na pustym miejscu pomnikowym pozostawionym przez
pomnik nowego patrona.
Zadanie
Napisz program, który wyznaczy najmniejszą liczbę jednostek pracy, jaką trzeba wykonać, aby
przenieść pomniki w taki sposób, żeby pomnik nowego patrona stanął na miejscu pomnikowym
znajdującym się naprzeciwko pałacu królewskiego oraz żeby na każdym miejscu pomnikowym stał
dokładnie jeden pomnik.
Wejście
Pierwsza linia zawiera liczbę całkowitą D (1 ≤ D ≤ 10), określającą liczbę zestawów danych. W
następnych liniach opisane są kolejno po sobie zestawy danych. W pierwszej linii zestawu danych
znajdują się trzy liczby całkowite oddzielone spacjami: N, P oraz K (1 ≤ N ≤ 50000; 1 ≤ P, K ≤ N)
oznaczające odpowiednio: liczbę pomników dawnych władców Opsslandii, numer miejsca
pomnikowego, na którym stoi pomnik nowego patrona, oraz numer miejsca pomnikowego
znajdującego się naprzeciwko pałacu królewskiego. Druga linia zestawu danych zawiera N
oddzielonych od siebie spacjami całkowitych liczb dodatnich mniejszych od 1000000, gdzie i-ta
liczba w tej linii oznacza ciężar pomnika stojącego na i-tym miejscu pomnikowym.
Wyjście
W kolejnych liniach dla każdego zestawu danych należy wypisać jedną liczbę całkowitą
oznaczającą najmniejszą liczbę jednostek pracy, jaką trzeba wykonać, aby przenieść pomniki w taki
sposób, żeby pomnik nowego patrona stanął na miejscu pomnikowym znajdującym się naprzeciwko
pałacu królewskiego oraz żeby na każdym miejscu pomnikowym stał dokładnie jeden pomnik.
     */
    public sealed class CzterystaDwadziesciaDwa : ProblemBase
    {
        protected override string Input => "2\r\n10 2 7\r\n8 3 6 12 9 4 8 7 1 5\r\n10 9 5\r\n8 3 6 12 9 4 8 7 1 5";

        protected override string Output => "37\r\n25";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[1]) - 1, b = int.Parse(splits[2]) - 1;
                j++;
                var tab = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                List<int> solution = [a, b];
                bool cont;
                do
                {
                    cont = false;
                    for (int k = 0; k < solution.Count; k++)
                    {
                        var curr = solution[k];
                        var next = solution[(k + 1) % solution.Count];
                        var minL = 0;
                        for (int l = 1; l < tab.Length; l++)
                        {
                            if (solution.Contains(l))
                                continue;
                            if (tab[curr] * Math.Abs(l - curr) + tab[l] * Math.Abs(l - next)
                                < tab[curr] * Math.Abs(minL - curr) + tab[minL] * Math.Abs(minL - next))
                                minL = l;
                        }
                        if (tab[curr] * Math.Abs(minL - curr) + tab[minL] * Math.Abs(minL - next) <
                            tab[curr] * Math.Abs(next - curr))
                        {
                            solution.Insert(k + 1, minL);
                            cont = true;
                        }
                    }
                }
                while (cont);
                int sum = 0;
                for (int k = 0; k < solution.Count; k++)
                {
                    sum += tab[solution[k]] * Math.Abs(solution[k] - solution[(k + 1) % solution.Count]);
                }
                output.Add(sum.ToString());
            }
        }
    }
}
