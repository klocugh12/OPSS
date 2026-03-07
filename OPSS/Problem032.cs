namespace OPSS
{
    /* 4/5
     * Zenek prowadzi niewielkie kasyno. Główną atrakcją w jego kasynie, jest "Binarne Bingo dla
dwojga" - gra którą sam wymyślił (przynajmniej tak mu się wydaje), a w której dwóch graczy
oczekuje na pewien ciąg wyników rzutów monetą.
Dokładniej: Każdy z graczy posiada wzorzec składający się z liter "O", "R", oznaczających
odpowiednio orła i reszkę. Krupier rzuca monetą zapisując kolejne wyniki, a pierwsze wystąpienie
wzorca oznacza wygraną posiadacza wzorca. Wystąpienie wzorca o długości L następuje, gdy
ostatnie L zapisanych przez krupiera wyników, pokrywa się ze wzorcem. Czasami może zdarzyć się
remis - gdy obaj gracze w tym samym momencie stwierdzą uzgodnienie wzorca. Wtedy gra jest
powtarzana od początku.
Zenek chciałby zalegalizować swoje kasyno. Poszedł więc do odpowiedniego urzędu, i otrzymał do
wypełnienia wiele formularzy. Okazało się, że aby zarejestrować "Binarne Bingo ..." musi podać w
formularzu, dla każdej zadanej pary wzorców, który wzorzec ma większe szanse wygranej. Cała
atrakcja gry Zenka, wynikała przecież z tego, że gracze mogą wybierać wzorce z ogromnej liczby
dostępnych wzorców. Zenek nie jest w stanie poradzić sobie z wymaganiami urzędników i oczekuje
Twojej pomocy. Pomóż Zenkowi zalegalizować kasyno.
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 200, oznaczająca liczbę
zestawów danych. W kolejnych wierszach znajdują się zestawy danych, po jednym zestawie w
wierszu. Zestaw danych składa się z 2 niepustych wzorców, wybranych przez obu graczy,
oddzielonych pojedynczą spacją. Wzorzec jest ciągiem znaków, którego elementami mogą być
wyłącznie duże litery "O" lub "R". Długość wzorca nie przekracza 30 znaków.
Wyjście
W C wierszach wyjścia należy umieścić odpowiedzi dla poszczególnych zestawów danych, przy
czym odpowiedzią jest dokładnie jedna liczba:
● 1 - jeśli wzorzec pierwszy daje większe szanse na wygraną,
● 2 - jeśli wzorzec drugi daje większe szanse na wygraną,
● 0 - jeśli oba wzorce dają takie same szanse na wygraną.
     */
    public sealed class BinarneBingo : ProblemBase
    {
        protected override string Input => "6\r\nOORO OROO\r\nOROO ROOO\r\nOORO ROO\r\nROR ORO\r\nORO RO\r\nRO RORO";

        protected override string Output => "1\r\n1\r\n2\r\n0\r\n2\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                string[] tab = input[i].Split(' ');

                if (tab[0].Length != tab[1].Length)
                {
                    output.Add(tab[0].Length < tab[1].Length ? "1" : "2");
                }
                else
                {
                    int n1 = 0, n2 = 0;
                    List<int[]> lcs = [];
                    while (n1 < tab[0].Length && n2 < tab[1].Length)
                    {
                        if (tab[0][n1] != tab[1][n2])
                            n1++;
                        int k = 0;
                        while (n1 + k < tab[0].Length && n2 + k < tab[1].Length && tab[0][n1 + k] == tab[1][n2 + k])
                        {
                            k++;
                        }
                        if (k > 1)
                        {
                            lcs.Add([n1, n2, k]);
                        }
                        n1 += k;
                        n2 += k;
                    }
                    n1 = 0;
                    n2 = 0;
                    while (n1 < tab[0].Length && n2 < tab[1].Length)
                    {
                        if (tab[0][n1] != tab[1][n2])
                            n2++;
                        int k = 0;
                        while (n1 + k < tab[0].Length && n2 + k < tab[1].Length && tab[0][n1 + k] == tab[1][n2 + k])
                        {
                            k++;
                        }
                        if (k > 1)
                        {
                            lcs.Add([n1, n2, k]);
                        }
                        n1 += k;
                        n2 += k;
                    }
                    for (int k = 0; k < lcs.Count; k++)
                    {
                        n1 = lcs[k][0];
                        n2 = lcs[k][1];
                        while (n1 > 0 && n2 > 0 && tab[0][n1 - 1] == tab[1][n2 - 1])
                        {
                            n1--;
                            n2--;
                        }
                        int delta = lcs[k][0] - n1;
                        if (delta > 0)
                        {
                            lcs[k][0] -= delta;
                            lcs[k][1] -= delta;
                            lcs[k][2] += delta;
                        }
                    }
                    lcs.Sort((a, b) => -a[2].CompareTo(b[2]));
                    lcs.RemoveAll(x => x[2] < lcs[0][2]);
                    if (lcs.Count == 1)
                    {
                        int before1 = lcs[0][0], before2 = lcs[0][1],
                            after1 = tab[0].Length - (lcs[0][0] + lcs[0][2]), after2 = tab[1].Length - (lcs[0][1] + lcs[0][2]);
                        n1 = after1 + Math.Max(before1 - 1, 0) + tab[0].Length;
                        n2 = after2 + Math.Max(before2 - 1, 0) + tab[1].Length;
                        output.Add(n2 == n1 ? "0" : n1 < n2 ? "1" : "2");
                    }
                    else
                        output.Add("0");
                }
            }
        }
    }
}
