using System.Globalization;

namespace OPSS
{
    /* 3/5
     * 
Pewien znany, najbardziej skuteczny detektyw Opsslandii prowadzi własne śledztwo w kolejnej 
    kryminalnej sprawie. Podejrzanych jest wielu, a wśród nich dokładnie jeden winny. Każdy 
    z podejrzanych może złożyć (nie musi) kilka zeznań. W jednym zeznaniu podejrzany może wskazać 
    winnego bądź potwierdzić lub zaprzeczyć złożone wcześniej zeznanie dowolnego podejrzanego. 
    Detektyw swoją skuteczność zawdzięcza własnemu systemowi wyszukiwania winnego. Według tego 
    systemu osobą uznaną za winną zostaje ta osoba spośród podejrzanych, dla której liczba osób, 
    które skłamały przy założeniu, że winna jest ta osoba, jest najmniejsza.
Należy wziąć pod uwagę fakt, że dany podejrzany może złożyć wykluczające się zeznania - np. raz 
    twierdzi że winna jest pewna osoba, drugi raz że inna, albo twierdzi że każdy z podejrzanych 
    jest niewinny - wtedy taki podejrzany kłamie.
Napisz program, który wskaże osobę uznaną przez system detektywa za winną. Jeśli jego system 
    uznaje wiele osób za winne, program powinien wskazać wszystkie te osoby.
Wejście
W pierwszym wierszu wejścia znajduje się liczba całkowita D określająca liczbę zestawów danych 
    (1 ≤ D ≤ 10). W kolejnych liniach wejścia występują opisy kolejnych zestawów danych. 
    W pierwszej linii zestawu danych znajdują się oddzielone pojedynczym odstępem dwie liczby 
    całkowite: N, Z (1 ≤ N ≤ 10000, 1 ≤ Z ≤ 20000), określające odpowiednio: liczbę podejrzanych 
    (podejrzanych numerujemy kolejnymi liczbami naturalnymi od 1 do N) oraz liczbę wszystkich 
    złożonych zeznań. W kolejnych Z liniach zestawu występują opisy zeznań w kolejności ich 
    składania - każde w oddzielnej linii. Zeznanie składa się z oddzielonych od siebie pojedynczym 
    odstępem: liczby całkowitej P, znaku C, oraz liczby całkowitej K (1 ≤ P ≤ N). Znak C określa 
    typ zeznania:
znak C równy "W" - oznacza, że podejrzany o numerze P stwierdził, że osoba o numerze K jest winna,
znak C równy "P" - oznacza, że podejrzany P stwierdził, że zeznanie złożone jako K-te jest prawdziwe,
znak C równy "F" - oznacza, że podejrzany P stwierdził, że zeznanie złożone jako K-te jest fałszywe.
Wyjście
W kolejnych liniach, dla każdego zestawu danych, należy wypisać w porządku rosnącym numery osób 
    podejrzanych uznanych przez system jako winne. 
     */
    public sealed class Detektyw : ProblemBase
    {
        protected override string Input => "2\r\n3 4\r\n1 W 3\r\n2 P 1\r\n3 W 1\r\n2 F 3\r\n3 3\r\n1 W 2\r\n2 W 3\r\n3 W 1";

        protected override string Output => "3\r\n1 2 3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                List<(int, string, int)> Ws = [];
                var D = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int[] supps = new int[D[0]]; 
                j++;
                for(int k = 0; k < D[1]; k++)
                {
                    var s = input[j].Split(' ');
                    var p = (int.Parse(s[0]), s[1], int.Parse(s[2]));
                    Ws.Add(p);
                    if (s[1] == "W")
                    {
                        supps[p.Item3 - 1]++;
                    }
                    else 
                    {
                        int x = p.Item3 - 1;
                        bool supp = p.Item2 == "P";
                        while (Ws[x].Item2 != "W")
                        {
                            if (Ws[x].Item2 == "F")
                                supp = !supp;
                            x = Ws[x].Item3 - 1;
                        }
                        if(supp)
                        {
                            supps[Ws[x].Item3 - 1]++;
                        }
                        else
                        {
                            for(int k2 = 0; k2 < supps.Length; k2++)
                            {
                                if(k2 != Ws[x].Item3 - 1)
                                    supps[k2]++;
                            }
                        }
                    }
                    j++;
                }
                List<int> minLiars = [];
                int min = 0;
                for(int k = 0; k < D[0]; k++)
                {
                    if (supps[k] > min)
                    {
                        min = supps[k];
                        minLiars.Clear();
                    }
                    if (supps[k] >= min)
                        minLiars.Add(k + 1);
                }
                output.Add(string.Join(' ', minLiars));
            }
        }
    }
}
