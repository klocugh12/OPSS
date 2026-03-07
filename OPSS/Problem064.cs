namespace OPSS
{
    /* 4/5
     * Zadanie
Dla zadanych liczb całkowitych x, y, a, należy wyznaczyć, ile jest liczb całkowitych pomiędzy x a y
(włącznie), których suma cyfr wynosi a.
Wejście
W pierwszej linii wejścia znajduje się liczba C, określająca liczbę zestawów danych, 1 ≤ C ≤ 1000.
W kolejnych wierszach wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa
się z wiersza zawierającego liczby całkowite x, y, a (0 ≤ x ≤ y < 2^31, 0 ≤ a ≤ 100), oddzielone
pojedyncza spacją.
Wyjście
Dla każdego zestawu danych, w osobnej linii wyjścia, należy wypisać liczbę, określającą ile jest
liczb całkowitych pomiędzy x a y, których suma cyfr jest równa a.
     */
    public sealed class SumaCyfr : ProblemBase
    {
        protected override string Input => "3\r\n1 100 11\r\n90 2400 14\r\n6502 68020 16";

        protected override string Output => "8\r\n180\r\n2807";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int>[] sums = new List<int>[10];
            sums[0] = Enumerable.Range(0, 10).Select(s => 1).ToList();
            sums[1] = Enumerable.Range(0, 19).Select(s => Math.Min(s, 19 - s)).ToList();
            for (int i = 2; i < sums.Length; i++)
            {
                sums[i] = [];
                for (int j = 0; j < sums[i - 1].Count + 9; j++)
                {
                    sums[i].Add(0);
                    int k = Math.Max(1, j - sums[i - 1].Count + 1);
                    while (k <= 9 && j - k > 0)
                    {
                        int a = i - 1;
                        while (a >= 0 && j - k < sums[a].Count)
                        {
                            sums[i][j] += sums[a][j - k];
                            a--;
                        }
                        k++;
                    }
                    if (j > 0 && j < 10)
                        sums[i][j]++;
                }
            }
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                var a = splits[0].Select(s => s - '0').ToList();
                var b = splits[1].Select(s => s - '0').ToList();
                int c = int.Parse(splits[2]);
                int sum = 0;
                int j = 0;
                int c2 = c;
                while(j < a.Count - 1)
                {
                    for(int k = a[j] + 1; k <= Math.Min(c2, 9); k++)
                    {
                        int l = a.Count - 2 - j;
                        while (l >= 0 && sums[l].Count > c2 - k)
                        {
                            sum += sums[l][c2 - k];
                            l--;
                        }
                    }
                    c2 -= a[j];
                    j++;
                }
                if (c2 >= a[a.Count - 1] && c2 < 10)
                    sum++;
                for(j = a.Count + 1; j < b.Count; j++)
                {
                    sum += sums[j - 1][c];
                }
                j = 0;
                c2 = c;
                while (j < b.Count - 1)
                {
                    for (int k = b[j] - 1; k >= (j == 0 ? 1 : 0) && k <= c2; k--)
                    {
                        int l = b.Count - 2 - j;
                        while (l >= 0 && sums[l].Count > c2 - k)
                        {
                            sum += sums[l][c2 - k];
                            l--;
                        }
                    }
                    c2 -= b[j];
                    j++;
                }
                if (c2 <= b[b.Count - 1] && c2 >= 0)
                    sum++;
                output.Add(sum.ToString());
            }
        }
    }
}
