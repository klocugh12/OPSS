namespace OPSS
{
    /* 2/5
     * 
Ludzie od wieków, jeszcze w czasach "przedkomputerowych", fascynowali się rachmistrzami.
Dotyczyło to w szczególności cudownych, najczęściej autystycznych dzieci, które często nie umiały
czytać i pisać, ale liczyły z zadziwiającą sprawnością. W szczególności w XIX wieku zostało
opisanych kilku takich wyjątkowych rachmistrzów egzaminowanych przez Francuską Akademię
Nauk. Jednym z nich był urodzony w 1826 roku, młody pasterz owiec Henri Mondeux. Zapytany,
jakie liczby podniesione do kwadratu maja różnicę równą 133 odpowiedział, że to 66 i 67 a po
chwili, że jest także inne rozwiązanie: 6 i 13. Dziś taka fascynacja rachmistrzami już bezpowrotnie
minęła, bo w erze powszechnej dostępności kalkulatorów i komputerów trudno by zgromadzić
publiczność, która chciałaby przyglądać się takim popisom, ale pozostała nadal ciekawość
odkrywania sposobów, dzięki którym takie skomplikowane operacje arytmetyczne można sprawnie
wykonać.
Spróbuj postawić się w położeniu małego Henri.
Zadanie
Należy znaleźć rozwiązanie równania a^2 - b^2 = c, gdzie c jest zadaną liczbą. Liczby a, b, c są
liczbami całkowitymi nieujemnymi.
W przypadku istnienia kilku różnych rozwiązań, tj. par (a, b) spełniających równanie, interesuje nas
to rozwiązanie dla którego różnica a-b jest najmniejsza. Jeżeli istnieje kilka rozwiązań dla których
różnica a-b jest taka sama, należy wybrać to, dla którego liczba b jest najmniejsza.
Wejście
W pierwszym wierszu wejścia podana jest liczba całkowita L, 1 ≤ L ≤ 60000, oznaczająca liczbę
zestawów danych. W kolejnych L wierszach występują wartości ci, 0 ≤ ci ≤ 5∙10^6.
Wyjście
Na wyjściu, w jednym wierszu dla każdej danej ci, należy wypisać jedną liczbę całkowitą bi,
spełniającą warunki opisane w zadaniu. Jeżeli rozwiązanie nie istnieje, należy wypisać liczbę -1.
     */
    public sealed class Rachmistrz : ProblemBase
    {
        protected override string Input => "2\r\n133\r\n28900";

        protected override string Output => "66\r\n7224";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                if (a == 0)
                    output.Add("0");
                else if (a % 2 == 1)
                    output.Add((a >> 1).ToString());
                else if (a % 4 == 2)
                    output.Add("-1");
                else
                    output.Add((((a >> 1) - 1) >> 1).ToString());
            }
        }
    }
}
