
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OPSS
{
    /* 2/5
     * 
Dla danych n liczb znajdź największą możliwą liczbę będącą ich konkatenacją.
Wejście

Wejście składa się z liczby testów t (t < 1001). Pierwsza linia każdego testu zawiera liczbę n (0 < n ≤ 105) oznaczającą ilość liczb. Druga i ostatnia linia każdego testu zawiera n liczb z przedziału 0..2*109.
Wyjście

Dla każdego testu jedna liczba będąca maksymalną konkatenacją liczb z wejścia.
     */
    public sealed class Konkatenacja : ProblemBase
    {
        protected override string Input => "3\r\n3\r\n1 2 3\r\n5\r\n23 645 561 532 315\r\n6\r\n925 5 235 923 553 9\r\n";

        protected override string Output => "321\r\n64556153231523\r\n99259235553235";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 2; i < input.Length; i += 2)
            {
                var strings = input[i].Split(' ').ToList();
                strings.Sort((a, b) => 
                {
                    int len = Math.Min(a.Length, b.Length);
                    for(int j = 0; j < len; j++)
                    {
                        int c = a[j].CompareTo(b[j]);
                        if (c != 0)
                            return -c;
                    }
                    return a.Length.CompareTo(b.Length);
                });
                output.Add(string.Join("", strings));
            }
        }
    }
}
