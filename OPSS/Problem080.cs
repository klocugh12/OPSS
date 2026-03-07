namespace OPSS
{
    /* 4/5
     * Jest rok 3749. Rasa ludzka wymyśliła sztuczny mechanizm obronny przeciwko wirusom
infekującym genotyp.
Genotyp to łańcuch genów. Każdy gen jest kodowany w postaci dwuznaku składającego się z liter
'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'. Zdrowy genotyp złożony jest z genów o takim samym kodzie
genetycznym P. Na przykład, jeśli P jest genem o kodzie 'AA', to zdrowy genotyp o długości 3 jest
postaci 'AA AA AA'. Wirus atakujący genotyp może zmienić kod jednego lub więcej genów
zdrowego genotypu na inny kod, różny od P. Chory gen nie może być już ponownie zmieniony
przez wirusa. Wszystkie geny ze zmienionym kodem stanowią genotyp wirusa. Przykładowo,
zainfekowany genotyp z poprzedniego przykładu może wyglądać następująco 'AB AC AA'
(genotyp wirusa 'AB AC'). Każde X kolejnych genów genotypu wirusa tworzy bakterię o długości
X. Bakterie o tej samej długości nie mają wspólnych genów. W genotypie wirusa mogą kryć się
bakterie różnych długości. Na przykład, genotyp wirusa 'AB AC' zawiera dwie bakterie o długości
1: 'AB','AC' i jedną o długości 2: 'AB AC'.
Jedyną znaną bronią przeciwko wirusowi są pikoboty. Pikobot jest robotem o bardzo małych
rozmiarach, którego zadaniem jest przywrócenie chorym genom pierwotnego kodu genetycznego P.
Pikobot działa według programu, który ma postać sekwencji kodów genetycznych.
Pikobot jest "wstrzykiwany" do zainfekowanego genotypu i zaczynając od pierwszego genu
genotypu wirusa, porównuje ciąg genów genotypu wirusa z sekwencją genów zapisaną w swoim
programie. Jeśli oba ciągi są identyczne, wówczas pikobot przywraca genom w ciągu ich pierwotną
postać (niszczy bakterię) i przechodzi do kolejnej sekwencji. W przeciwnym razie pikobot
przechodzi do kolejnej sekwencji genów w genotypie wirusa (przesuwa się o liczbę genów równą
długości swojego programu).
Natychmiast po analizie ostatniego genu pikobot wysyła do głównej bazy danych liczbę
zniszczonych przez siebie bakterii. Zniszczenie bakterii następuje wtedy, gdy wszystkie geny
bakterii zostaną wyleczone. Pikobot, którego program ma długość X może niszczyć tylko bakterie o
długości X.
Zadanie
Znając kod genetyczny genu pierwotnego P, postać genotypu zainfekowanego, dlugości programów
pikobota (równe długościom bakterii) i wiedząc, że długość genotypu wirusa nie przekracza
100000, znaleźć program najlepszego pikobota, czyli takiego, który wyleczy największą część
"chorego" genotypu. Jeżeli wybór jest niejednoznaczny, należy spośród wcześniej wybranych
pikobotów, wybrać tego, który zniszczył najwięcej bakterii. Jeśli to nie wyłoni zwycięzcy, wówczas
zamiast programu należy podać 0. Ponadto jeżeli długość zainfekowanego genotypu nie przekracza
10000 lekarze chcieliby wiedzieć, na ile różnych sposobów wirus może zmienić zdrowy genotyp,
wykorzystując kody genetyczne wchodzące w skład genotypu wirusa.
Wejście
W pierwszej linii wejścia znajduje się liczba 0< N ≤1000000, określająca długość zainfekowanego
genotypu oraz po pojedynczej spacji kod genetyczny genu pierwotnego P. W drugim wierszu
znajduje się N kodów genetycznych oddzielonych pojedynczą spacją, które stanowią opis
zmienionego genotypu. Liczba 0< D≤ 40 w kolejnym wierszu określa ilość długości programów
pikobota (równym długościom bakterii). W ostatnim wierszu wejścia znajdują się liczby 1≤ di≤ 40,
gdzie 0< i≤ D opisujące długości bakterii.
Wyjście
W pierwszej linii wyjścia powinien zostać wypisany program najlepszego pikobota. Jeżeli długość
genotypu nie przekracza 10000 w kolejnym wierszu należy wypisać liczbę różnych sposobów
modyfikacji zdrowego genotypu (co najmniej jeden gen musi się różnić) przez wirusa. W
przypadku, gdy liczba ta będzie miała więcej niż 50 cyfr wystarczy wypisać 50 ostatnich.
     */
    public sealed class Pikoboty : ProblemBase
    {
        protected override string Input => "7 AA\r\nBC BC FD GF FG FD FD\r\n3\r\n1 2 3";

        protected override string Output => "FD\r\n78124";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string ok = input[0].Split(' ')[1];
            var unhealthy = input[1].Split(' ').ToArray();
            Dictionary<string, int> counts = [];
            int[] values = input[3].Split(' ').Select(c => int.Parse(c)).ToArray();
            foreach (var v in values)
            {
                string healthy = string.Join("", Enumerable.Range(0, v).Select(s => ok));
                for (int i = 0; i + v <= unhealthy.Length; i++)
                {
                    string key = string.Join("", Enumerable.Range(i, v).Select(s => unhealthy[s]));
                    if (key == healthy)
                        continue;
                    if (!counts.ContainsKey(key))
                        counts.Add(key, 1);
                    else
                        counts[key]++;
                }
            }
            List<(string, int, int)> results = [];
            foreach(var k in counts.Keys)
            {
                results.Add((k, counts[k], counts[k] * (k.Length >> 1)));
            }
            results.Sort((a, b) =>
            {
                var c = -a.Item3.CompareTo(b.Item3);
                return c == 0 ? -a.Item2.CompareTo(b.Item2) : c;
            });
            output.Add(results.Count == 1 || results[0].Item3 > results[1].Item3 || results[0].Item2 > results[1].Item2 
                ? results[0].Item1 : "0");
            if(unhealthy.Length <= 10000)
            {
                int min = values.Min();
                int counter = 0;
                for(int k = 0; k <= unhealthy.Length - min; k++)
                {
                    if(Enumerable.Range(0, min).Any(j => unhealthy[k + j] != ok))
                        counter++;
                }
                List<int> result = [counts.Keys.Count(c => c.Length == 2) + 1];
                while(result[0] > 9)
                {
                    result = [result[0] / 10, result[0] % 10];
                }
                List<int> factor = new(result);
                for(int j = 0; j < min * counter - 1; j++)
                {
                    List<int> newRes = [0];
                    int carry = 0;
                    for(int k = result.Count - 1; k >= 0; k--)
                    {
                        for (int l = factor.Count - 1; l >= 0; l--)
                        {
                            int index = k - (factor.Count - l - 1);
                            while (newRes.Count <= index)
                                newRes.Insert(0, 0);
                            newRes[index] += result[k] * factor[l] + carry;
                            carry = newRes[index] / 10;
                            newRes[index] %= 10;
                        }
                        int index2 = k - factor.Count;
                        while (carry > 0)
                        {
                            if (index2 >= 0)
                            {
                                newRes[index2] += carry;
                                carry = newRes[index2] / 10;
                                newRes[index2] %= 10;
                                index2--;
                            }
                            else
                            {
                                newRes.Insert(0, carry);
                                carry = 0;
                            }
                        }
                    }
                    result = newRes;
                }
                int x = result.Count - 1;
                result[x]--;
                while (x >= 0 && result[x] < 0)
                {
                    result[x] += 10;
                    result[x - 1]--;
                    x--;
                }
                if (result[0] == 0)
                    result.RemoveAt(0);
                output.Add(string.Join("", result.Take(50)));
            }
        }
    }
}
