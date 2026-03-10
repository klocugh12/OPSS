namespace OPSS
{
    /* Difficulty: 3/5
     * 
Kosmita wylądował w centrum miasta i chce zniszczyć wszystkie budynki. Tym razem, wyjątkowo,
nie będziesz ratował świata, ale pomożesz kosmicie.
Statek kosmity jest wyposażony w potężne działo laserowe dużej mocy. Działo strzela wiązkami
laserowymi, które są półprostymi wychodzącymi ze statku kosmity. Wiązka lasera jest tak silna, że
przechodzi przez wszystkie budynki znajdujące się na jej drodze. Budynek trafiony promieniem
lasera natychmiast zamienia się w obłok pary. Wiązka lasera przemieszcza się tuż nad ziemią - jest
w stanie trafić w każdy budynek na swojej drodze niezależnie od jego wysokości. Każdy strzał
pozbawia kosmitę zasobów energii, dzięki której może wrócić na macierzystą planetę. Kosmicie
zależy na tym, aby zniszczyć wszystkie budynki możliwie najmniejszą liczbą strzałów.
Zadanie
Dysponując dokładnym planem rozmieszczenia budynków, musisz wyznaczyć minimalną liczbę
strzałów potrzebną do zniszczenia wszystkich budynków, przy założeniu, że statek nie może
zmieniać swojej pozycji.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, 1 ≤ C ≤ 10, określająca liczbę zestawów
danych. W kolejnych wierszach znajdują się zestawy danych. W pierwszym wierszu każdego
zestawu danych znajduje się liczba N, 1 ≤ N ≤ 10^5, oznaczająca ilość budynków w mieście. W
kolejnych N wierszach znajdują się opisy budynków. W każdym następnym wierszu danego
zestawu znajdują się 4 liczby całkowite: xl, yg, xp, yd, -10^9 ≤ xl, yg, xp, yd ≤ 10^9, xl < xp, yd < yg,
gdzie punkt (xl, yg) jest lewym górnym rogiem budynku, a punkt (xp,yd) prawym dolnym rogiem
budynku. Zakładamy, że wszystkie budynki mają ściany prostopadłe do osi układu współrzędnych.
Żadne 2 budynki nie zajmują wspólnej powierzchni, jednak mogą "dotykać się" rogami i bokami.
Ponieważ niezbędnych pomiarów dokonujemy na statku kosmicznym, więc początek układu
współrzędnych został ustalony w miejscu lądowania statku. W tym miejscu nie stoi też żaden
budynek.
Wyjście
Dla każdego zestawu danych należy wypisać na standardowe wyjście linię zawierającą jedną liczbę
naturalną, oznaczającą ilość strzałów konieczną do zniszczenia wszystkich budynków, przy
założeniu, że statek nie zmienia pozycji i znajduje się w punkcie o współrzędnych (0, 0).
     */
    public sealed class Kosmita : ProblemBase
    {
        protected override string Input => "1\r\n7\r\n4 0 6 -4\r\n7 0 9 -2\r\n6 -6 8 -8\r\n8 -8 10 -10\r\n-2 -4 4 -9\r\n-3 6 4 2\r\n-4 11 4 7";

        protected override string Output => "3";

        bool IsInside(double from, double to, double toCheck)
        {
            if (from <= to)
                return from <= toCheck && to >= toCheck;
            else
                return from <= toCheck && to >= toCheck - Math.PI;
        }

        (double, double) Overlap((double, double) first, (double, double) second)
        {
            bool pass1 = first.Item2 < first.Item1;
            bool pass2 = second.Item2 < second.Item1;
            if (pass1 == pass2)
                return (Math.Max(first.Item1, second.Item1), Math.Min(first.Item2, second.Item2));
            else if (pass1)
            {
                return (second.Item1 < 0 ? Math.Min(first.Item1, second.Item1) : Math.Max(first.Item1, second.Item1),
                    second.Item2 < 0 ? Math.Max(first.Item1, second.Item1) : Math.Min(first.Item1, second.Item1));
            }
            else
            {
                return (first.Item1 < 0 ? Math.Min(first.Item1, second.Item1) : Math.Max(first.Item1, second.Item1),
                    first.Item2 < 0 ? Math.Max(first.Item1, second.Item1) : Math.Min(first.Item1, second.Item1));
            }

        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                List<(double, double)> houseSweeps = [];
                int c = int.Parse(input[j]);
                j++;
                for (int k = 0; k < c; k++)
                {
                    var coords = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    double[] angles = [Math.Atan2(coords[1], coords[0]), Math.Atan2(coords[3], coords[0]),
                            Math.Atan2(coords[1], coords[2]), Math.Atan2(coords[3], coords[2])];
                    houseSweeps.Add((angles.Min(), angles.Max()));
                    j++;
                }
                houseSweeps.Sort();
                List<(double, double)> lasers = [];
                foreach (var house in houseSweeps)
                {
                    int index = -1;
                    if (lasers.Count > 0 && (IsInside(lasers[^1].Item1, lasers[^1].Item2, house.Item1) || IsInside(lasers[^1].Item1, lasers[^1].Item2, house.Item2)))
                        index = lasers.Count - 1;
                    else if (lasers.Count > 1 && (IsInside(lasers[0].Item1, lasers[0].Item2, house.Item1) || IsInside(lasers[0].Item1, lasers[0].Item2, house.Item2)))
                        index = 0;
                    if(index >= 0)
                    {
                        var found = lasers[index];
                        lasers.RemoveAt(index);
                        lasers.Insert(index, Overlap(found, house));
                    }
                    else
                        lasers.Add(house);
                }
                output.Add(lasers.Count.ToString());
            }
        }
    }
}
