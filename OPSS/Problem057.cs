using System.Drawing;

namespace OPSS
{
    /* Difficulty: 5/5
     * Zadanie
Należy podać wartość promieniowania oraz współrzędne punktu, w którym to promieniowanie jest
najmniejsze. Punkt musi należeć do obszaru leżącego poza zasięgiem dział laserowych. Dane,
którymi dysponuje robot są następujące:
1. Liczba dział laserowych (N), wymiar przestrzeni (W) w której rozgrywa się wojna (W=2 -
obszar płaski, W=3 - przestrzeń trójwymiarowa, W>3 - hiperprzestrzeń).
2. Działo laserowe o numerze i opisane jest przez współrzędne punktu działania Di i składowe
wektora Li wskazującego główny kierunek rażenia. Działo ostrzeliwuje tylko tę połowę
płaszczyzny (przestrzeni, hiperprzestrzeni), którą wskazuje wektor Li. Przebywanie na
prostej (płaszczyźnie, hiperpłaszczyźnie) rozdzielającej obie połowy jest bezpieczne,
wkroczenie na obszar wskazywany przez wektor powoduje zniszczenie robota. Niektóre
działa mogą znajdować się obszarze ostrzału (np. L8 na rysunku) - ponieważ nie ograniczają
one obszaru bezpiecznego, powinny być zignorowane.
3. Współrzędne punktu A, w którym promieniowanie jest zerowe oraz składowe wektora
promieniowania R, wskazującego kierunek wzrostu i intensywność promieniowania.
Wartość promieniowania w punkcie P, oblicza się mnożąc moduł wektora promieniowania
|R| przez odległość d punktu P od prostej (płaszczyzny, hiperpłaszczyzny) do której wektor
promieniowania jest prostopadły.
Wejście
W pierwszym wierszu wejścia podana jest para liczb naturalnych, oddzielona jedną spacją: N W,
gdzie N jest liczbą dział laserowych, 2<N<1024, a W wymiarem przestrzeni, 1<W≤10. Drugi
wiersz zawiera 2W liczb rzeczywistych oddzielonych spacjami, z których W początkowych liczb
jest współrzędnymi punktu A (punktu leżącego na granicy zerowego promieniowania), a W
następnych liczb oznacza składowe wektora promieniowania R. Dalej następuje N wierszy po 2W
liczb każdy, które oznaczają współrzędne pozycji działa Di i składowe wektora Li wskazującego
główny kierunek rażenia działa. Wszystkie współrzędne punktów (Aj, Dij, 1 ≤ i ≤ N, 1 ≤ j ≤ W)
występujące na wejściu spełniają warunki: 0 ≤ Aj, Dij <10^9. Wszystkie składowe wektorów (Rj,
Lij, 1 ≤ i ≤ N, 1 ≤ j ≤ W) występujące na wejściu spełniają warunki: -10^9< Rj, Lij <10^9 .
Wyjście
Na wyjściu, w jednym wierszu, należy wypisać W+1 liczb całkowitych oddzielonych pojedynczymi
spacjami. Pierwsza liczba p oznacza wartość promieniowania w znalezionym punkcie, 0≤ p <109, a
W następnych liczb to współrzędne tego punktu, -10^9< Xi <10^9. Dane występujące w testach zostały
tak dobrane, ze wynikami są liczby całkowite. Dane wejściowe gwarantują, że znaleziony punkt jest
jednoznaczny.
     */
    public sealed class SztukaPrzetrwania : ProblemBase
    {
        protected override string Input => "8 2\r\n80.0 70.0 -9.0 -18.0\r\n55.5 11.0 12.0 -17.0\r\n36.0 7.0 -4.0 -22.0\r\n16.0 18.0 -18.0 -18.0\r\n7.0 39.0 -24.0 0.0\r\n13.5 54.5 -7.0 13.0\r\n34.0 52.0 12.0 28.0\r\n56.0 31.5 29.0 16.0\r\n70.0 15.0 14.0 0.0";

        protected override string Output => "720 48 46";

        const double PI_2 = Math.PI / 2.0;

        static double[] Norm(double[] a)
        {
            if (a.Length == 2)
                return [-a[1], a[0]];
            var ret = new double[a.Length];
            for (int k = 0; k < a.Length; k++)
            {
                double val = 1.0, val2 = -1.0;
                for (int l = (k + 2) % a.Length; l != (k + a.Length - 2) % a.Length; l = (l + 1) % a.Length)
                {
                    val *= a[l];
                }
                val2 = -val;
                val *= a[(k + 1) % a.Length];
                val2 *= a[(k + a.Length - 1) % a.Length];
                ret[k] = a[k] * (val - val2);
            }
            return ret;
        }

        static double[] Crossing(double[] pt1, double[] pt2, double[] norm1, double[] norm2)
        {
            int index1 = 0;
            while (norm1[index1] == 0)
                index1++;
            int index2 = 0;
            while (index2 < norm1.Length && (index2 == index1 || (norm1[index2] == 0 && norm2[index2] == 0)))
                index2++;
            if (index2 == norm1.Length)
                index2 = (index1 == 0) ? 1 : 0;
            var t1 = (pt2[index1] - pt1[index1] + norm2[index1] * (pt1[index2] - pt2[index2]) / norm2[index2]) / (norm1[index1] - norm2[index1] * norm1[index2] / norm2[index2]);
            return pt1.Zip(norm1, (a, b) => b * t1 + a).ToArray();
        }

        record Laser(double[] Point, double[] Norm, List<double> Atans);

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
            splits = input[1].Split(' ');
            var radiationPt = splits.Take(b).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            var radiationVec = splits.Skip(b).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            var radiationNorm = Norm(radiationVec);
            var modulus = Math.Sqrt(radiationVec.Select(s => s * s).Sum());
            List<Laser> lasers = [];
            double[] pt = [];
            double dist = double.MaxValue;
            for (int i = 2; i <= a + 1; i++)
            {
                splits = input[i].Split(' ');
                var laserPt = splits.Take(b).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                var laserVec = splits.Skip(b).Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                var laserNorm = Norm(laserVec);
                List<double> atans = [];
                for (int j = 0; j < b; j++)
                    for (int k = j + 1; k < b; k++)
                        atans.Add(Math.Atan2(laserVec[j], laserVec[k]));
                lasers.Add(new(laserPt, laserNorm, atans));
            }
            List<(double, double[])> candidates = [];
            for (int i = 0; i < a; i++)
            {
                for (int j = i + 1; j < a; j++)
                {
                    var cross = Crossing(lasers[i].Point, lasers[j].Point, lasers[i].Norm, lasers[j].Norm);
                    if (cross.Any(c => double.IsNaN(c)))
                        continue;
                    bool valid = true;
                    var cross2 = Crossing(radiationPt, cross, radiationNorm, radiationVec);
                    var dist2 = Math.Sqrt(cross2.Zip(cross, (a, b) => (a - b) * (a - b)).Sum()) * modulus;
                    if (dist2 < dist)
                    {
                        foreach (var l in lasers.Except([lasers[i], lasers[j]]))
                        {
                            int dim = 0;
                            var diffs = cross.Zip(l.Point, (a, b) => a - b).ToArray();
                            for (int k = 0; k < b; k++)
                            {
                                for (int k2 = k + 1; k2 < b; k2++)
                                {
                                    var atan = Math.Atan2(diffs[k], diffs[k2]);
                                    if (Math.Abs(atan - l.Atans[dim]) < PI_2)
                                    {
                                        valid = false;
                                        break;
                                    }
                                    dim++;
                                }
                                if (!valid)
                                    break;
                            }
                        }
                        if (valid)
                        {
                            dist = dist2;
                            pt = cross;
                        }
                    }
                }
            }
            output.Add($"{(int)Math.Round(dist)} {string.Join(" ", pt.Select(p => (int)p))}");
        }
    }
}
