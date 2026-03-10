namespace OPSS
{
    /* Difficulty: 4/5
     * Pewnego słonecznego dnia na beztroskie królestwo Bajtocji najechał bezwzględny król Małych i
Miękkich. Wstrząśnięci mieszkańcy po pierwszym szoku wywołanym tak nagłą napaścią podjęli
próby obrony. Jednak pomimo zaciętego oporu, jaki stawiali, duża część ich wojsk dostała się do
niewoli. Wśród nich znalazł się także ukochany nasz Linus T. Jednak on, w przeciwieństwie do
reszty współwięźniów, postanowił uciec z niewoli. Naprzeciw wszystkim przeciwnościom losu
powiodło mu się. Teraz, będąc na wolności, zaczął się martwić o pozostawiony dom i rodzinę.
Dlatego też pierwsze co zrobił, to udał się do najbliższego pubu i zaczął wypytywać jak przebiegają
działania wojenne... Dowiedział się, że bardzo duży obszar jest pod okupacją wroga. Wówczas
zaczął się zastanawiać, czy jego dom też się tam znajduje. Twoim zadaniem jest pomóc Linusowi
T. stwierdzić czy jego dom znajduje się na terenach okupowanych czy nie.
Teren okupowany jest opisany łamaną zamkniętą, której krawędzie nie przecinają się. Do terenu
okupowanego zaliczają się również punkty znajdujące się na odcinkach łączących kolejne punkty
łamanej.
Wejście
W pierwszej linii znajduje się liczba naturalna D, 1 ≤ D ≤ 100, oznaczająca liczbę zestawów
danych. W pierwszej linii każdego zestawu danych znajdują się współrzędne domu Linusa T.
(współrzędne naturalne x, y; 0 ≤ x, y ≤ 10^9. W drugiej linii znajduje się liczba K, 3 ≤ K ≤ 1000,
określająca liczbę punktów ograniczających teren okupowany przez wroga, a następnie K par
współrzędnych kolejnych punktów (współrzędne naturalne kx, ky; 0 ≤ kx, ky ≤ 10^9).
Wyjście
Dla każdego zestawu danych wypisz w osobnej linii pojedyncze słowo TAK lub NIE oznaczające,
czy dom znajduje się na terenie okupowanym.
     */
    public sealed class Wojna : ProblemBase
    {
        protected override string Input => "3\r\n4 4\r\n5 0 0 0 6 1 2 6 6 6 0\r\n2 2\r\n3 0 0 0 10 10 0\r\n5 5\r\n3 10 10 11 13 13 11";

        protected override string Output => "TAK\r\nTAK\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var pt = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                double minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
                var splits = input[j].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                j++;
                List<double[]> points = [];
                for (int j2 = 0; j2 < splits.Length; j2 += 2)
                {
                    double[] point = [splits[j2], splits[j2 + 1], 0];
                    if (point[0] < minX) minX = point[0];
                    if (point[0] > minX) maxX = point[0];
                    if (point[1] < minY) minY = point[1];
                    if (point[1] > maxY) maxY = point[1];
                    points.Add(point);
                }
                (double, double) center = ((minX + maxX) / 2.0, (minY + maxY) / 2.0);
                points.ForEach(p => p[2] = Math.Atan2(p[1] - center.Item2, p[0] - center.Item1));
                points.Sort((a, b) => a[2].CompareTo(b[2]));
                List<double> horiz = [], vert = [];
                for (int k2 = 0; k2 < points.Count; k2++)
                {
                    var curr = points[k2];
                    var next = points[(k2 + 1) % points.Count];
                    if ((curr[0] - pt[0]) * (next[0] - pt[0]) <= 0)
                    {
                        if (curr[0] == next[0])
                            vert.Add(curr[1]);
                        else
                            vert.Add((next[1] - curr[1]) * pt[0] / (next[0] - curr[0]) + (curr[1] - (next[1] - curr[1]) * curr[0] / (next[0] - curr[0])));
                    }
                    if ((curr[1] - pt[1]) * (next[1] - pt[1]) <= 0)
                    {
                        if (curr[0] == next[0])
                            horiz.Add(curr[0]);
                        else
                            horiz.Add((next[0] - curr[0]) * (pt[1] - (curr[1] - (next[1] - curr[1]) * (next[0] - curr[0]))) / (next[1] - curr[1]));
                    }
                }
                vert.Sort((a, b) => a.CompareTo(b));
                horiz.Sort((a, b) => a.CompareTo(b));
                int index1 = -1, index2 = -1;
                for (int k = 0; k < vert.Count - 1; k++)
                {
                    if ((vert[k] - pt[1]) * (vert[k + 1] - pt[1]) <= 0)
                    {
                        index1 = k;
                        break;
                    }
                }
                for (int k = 0; k < horiz.Count - 1; k++)
                {
                    if ((horiz[k] - pt[0]) * (horiz[k + 1] - pt[0]) <= 0)
                    {
                        index2 = k;
                        break;
                    }
                }
                output.Add(index1 % 2 == 0 && index2 % 2 == 0 ? "TAK" : "NIE");
            }
        }
    }
}
