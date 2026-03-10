namespace OPSS
{
    /* Difficulty: 2/5
     * Waga binarna to specyficzne urządzenie, które może dokonywać pomiarów dowolnych wielkości z
przedziału (0,1) z ustaloną dokładnością. Dokładność wagi ustala się pokrętłem, które można
ustawić na pozycji 1 lub 2, lub 3, lub ..., lub 10. Gdy dokładność jest ustawiona na m, to waga
dokonuje pomiarów z dokładnością do 1/2^m. Wyniki pomiarów wagi są zapisywane w postaci par
(l,m). Taka para oznacza, że dokładność wagi jest ustawiona na m i wskazanie wagi wynosi l, czyli
ciężar ważonego przedmiotu wynosi l/2^m (l jest liczbą naturalną i oczywiście 0 < l < 2^m, gdyż
wspominaliśmy, że waga wskazuje wielkości z przedziału (0,1)).
Zadanie
Twoim zadaniem jest napisanie programu, który uporządkuje wyniki pomiarów od najmniejszych
do największych. Wyniki pomiarów zadane są w postaci par (l,m). Różne pary oznaczające takie
same wyniki (np. (1,2) i (2,3) należy uporządkować rosnąco według wskazań, czyli pierwszych
elementów w parach.
Wejście
Program powinien czytać dane z wejścia standardowego. W pierwszym wierszu danych podana jest
liczba n (1 ≤ n ≤ 20000) oznaczająca liczbę par. W kolejnych n wierszach podane są pary liczb li i
mi, po jednej parze w wierszu; li i mi są oddzielone jedną spacją. Dla każdej pary spełnione są
warunki: 1 ≤ mi ≤ 10 oraz 0 < l < 2^mi.
Wyjście
Program powinien pisać wynik na wyjście standardowe. Wynikiem powinno być n par liczb
podanych na wejściu, ale w takiej kolejności, by pary odpowiadające mniejszym wartościom
pomiarów występowały przed parami odpowiadającymi większym wartościom. Takie same
pomiary należy zapisać niemalejąco według wskazań. Każdą parę należy zapisać w takiej samej
postaci, w jakiej była podana na wejściu.
     */
    public sealed class WagaBinarna : ProblemBase
    {
        protected override string Input => "4\r\n1000 10\r\n3 10\r\n5 3\r\n250 8";

        protected override string Output => "3 10\r\n5 3\r\n250 8\r\n1000 10";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int[]> result = [];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int[] newSplit = [int.Parse(splits[0]), int.Parse(splits[1]), 0];
                newSplit[2] = newSplit[0] << (10 - newSplit[1]);
                result.Add(newSplit);
            }
            result.Sort((a, b) => a[2] == b[2] ? a[0].CompareTo(b[0]) : a[2].CompareTo(b[2]));
            output.AddRange(result.Select(r => $"{r[0]} {r[1]}"));
        }
    }
}
