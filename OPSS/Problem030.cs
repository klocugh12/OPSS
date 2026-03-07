namespace OPSS
{
    /* 4/5
     * Małgosia dostała na Mikołaja klocki z wypisanymi literami alfabetu. Układanie słów z klocków
było pasjonującą rozrywką. W trakcie zabawy odkryła, że niektóre słowa czytane od lewej do
prawej i odwrotnie wyglądają tak samo. Opowiedziała tacie o swoich odkryciach. Ojciec wyjaśnił
córce, że z dowolnego słowa można usunąć pewną liczbę liter (nawet równą zeru) i otrzymać
niepuste słowo, które wygląda tak samo, gdy jest czytane od prawej do lewej i odwrotnie. Małgosia,
po chwili zastanowienia, spytała, na ile sposobów da się to zrobić. Zafrasowany rodzic, jak
przystało na programistę firmy MIRACLE, postanowił napisać program, który rozwiąże ten
problem. Niestety, z powodu nawału pracy nad modułem sortującym nowej bazy danych zadanie to
zlecił Tobie. Dodał jeszcze, że słowo musi zawierać co najmniej jedną literę i dwa sposoby
usuwania liter są różne, jeśli usuwamy litery na różnych pozycjach (nie jest ważna kolejność
usuwania). Np. ze słowa AAA możemy usunąć literę na pozycji 1, 2 lub 3 (trzy różne sposoby) i
otrzymamy trzy słowa AA (*AA, A*A, AA*).
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, 1 ≤ C ≤ 100 , oznaczająca liczbę zestawów
danych. W kolejnych C liniach znajdują sie zestawy danych. Każdy zestaw danych to opis słowa -
składa się z dużych liter alfabetu angielskiego, o długości L, 1 ≤ L ≤ 50.
Wyjście
Na wyjściu, dla każdego słowa wejściowego, trzeba podać liczbę sposobów usuwania liter, tak by
otrzymane słowa czytane od lewej do prawej, wyglądały tak samo, jak czytane od prawej do lewej.
     */
    public sealed class Mikolaj : ProblemBase
    {
        protected override string Input => "4\r\nX\r\nXX\r\nALA\r\nAAA";

        protected override string Output => "1\r\n3\r\n5\r\n7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j;
            for(int i = 1; i <= N; i++)
            {
                Dictionary<char, List<int>> occurences = [];
                for(j = 0; j < input[i].Length; j++)
                {
                    if (!occurences.ContainsKey(input[i][j]))
                        occurences.Add(input[i][j], []);
                    occurences[input[i][j]].Add(j);
                }
                if (input[i].Length == 1)
                {
                    output.Add("1");
                    continue;
                }
                else if (input[i].Length == 2)
                {
                    output.Add(input[i][0] == input[i][1] ? "3" : "2");
                    continue;
                }
                int count = input[i].Length + (input[i].Length > 1 ? 1 : 0);
                List<(int, int, int, int)> pal2 = [];
                foreach(var oc in occurences.Keys.Where(k => occurences[k].Count > 1))
                {
                    for(j = 0; j < occurences[oc].Count; j++)
                    {
                        for (int k = j + 1; k < occurences[oc].Count; k++)
                        {
                            pal2.Add((occurences[oc][j], occurences[oc][j], occurences[oc][k], occurences[oc][k]));
                            count++;
                        }
                    }
                }
                j = 3;
                while(j < input[i].Length && pal2.Count > 0)
                {
                    if(j % 2 == 1)
                        foreach(var pal in pal2)
                        {
                            count += (pal.Item3 - pal.Item2 - 1);
                        }
                    else
                    {
                        List<(int, int, int, int)> newPal2 = [];
                        for (int k = 0; k < pal2.Count; k++)
                        {
                            for(int l = k + 1; l < pal2.Count; l++)
                            {
                                if (pal2[k].Item2 < pal2[l].Item1 && pal2[k].Item3 > pal2[l].Item4)
                                {
                                    if(pal2[l].Item3 - pal2[l].Item2 > 1)
                                        newPal2.Add((pal2[k].Item1, pal2[l].Item2, pal2[l].Item3, pal2[k].Item4));
                                    count++;
                                }
                                else if(pal2[l].Item2 < pal2[k].Item1 && pal2[l].Item3 > pal2[k].Item4)
                                {
                                    if(pal2[k].Item3 - pal2[k].Item2 > 1)
                                        newPal2.Add((pal2[l].Item1, pal2[k].Item2, pal2[k].Item3, pal2[l].Item4));
                                    count++;
                                }
                            }
                        }
                        pal2 = newPal2;
                    }
                    j++;
                }
                output.Add(count.ToString());
            }
        }
    }
}
