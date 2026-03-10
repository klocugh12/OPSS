namespace OPSS
{
    /* Difficulty: 4/5
     * Bandyci obrabowali bank. Będą próbowali przedostać się w bezpieczne miejsce na drugim końcu
miasta. Jednak okazało się, że ich samochód jest uszkodzony i są zmuszeni uciekać pieszo. Nie
będą więc, rzecz jasna, uciekać ulicami, a poprzez liczne w mieście parki i tereny niezabudowane
(dla ułatwienia wszystkie takie tereny nazywać będziemy parkami). Park jest obszarem miasta
ograniczonym trzema lub więcej ulicami, nie zawierający ulic i niezabudowany. Bandyci mogą
przemieścić się z parku do parku tylko wtedy, gdy mają one co najmniej jedną wspólną ulicę je
ograniczającą lub oba sąsiadują ze wspólnym skrzyżowaniem (wtedy bandyci szybko przebiegają
na drugi koniec ulicy lub skrzyżowania). Bandyci postanowili uciekać tylko i wyłącznie poprzez
parki, wybierając jedną z możliwych tras, niekoniecznie najkrótszą aby nie ułatwiać pracy
ścigającej ich policji.
Przed komendantem policji stoi poważne zadanie złapania bandytów. Wie, że będą uciekać pieszo
poprzez sąsiadujące parki, ale nie wie dokładnie którędy. Bandyci znajdują się w parku o numerze 1
i chcą dostać się do parku o numerze p. W parkach o numerach 1 i p bandyci czują się bezpiecznie i
do tych parków policja nie ma wstępu. Parki 1 i p na całe szczęście nie sąsiadują ze sobą, więc
policja nie jest bez szans. Jedną z możliwości rozpatrywanych przez komendanta, jest zastawienie
zasadzek na bandytów w parkach przez które mogą się przemieszczać. Nie może jednak dopuścić
do tego, by bandyci się wymknęli. Komendant chce wiedzieć, w ilu co najmniej parkach należy
umieścić policyjne patrole, aby bandyci nie mieli możliwości ucieczki. Zakładamy, że jeśli park jest
patrolowany, i bandyci spróbują się przez niego przedostać, zostaną złapani. Pomóż komendantowi
podjąć decyzję i wyznacz minimalną liczbę parków jaką należy patrolować, aby mieć gwarancję że
bandyci zostaną złapani. Jeśli liczba ta będzie zbyt duża, może nie wystarczyć radiowozów i trzeba
będzie szybko szukać innego planu, więc to od Ciebie w dużej mierze zależy sukces całej operacji.
Do dzieła! Każda minuta ma znaczenie!
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, 1<=C<=100, oznaczająca ilość zestawów
danych. W kolejnych wierszach znajdują się zestawy danych. W pierwszym wierszu każdego
zestawu danych znajdują się dwie liczby, n i m, 1<=n<m<=100000. Są to odpowiednio: liczba
skrzyżowań i liczba ulic w mieście. Skrzyżowania są ponumerowane od 1 do n, kolejnymi liczbami
naturalnymi. W kolejnych m wierszach znajdują się pary liczb oznaczające numery skrzyżowań
które łączą kolejne ulice. Dwa skrzyżowania mogą być bezpośrednio połączone co najwyżej jedną
ulicą, a żadne dwie ulice nie przecinają się. W kolejnym wierszu znajduje się liczba p, 3<=p<=300
oznaczająca liczbę parków. W kolejnych p wierszach znajdują się opisy parków. W każdym
wierszu opisującym park znajdują się liczby naturalne, oddzielone pojedynczymi spacjami.
Pierwszą z nich jest liczba skrzyżowań (a tym samym i ulic) sąsiadujących z parkiem, a następnie
po spacji oddzielone spacjami numery tych skrzyżowań. Parki są ponumerowane od 1 do p i
podawane w kolejności rosnących numerów. (Przypominamy, że bandyci znajdują się w parku 1 i
chcą uciec do parku p).
Wyjście
W C wierszach wyjścia należy podać wyznaczoną dla każdego zestawu minimalną liczbę patroli
niezbędną do zatrzymania bandytów przy założeniu, że wybrano plan działania polegający na
patrolowaniu wybranych parków.
     */
    public sealed class Oblawa : ProblemBase
    {
        protected override string Input => "1\r\n12 22\r\n1 2\r\n1 3\r\n2 4\r\n3 4\r\n3 5\r\n3 6\r\n4 6\r\n4 7\r\n4 8\r\n5 6\r\n6 7\r\n7 8\r\n5 9\r\n6 10\r\n7 10\r\n8 10\r\n9 10\r\n9 12\r\n10 12\r\n12 11\r\n10 11\r\n11 8\r\n9\r\n3 4 7 6\r\n4 1 2 3 4\r\n3 3 5 6\r\n3 4 7 8\r\n4 5 9 10 6\r\n3 7 8 10\r\n3 8 10 11\r\n3 9 12 10\r\n3 12 11 10";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                List<HashSet<int>> parksStreets = [];
                int k;
                for(k = 0; k < b; k++)
                {
                    j++;
                }
                a = int.Parse(input[j]);
                j++;
                for (k = 0; k < a; k++)
                {
                    var parkJunctions = input[j].Split(' ').Skip(1).Select(i => int.Parse(i) - 1).ToHashSet();
                    parksStreets.Add(parkJunctions);
                    j++;
                }
                List<List<int>> parkParks = [];
                for (k = 0; k < parksStreets.Count; k++)
                {
                    parkParks.Add(Enumerable.Range(0, parksStreets.Count)
                        .Where(l => k != l && parksStreets[l].Any(p2 => parksStreets[k].Contains(p2))).Distinct().ToList());
                }
                k = 1;
                while(k < parkParks.Count - 1)
                {
                    if (parkParks[k].Skip(1).All(p => parkParks[parkParks[k][0]].Contains(p)))
                    {
                        foreach(var p in parkParks[k])
                            parkParks[p].Remove(k);
                        parkParks[k].Clear();
                    }
                    k++;
                }
                output.Add(parkParks.Where(p => p.Count > 0).Min(p => p.Count).ToString());
            }
        }
    }
}
