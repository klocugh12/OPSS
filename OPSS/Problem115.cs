using System.Globalization;

namespace OPSS
{
    /* Difficulty: 4/5
     * 
Janek jest studentem ogrodnictwa. W czasie zajęć opiekuje się egzotycznymi drzewkami
owocowymi mango. Zauważył, że szkodniki, które uszkadzają owoce mango często uszkadzają
skórkę owocu w miejscach najbardziej nasłonecznionych (lub oświetlonych żarówką). Swoje
obserwacje chce przekazać w czasie seminarium Koła Naukowego Ogrodników. Janek chciałby,
aby ta prezentacja wyglądała bardzo efektownie - powinna być poparta jakąś "symulacja
komputerową". Ponieważ nie jest on zbyt biegły w technice cyfrowej, więc poszukuje kogoś, kto
potrafi wykonać taką symulację. Najistotniejszym elementem tego pokazu musi być wyznaczenie
miejsca uszkodzenia owocu w zależności od jego kształtu, kierunku padania promieni i odległości
od sztucznego źródła światła.
Powierzchnia owocu została opisana za pomocą małych płatów - figur płaskich (trójkąty,
czworokąty) tworzących wypukły wielościan. Należy podać numer tego płata, który jest najlepiej
oświetlony, czyli mówiąc ściśle, na jego powierzchni jest największe natężenie oświetlenia.
Natężenie oświetlenia (fizycy mierzą je w luksach) płatu zależy od nachylenia jego powierzchni
względem promieni padających. Maksymalne oświetlenie dostajemy wtedy, gdy promienie są
prostopadłe do powierzchni, zerowe oświetlenie wtedy gdy promienie są równoległe lub nie
docierają bezpośrednio do powierzchni, w innych przypadkach dostajemy wartości pośrednie.
Natężenie oświetlenia jest odwrotnie proporcjonalne do kwadratu odległości płatu od źródła
światła. W tym zadaniu należy przyjąć, dla uproszczenia, że dla wszystkich punktów płatu
odległość jest taka sama i równa odległości źródła światła od środka płatu (wyznaczonego przez
średnią arytmetyczną współrzędnych wierzchołków płatu). Można również założyć, że jeśli
natężenie oświetlenia najlepiej oświetlonego płatu jest równe E, to natężenie oświetlenia każdego
innego płatu jest mniejsze od (1 - 10^-11) * E.
Wejście
W pierwszym wierszu wejścia jest podana liczba zestawów danych L, (1 ≤ L ≤ 5). Po niej następują
zestawy danych. W pierwszym wierszu zestawu danych podana jest para liczb naturalnych,
oddzielona jedną spacją: N W, gdzie N jest liczbą płatów powierzchni (4 ≤ N ≤ 15000), a W liczbą
punktów, które są wierzchołkami wielokątów opisujących powierzchnię owocu (4 ≤ W ≤ 15000).
Drugi wiersz zestawu zawiera 3 liczby rzeczywiste oddzielone pojedynczą spacją (-10^10 < x,y,z <
10^10), które są współrzędnymi źródła światła. Można założyć, że źródło światła jest zawsze
umieszczone na zewnątrz powierzchni owocu. Następne W wierszy zestawu zawiera po 3 liczby
rzeczywiste (-10^10 < x,y,z < 10^10) oddzielone pojedynczą spacją, które są współrzędnymi
wierzchołków wielościanu. Wierzchołki ponumerowane są kolejnymi liczbami naturalnymi od 1 do
W w kolejności podawania ich współrzędnych. Następne N wierszy zestawu danych zawiera po 4
liczby całkowite (pierwsze trzy z nich są dodatnie, czwarta jest nieujemna), które są numerami
wierzchołków należących do płatów powierzchni. Gdy czwarty numer wierzchołka jest równy 0, to
płat powierzchni jest trójkątem. Gdy wszystkie 4 numery są większe od 0, to płat jest czworokątem.
Numery wierzchołków są również rozdzielone pojedynczą spacją.
Wyjście
Na wyjściu, dla każdego zestawu danych, w jednym wierszu należy wypisać jedną liczbę naturalną,
która jest numerem najlepiej oświetlonego płata powierzchni.
     */
    public sealed class Robaczek : ProblemBase
    {
        protected override string Input => "1\r\n4 4\r\n6.100000E+00 5.100000E+00 4.300000E+00\r\n1.619953E-01 8.379142E-02 7.330799E-03\r\n1.923704E-01 2.691684E-01 2.354919E-02\r\n3.671053E-01 3.717102E-01 3.252042E-02\r\n7.274067E-02 3.063127E-01 2.044082E-01\r\n2 1 3 0\r\n1 2 4 0\r\n3 1 4 0\r\n3 2 4 0";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                j++;
                var light = input[j].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                j++;
                List<double[]> points = [];
                double[] mins = [double.MaxValue, double.MaxValue, double.MaxValue],
                    maxs = [double.MinValue, double.MinValue, double.MinValue];
                for (int k = 0; k < splits[0]; k++)
                {
                    var splits2 = input[j].Split(' ').Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
                    for (int l = 0; l < mins.Length; l++)
                    {
                        mins[l] = Math.Min(mins[l], splits2[l]);
                        maxs[l] = Math.Max(maxs[l], splits2[l]);
                    }
                    points.Add(splits2);
                    j++;
                }
                int[] range = [0, 1, 2];
                var center = new double[3];
                foreach (var r in range)
                    center[r] = points.Select(p => p[r]).Sum() / points.Count;
                var line = center.Zip(light, (a, b) => a - b).ToArray();
                var dirs = points.Select(p => range.Select(r => p[r] - light[r]).ToArray()).ToArray();
                var dists = points.Select(p => range.Select(r => (p[r] - light[r]) * (p[r] - light[r])).Sum()).ToArray();
                var distLightCenter = light.Zip(center, (a, b) => (a - b) * (a - b)).Sum();
                double minAvg = double.MaxValue, min = 0;
                for (int k = 0; k < splits[1]; k++)
                {
                    var splits2 = input[j].Split(' ').Where(s => s != "0").Select(s => int.Parse(s)).ToArray();
                    var d = splits2.Select(s => dirs[s - 1]);
                    var p = splits2.Select(s => dists[s - 1]);
                    j++;
                    if (p.All(d2 => d2 > distLightCenter))
                        continue;
                    var avgs = range.Select(r => d.Select(d2 => d2[r]).Average()).ToArray();
                    var val = range.Select(r => (avgs[r] - line[r]) * (avgs[r] - line[r])).Sum();
                    if(val < minAvg)
                    {
                        minAvg = val;
                        min = k + 1;
                    }
                }
                output.Add(min.ToString());
            }
        }
    }
}
