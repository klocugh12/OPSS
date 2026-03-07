using System.Globalization;

namespace OPSS
{
    /* 2/5
     * Niegdyś w Opsslandii do handlu służył wydzielony teren na otwartej przestrzeni. Dokonywano tam
transakcji kupna i sprzedaży pewnych rzeczy (dóbr) wartościowych. Takie targowisko umożliwiało
swobodny, bezpośredni kontakt handlowca z klientem i możliwość negocjowania ceny.
Wraz z upływem czasu oraz rozwojem Opsslandii targowisko zmieniło się w pełni
skomputeryzowaną Giełdę Rzeczy Wartościowych.
Rządzi się ona następującymi regułami:
● Na Giełdzie Rzeczy Wartościowych można kupować lub sprzedawać wartościowe rzeczy.
Odbywa się to poprzez składanie zleceń kupna bądź sprzedaży, z podanym limitem ceny.
Osoba składająca zlecenie musi określić:
○ symbol (identyfikator) rzeczy wartościowej, którą chce kupić/sprzedać
○ rodzaj oferty (kupno lub sprzedaż)
○ liczbę rzeczy wartościowych
○ limit ceny
● Kupujący godzi się na kupno określonej rzeczy tylko po cenie, która jest nie większa od
zadanego limitu ceny zlecenia kupna, zaś sprzedający godzi się na sprzedaż tylko po cenie
nie mniejszej od zadanego limitu ceny zlecenia sprzedaży.
● Pomiędzy dwoma osobami (z których jedna złożyła zlecenie kupna, a druga sprzedaży)
może dojść do realizacji transakcji kupna-sprzedaży po cenie na jaką się godzą.
● Jeśli kupujący kupi od sprzedającego mniejszą liczbę rzeczy od zadeklarowanej w swoim
zleceniu, jego zlecenie zostaje zrealizowane częściowo (liczba rzeczy do kupna na zleceniu
jest zmniejszana o liczbę rzeczy, które zostały faktycznie kupione). Jeśli kupi dokładnie taką
liczbę rzeczy, jaką zadeklarował, jego zlecenie zostaje zrealizowane w całości. Analogicznie
jeśli sprzedający nie sprzeda wszystkich rzeczy w jednej transakcji, jego zlecenie sprzedaży
zostaje zrealizowane częściowo (liczba rzeczy do sprzedaży na zleceniu jest zmniejszana o
liczbę rzeczy, które zostały sprzedane), jeśli sprzeda dokładnie taką liczbę rzeczy, jaką
zadeklarował, jego zlecenie zostaje zrealizowane w całości.
● Zlecenia na daną rzecz umieszczane są w arkuszu zleceń dotyczącym tej rzeczy. Arkusz
zleceń składa się z dwóch części. Lewa dotyczy zleceń kupna, prawa sprzedaży. Zlecenia
ułożone są według kolejności ich ewentualnej realizacji, tzn. po stronie kupna od
najwyższego do najniższego limitu ceny, po stronie sprzedaży odwrotnie. W ramach tego
samego limitu zlecenia ułożone są według czasu wprowadzenia do systemu giełdowego -
zlecenia wprowadzone wcześniej znajdują się powyżej zleceń wprowadzonych później.
Dzięki takiemu układowi najwyższy wiersz arkusza prezentuje zawsze zlecenia posiadające
najlepszy limit kupna i sprzedaży. Pozycja zlecenia w arkuszu decyduje o kolejności jego
realizacji.
● Po złożeniu zlecenia system umieszcza je w arkuszu zleceń i przystępuje do realizacji
wszystkich możliwych transakcji (w kolejności wynikającej z arkusza zleceń). W przypadku
złożenia zlecenia kupna transakcje wykonywane są po cenie zleceń sprzedaży. Natomiast w
przypadku złożenia zlecenia sprzedaży transakcje dokonywane są po cenie zleceń kupna.
Zlecenia zrealizowane w całości są usuwane z arkusza zleceń.
● Wolumen dla danej rzeczy jest to suma ilości rzeczy występująca we wszystkich
dokonanych transakcjach.
● Aktualny kurs danej rzeczy jest to cena, po której dokonana została ostatnia transakcja dla
tej rzeczy.
Przykład:
Jeśli arkusz zleceń dla rzeczy "A" zawiera zlecenia:
strona kupna strona sprzedaży
ilość limit ceny ilość limit ceny
200 10.50 300 11.10
100 10.00 200 11.20
i zostanie złożone zlecenie sprzedaży 250 sztuk rzeczy "A" z limitem ceny 10.00 to zostaną
wykonane 2 transakcje: sprzedaż-kupno "A": 200 sztuk po 10.50 oraz 50 sztuk po 10.00 (złożone
zlecenie zostanie zrealizowane w całości, zlecenie kupna 200 sztuk po 10.50 zostanie zrealizowane
w całości, zlecenie kupna 100 sztuk po 10.00 zostanie zrealizowane częściowo). Po zrealizowaniu
transakcji arkusz będzie zawierał zlecenia:
strona kupna strona sprzedaży
ilość limit ceny ilość limit ceny
50 10 300 11.10
200 11.20
Aktualny kurs "A" to 10.00 (kurs ostatniej transakcji), aktualny wolumen to 250 sztuk.
Jeśli teraz zostanie złożone zlecenie kupna 310 sztuk rzeczy "A" z limitem ceny 11.10 zostanie
wykonana 1 transakcja - 300 sztuk w cenie 11.10 a arkusz zleceń będzie zawierał:
strona kupna strona sprzedaży
ilość limit ceny ilość limit ceny
10 11.10 200 11.20
50 10
Aktualny kurs "A" to 11.10 (kurs ostatniej transakcji), aktualny wolumen to 550 sztuk.
Zadanie
Twoim zadaniem jest napisanie oprogramowania dla opsslandzkiej Giełdy Rzeczy Wartościowych.
Program na podstawie składanych zleceń powinien wyznaczyć aktualny wolumen oraz kurs dla
wszystkich rzeczy dostępnych na Giełdzie.
Wejście
Pierwsza linia wejścia zawiera liczbę N określającą liczbę złożonych zleceń (1 ≤ N ≤ 100000).
Kolejne N wierszy zawiera opisy kolejno składanych zleceń. Jedno zlecenie składa się z jednego
wiersza zawierającego oddzielone od siebie pojedynczą spacją: symbol rzeczy (jedna wielka litera
alfabetu angielskiego), typ zlecenia (wielka litera K lub S, oznaczająca odpowiednio zlecenie kupna
lub sprzedaży), liczbę naturalną L określającą liczbę rzeczy wartościowych (1 ≤ L ≤ 10000) oraz
limit ceny C (0 < C ≤ 10000). Zapis liczby C zawiera separator dziesiętny (kropkę), co najmniej
jedną cyfrę przed separatorem oraz dokładnie dwie cyfry po separatorze.
Wyjście
W pierwszej linii wyjścia należy wypisać liczbę X określającą liczbę rzeczy, dla których doszło do
przynajmniej jednej transakcji kupna-sprzedaży na Giełdzie. Dla każdej z tych X rzeczy, w
oddzielnych liniach, należy wypisać: literę określającą symbol rzeczy, wolumen oraz aktualny kurs
(oddzielone od siebie spacjami). Linie te powinny być wypisane w porządku alfabetycznym
względem symbolu rzeczy. Kursy należy wypisać w takim samym formacie, w jakim podane są na
wejściu, czyli z kropką, jako separatorem dziesiętnym, co najmniej jedną cyfrą przed separatorem
oraz dokładnie dwoma cyframi po separatorze.
     */
    public sealed class GieldaRzeczyWartosciowych : ProblemBase
    {
        protected override string Input => "8\r\nA K 100 10.00\r\nA K 200 10.50\r\nA S 300 11.10\r\nA S 200 11.20\r\nA S 250 10.00\r\nA K 310 11.10\r\nB K 100 10.00\r\nB S 50 10.00";

        protected override string Output => "2\r\nA 550 11.10\r\nB 50 10.00";

        static (int, double) ManageSales(List<(int, double)> buys, List<(int, double)> sales)
        {
            (int, double) result = (0, 0.0);
            while (buys.Count > 0 && sales.Count > 0 && buys[0].Item2 >= sales[0].Item2)
            {
                if (buys[0].Item1 > sales[0].Item1)
                {
                    result = (result.Item1 + sales[0].Item1, sales[0].Item2);
                    buys[0] = (buys[0].Item1 - sales[0].Item1, buys[0].Item2);
                    sales.RemoveAt(0);
                }
                else
                {
                    result = (result.Item1 + buys[0].Item1, sales[0].Item2);
                    sales[0] = (sales[0].Item1 - buys[0].Item1, sales[0].Item2);
                    buys.RemoveAt(0);                    
                }
            }
            return result;
        }

        static void Insert(List<(int, double)> collection, (int, double) value, bool ascending)
        {
            if(collection.Count == 0)
            {
                collection.Add(value);
                return;
            }
            int a = 0, b = collection.Count;
            while(a != b)
            {
                int c = (a + b) >> 1;
                if (ascending ^ collection[c].Item2 > value.Item2)
                {
                    a = c + 1;
                }
                else
                {
                    b = c;
                }
            }
            collection.Insert(a, value);
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            Dictionary<string, List<(int, double)>> buys = [], sales = [];
            Dictionary<string, (int, double)> results = [];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                if (!buys.ContainsKey(splits[0]))
                    buys.Add(splits[0], []);
                if (!sales.ContainsKey(splits[0]))
                    sales.Add(splits[0], []);
                if (splits[1] == "K") 
                    Insert(buys[splits[0]], (int.Parse(splits[2]), double.Parse(splits[3], CultureInfo.InvariantCulture)), false);                  
                else
                    Insert(sales[splits[0]], (int.Parse(splits[2]), double.Parse(splits[3], CultureInfo.InvariantCulture)), true);
                if (buys.Count > 0 && sales.Count > 0)
                {
                    var newResult = ManageSales(buys[splits[0]], sales[splits[0]]);
                    if (newResult.Item1 > 0)
                    {
                        if (!results.ContainsKey(splits[0]))
                            results.Add(splits[0], newResult);
                        else
                            results[splits[0]] = (newResult.Item1 + results[splits[0]].Item1, newResult.Item2);
                    }
                }
            }
            output.Add(results.Count.ToString());
            output.AddRange(results.Select(r => $"{r.Key} {r.Value.Item1} {r.Value.Item2.ToString("#.00", CultureInfo.InvariantCulture)}"));
        }
    }
}
