using System.Text;

namespace OPSS
{
    /* Difficulty: 4/5
     * Wzorcem bitowym nazywamy dowolny ciąg zerojedynkowy długości n zawierający k jedynek, 1 ≤
k ≤ n. Dla ustalonej długości n i liczby jedynek k, rozpatrzmy wszystkie wzorce bitowe
posortowane malejąco w porządku leksykograficznym. Jaką będzie miał postać wzorzec bitowy
znajdujący się na pozycji d?. Zakładamy, że liczba d jest tak dobrana, że taki wzorzec istnieje.
Wejście
W pierwszym wierszu znajduje się liczba zestawów danych C, 1 ≤ C ≤ 500. Każdy zestaw danych
składa się z dwóch wierszy. W pierwszym wierszu znajdują się dwie liczby naturalne n i k, 1 ≤ k ≤
n ≤ 100 oddzielone pojedynczą spacją. Oznaczają odpowiednio długość wzorca i liczbę jedynek. W
drugim wierszu podana jest pozycja d, 1 ≤ d ≤ 2^31-1 szukanego wzorca.
Wyjście
Dla każdego zestawu danych na wyjściu należy wypisać wzorzec bitowy znajdujący się na pozycji
d.
     */
    public sealed class WzorceBitowe : ProblemBase
    {
        protected override string Input => "2\r\n3 3\r\n1\r\n2 1\r\n2";

        protected override string Output => "111\r\n01";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<List<int>> binoms = [[1], [1, 1]];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[(i << 1) - 1].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = int.Parse(input[i << 1]);
                StringBuilder sb = new();
                while(c > 0 && b > 0)
                {
                    while(a - 1 >= binoms.Count)
                    {
                        binoms.Add([1]);
                        for(int k = 1; k < binoms.Count - 1; k++)
                        {
                            binoms[binoms.Count - 1].Add(binoms[binoms.Count - 2][k] + binoms[binoms.Count - 2][k - 1]);
                        }
                        binoms[binoms.Count - 1].Add(1);
                    }    
                    int skip = binoms[a - 1][b - 1];
                    if (skip < c)
                    {
                        sb.Append('0');
                        c -= skip;
                    }
                    else
                    {
                        sb.Append('1');
                        b--;
                    }
                }
                sb.Append(b== 0 ? '0' : '1', a - sb.Length);
                output.Add(sb.ToString());
            }
        }
    }
}
