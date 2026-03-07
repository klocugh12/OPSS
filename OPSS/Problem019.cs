namespace OPSS
{
    /* 4/5
     * Wieże Hanoi to tradycyjna zabawa-łamigłówka polegająca na nakładaniu krążków na słupki.
Dysponujemy n krążkami o średnicach 1,2, ...,n i trzema słupkami, które nazwiemy A, B i C. Każdy
krążek ma w środku dziurkę, która pozwala nałożyć krążek na słupek. Początkowo wszystkie krążki
znajdują się na słupku A i są ułożone począwszy od największego (na dole) do najmniejszego (na
górze). Zabawa polega na przeniesieniu wszystkich krążków na jeden z wolnych słupków
(powiedzmy B) zgodnie z następującymi zasadami:
● w jednym ruchu wolno nam wziąć jeden krążek leżący na górze na jednym ze słupków i
położyć go na górze na innym słupku;
● na każdym słupku zawsze musi być zachowany porządek, tzn. krążki muszą leżeć w
kolejności od największego (na dole słupka) do najmniejszego (na górze).
Krążki nałożone na jeden słupek nazwiemy wieżą. Podsumowując powyższe zasady, możemy
stwierdzić, że:
● nie jest możliwe wyciągnięcie krążka ze środka wieży lub włożenie krążka do środka wieży;
● nie wolno brać więcej niż jeden krążek na raz;
● nie wolno kłaść większego krążka na mniejszym.
Celem w tej zabawie jest przeniesienie wieży z jednego słupka na drugi w najmniejszej, możliwej
liczbie ruchów.
Dwubarwne wieże Hanoi, to nieco zmodyfikowana odmiana powyższej układanki. Jak poprzednio
mamy trzy słupki i n krążków o średnicach 1,2,...n. Tym razem jednak krążki o średnicach
nieparzystych (1,3,5,...) są białe, a krążki o średnicach parzystych (2,4,6,...) są czarne. Celem
zabawy jest przeniesienie (zgodnie z podanymi wyżej zasadami) wszystkich krążków białych na
słupek B, a krążków czarnych na słupek C.
Zadanie
Napisz program, który wyliczy minimalną liczbę ruchów potrzebnych do ułożenia krążków białych
na słupku B, a krążków czarnych na słupku C.
Wejście
Program powinien czytać dane z wejścia standardowego. W pierwszym wierszu podana jest liczba
n (0 ≤ n ≤ 1000) oznaczająca liczbę krążków.
Wyjście
Program powinien pisać wynik na wyjście standardowe. Wynikiem powinna być jedna liczba
oznaczająca minimalną liczbę ruchów potrzebnych do rozdzielenia białych i czarnych krążków.
     */
    public sealed class DwubarwneWiezeHanoi : ProblemBase
    {
        protected override string Input => "6";

        protected override string Output => "45";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            int x = n % 3;
            List<int> result = [x];
            List<int> mul5power = x == 0 ? [5] : [x, 0];
            while (x < n)
            {
                int offset = mul5power.Count - result.Count;
                int carry = 0;
                for (int k = result.Count - 1; k >= 0; k--)
                {
                    result[k] += mul5power[offset + k];
                    result[k] += carry;
                    carry = result[k] / 10;
                    result[k] %= 10;
                }
                for (int k = offset - 1; k >= 0; k--)
                {
                    result.Insert(0, mul5power[k] + carry);
                    carry = result[k] / 10;
                    result[k] %= 10;
                }
                carry = 0;
                for (int k = mul5power.Count - 1; k >= 0; k--)
                {
                    mul5power[k] <<= 3;
                    mul5power[k] += carry;
                    carry = mul5power[k] / 10;
                    mul5power[k] %= 10;
                }
                if (carry > 0)
                    mul5power.Insert(0, carry);
                x += 3;
            }
            output.Add(string.Join("", result));
        }
    }
}
