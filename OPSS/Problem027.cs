namespace OPSS
{
    /* Difficulty: 3/5
     * 
Masz do dyspozycji nieograniczoną ilość kamieni domina, z których każdy ma wymiary 2x1.
Twoim zadaniem będzie obliczenie, na ile różnych sposobów można za pomocą nierozróżnialnych
kamieni domina pokryć prostokąt o wymiarach 3xN.
Na rysunku pokazano wszystkie sposoby na jakie można pokryć dominem prostokąty 3x2 i 3x4.
Jest ich odpowiednio 3 i 11.
Rys. Wszystkie sposoby pokrycia prostokątów o wymiarach 3x2 i 3x4 kamieniami domina.
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 2000, oznaczająca liczbę
zestawów danych. Każdy zestaw danych składa się z jednego wiersza, zawierającego liczbę
naturalną N, 1 ≤ N ≤ 100000.
Wyjście
Dla każdego zestawu danych należy podać jeden wiersz wyniku zawierający liczbę P mod 106,
gdzie P jest liczbą sposobów na jakie można pokryć prostokąt o wymiarach 3xN.
     */
    public sealed class Domino : ProblemBase
    {
        protected override string Input => "2\r\n2\r\n4";

        protected override string Output => "3\r\n11";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int> dominos = [3];
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                if (a % 2 == 1)
                {
                    output.Add("0");
                    continue;
                }
                a >>= 1;
                for (int j = dominos.Count; j < a; j++)
                {
                    dominos.Add(0);
                    for (int k = 0; k < j; k++)
                    {
                        dominos[j] += (k == 0 ? 3 : 2) * dominos[j - k - 1];
                    }
                    dominos[j] += 2;
                    dominos[j] %= 1_000_000;
                }
                output.Add(dominos[a - 1].ToString());
            }
        }
    }
}
