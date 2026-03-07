using System.Text;

namespace OPSS
{
    /* 3/5
     * Pszczoły, jak wiadomo, budują plastry miodu z przylegających do siebie sześciokątów foremnych.
Jednak, na jednej z pasiek, pszczoły zbudowały plaster w inny sposób. Plaster ma następującą
budowę:
● sześciokąty foremne stykają się ze sobą tylko wierzchołkami,
● przestrzeń między szesciokątami wypełniają trójkąty równoboczne,
● trójkąty i sześciakąty stykają się tylko wzdłuż swoich krawędzi,
● każdy trójkąt styka się krawędzią przynajmniej z jednym sześciokątem.
Zgodnie z najnowszymi unijnymi przepisami, miody wytwarzane na plastrach składających się z
innych figur niż sześciokąty, są objęte wyższą stawką podatku VAT.
Na szczęście pszczoła pilnująca poprawności plastra zorientowała się, że coś jest nie tak. Pszczoły
zastanawiają się, czy opłaca się przekształcać strukturę plastra, tak aby składał się z samych
sześciokątów. Sześciokąty foremne będą bezproblemowo przenoszone w inne miejsca plastra,
problemem są natomiast trójkąty. Podczas naprawy plastra, miód zawarty w trójkątach jest spisany
na straty. Pszczoły nie wiedzą czy zyski związane z objęciem miodu niższą stawką podatku VAT
zrekompensują straty miodu spowodowane naprawą struktury. Pomóż pszczołom stwierdzić, czy
opłaca się dostosowywać plaster do unijnych norm.
Pszczoły dysponują opisem kształtu plastra otrzymanym poprzez obejście plastra na około. Opis
składa się z ciągu kierunków wzdłuż których poruszano się obchodząc plaster po obwodzie.
Pszczoły budują plaster w taki sposób, że nigdy nie ma w nim dziur, dzięki czemu taki opis kształtu
plastra jest w pełni wystarczający i od dawna stosowany przez pszczoły. Obwód plastra jest łamaną
zamkniętą, a to znaczy, że mogą istnieć punkty do których pszczoła dotrze więcej niż jeden raz,
zanim obejdzie cały plaster. Dla zadanej zamkniętej trasy po której pszczoła obeszła plaster na
około, oblicz liczbę zawartych wewnątrz plastra trójkątów.
Rys. Struktura miodu którą stworzyły pszczoły wraz z przykładową trasą przejścia.
Wejście
W pierwszym wierszu wejścia znajduję się liczba całkowita C, 1 ≤ C ≤ 100, oznaczająca liczbę
zestawów danych testowych. W kolejnych wierszach znajdują się zestawy danych testowych. W
pierwszym i jedynym wierszu każdego zestawu danych znajduje się ciąg cyfr z zakresu 1..6
opisujący jednoznacznie zamkniętą trasę, którą musiała pokonać pszczoła obchodząc
nieprawidłowy plaster miodu. Pomiędzy cyframi znajdującymi się w jednym wierszu nie występują
inne znaki. Każdy wiersz wejścia zawiera co najwyżej 100000 cyfr.
Wyjście
W C wierszach wyjścia należy podać wyznaczoną dla każdego zestawu danych liczbę trójkątów
równobocznych (tylko tych najmniejszych, nie zawierających wewnątrz innych figur) zawartych w
plastrze miodu zbudowanym przez pszczoły z tej pasieki.
     */
    public sealed class PlasterMiodu : ProblemBase
    {
        protected override string Input => "2\r\n112611322312345345345555561\r\n123423456156";

        protected override string Output => "13\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            Dictionary<string, string> replacementsTri = new()
            {
                ["111"] = "1261",
                ["222"] = "2312",
                ["333"] = "3423",
                ["444"] = "4534",
                ["555"] = "5645",
                ["666"] = "6156",
                ["135"] = "",
                ["246"] = "",
                ["351"] = "",
                ["462"] = "",
                ["513"] = "",
                ["624"] = "",
                ["13"] = "2",
                ["24"] = "3",
                ["35"] = "4",
                ["46"] = "5",
                ["51"] = "6",
                ["62"] = "1"
            };
            Dictionary<string, string> replacementsHexFull = new()
            {
                ["123456"] = "",
                ["234561"] = "",
                ["345612"] = "",
                ["456123"] = "",
                ["561234"] = "",
                ["612345"] = ""
            };
            Dictionary<string, string> replacementsHexPartial = new()
            {
                ["12345"] = "3",
                ["23456"] = "4",
                ["34561"] = "5",
                ["45612"] = "6",
                ["56123"] = "1",
                ["61234"] = "2",
                ["1234"] = "32",
                ["2345"] = "43",
                ["3456"] = "54",
                ["4561"] = "65",
                ["5612"] = "16",
                ["6123"] = "21"
            };
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                StringBuilder sb = new(input[i]);
                int c = 0;
                while (sb.Length > 2)
                {
                    string key, newRep;
                    for (int k = 0; k < sb.Length - 5; k++)
                    {
                        key = sb.ToString().Substring(k, 6);
                        if (replacementsHexFull.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                    }
                    for (int k = 0; k < sb.Length - 4; k++)
                    {
                        key = sb.ToString().Substring(k, 5);
                        if (replacementsHexPartial.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                        else
                        {
                            key = key.Substring(0, 4);
                            if (replacementsHexPartial.TryGetValue(key, out newRep))
                                sb.Replace(key, newRep);
                        }
                    }
                    if (sb.Length >= 4)
                    {
                        key = sb.ToString().Substring(sb.Length - 4, 4);
                        if (replacementsHexPartial.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                    }
                    for (int k = 0; k < sb.Length - 3; k++)
                    {
                        key = sb.ToString().Substring(k, 3);
                        if (replacementsTri.TryGetValue(key, out newRep))
                        {
                            int n = sb.Length;
                            sb.Replace(key, newRep);
                            c += (n - sb.Length) / (key.Length - newRep.Length);
                        }
                        else
                        {
                            key = key.Substring(0, 2);
                            if (replacementsTri.TryGetValue(key, out newRep))
                            {
                                int n = sb.Length;
                                sb.Replace(key, newRep);
                                c += (n - sb.Length) / (key.Length - newRep.Length);
                            }
                        }
                    }
                    if (sb.Length >= 2)
                    {
                        key = sb.ToString().Substring(sb.Length - 2, 2);
                        if (replacementsTri.TryGetValue(key, out newRep))
                        {
                            int n = sb.Length;
                            sb.Replace(key, newRep);
                            c += (n - sb.Length) / (key.Length - newRep.Length);
                        }
                    }
                }
                output.Add(c.ToString());
            }
        }
    }
}
