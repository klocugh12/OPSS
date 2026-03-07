namespace OPSS
{
    /* 1/5
     * 
Zorganizowane zostało ogromne całodniowe przyjęcie, na które zaproszono wiele osób. Każda 
    zaproszona osoba mogła przyjść na przyjęcie i wyjść, kiedy tylko zechciała. Zaproszony został 
    też pan Marek, właściciel cukierni, który często obdarowuje swoich znajomych ciastkami. 
    Na przyjęcie zabrał ze sobą C ciastek, a kiedy tylko dotarł na miejsce, od razu rozdał 
    wszystkim obecnym (nie licząc siebie) jak najwięcej z tych C ciastek i każdemu po tyle samo. 
    Ciastka, które po rozdaniu mu zostały, zjadł, bo bardzo je lubi. Napisz program, który 
    obliczy, ile ciastek zjadł pan Marek.
Wejście
Pierwsza linia zawiera liczbę całkowitą D (1 ≤ D ≤ 10), określającą liczbę zestawów danych. 
    W następnych liniach opisane są kolejno po sobie zestawy danych. W pierwszej linii zestawu 
    danych znajduje się liczba całkowita N (1 ≤ N ≤ 100). Druga linia zestawu danych zawiera N 
    liczb całkowitych rozdzielonych pojedynczymi spacjami: x1, x2, x3, ..., xN (-10 ≤ xi ≤ 10 oraz 
    xi ≠ 0 dla 1 ≤ i ≤ N) oznaczających kolejne zmiany liczby obecnych osób na przyjęciu. Dodatnie 
    xi oznacza, że na przyjęcie przyszło xi osób. Ujemne xi oznacza, że z przyjęcia wyszło -xi 
    osób. Przed pierwszą zmianą liczby obecnych, czyli przed x1, na przyjęciu nie było jeszcze 
    nikogo. Po wszystkich N zmianach liczby obecnych, czyli po xN, na przyjęcie przyszedł pan Marek. 
    W trzeciej linii zestawu danych znajduje się liczba całkowita C (1 ≤ C ≤ 1000) oznaczająca liczbę 
    ciastek, z którymi pan Marek przyszedł na przyjęcie.
Wyjście
W kolejnych liniach dla każdego zestawu danych należy wypisać jedną liczbę całkowitą oznaczającą 
    liczbę ciastek, jakie zjadł pan Marek. 
     */
    public sealed class Ciastka : ProblemBase
    {
        protected override string Input => "3\r\n4\r\n1 3 -4 2\r\n7\r\n3\r\n4 2 -5\r\n8\r\n1\r\n9\r\n14";

        protected override string Output => "1\r\n0\r\n5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                int c = int.Parse(input[j]);
                j++;
                var guests = input[j].Split(' ').Select(s => int.Parse(s)).Sum();
                j++;
                int C = int.Parse(input[j]);
                j++;
                output.Add((guests == 0 ? C : C % guests).ToString());
            }
        }
    }
}
