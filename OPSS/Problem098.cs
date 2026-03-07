namespace OPSS
{
    /* 5/5
     * Danych jest N dowolnych liczb a1, a2, a3, .. , aN, będących początkowymi wyrazami pewnego ciągu.
Kolejne wyrazy ciągu powstają poprzez zsumowanie N bezpośrednio je poprzedzających wyrazów
tego ciągu:
aN+1 = a1 + a2 + .. + aN,
aN+2 = a2 + a3 + .. + aN+1,
..
Zadanie
Wyznaczyć pięć ostatnich cyfr liczby, będącej sumą wyrazów powyższego ciągu, począwszy od
wyrazu ap, a skończywszy na wyrazie ak.
Wejście
Pierwszą linię wejścia stanowi liczba D, 1 ≤ D ≤ 20, wyznaczająca liczbę zestawów danych. W
dalszej części wejścia znajduje się D zestawów danych. Zestaw danych składa się z trzech linii.
Pierwszą z nich stanowi liczba całkowita N, 2 ≤ N ≤ 50, określająca początkową liczbę wyrazów
ciągu. W drugiej zaś znajduje się N, oddzielonych pojedynczą spacją, liczb całkowitych z
przedziału <0; 10^5>, będących początkowymi wyrazami ciągu. Ostatnia linia zestawu danych ma
postać p k, gdzie 0 < p ≤ k < 2^31 oraz p oznacza numer wyrazu od którego rozpoczynamy
sumowanie, a k numer wyrazu dla którego kończymy sumowanie.
Wyjście
Dla każdego z D zestawów danych, w osobnej linii wyjścia należy wypisać pięć (lub mniej - bez zer
wiodących) ostatnich cyfr liczby, będącej sumą wyrazów ciągu, począwszy od wyrazu ap do
wyrazu ak włącznie.
     */
    public sealed class Ciag : ProblemBase
    {
        protected override string Input => "1\r\n4\r\n2 3 4 5\r\n5 6";

        protected override string Output => "40";

        const int C = 100_000;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                int c = int.Parse(input[j]);
                j++;
                List<int> list = input[j].Split(' ').Select(s => int.Parse(s)).Reverse().ToList();
                j++;
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                int sum = GetSum(b + 2, list);
                sum -= GetSum(a + 2, list);
                sum -= list[0];
                output.Add(((sum + C) % C).ToString());
            }

            static int GetSum(int a, List<int> first)
            {
                if (first.Count > a)
                    return first.Take(a).Sum() % C;
                var exp = Exp(a - first.Count, first.Count);
                return exp.Zip(first, (a, b) => a * b).Sum() % C;
            }

            static int[] Exp(int power, int rank)
            {
                int[][] ret = new int[rank][];
                ret[0] = Enumerable.Range(0, rank).Select(i => 1).ToArray();
                for (int i = 1; i < rank; i++)
                {
                    ret[i] = Enumerable.Range(0, rank).Select(j => 0).ToArray();
                    ret[i][i - 1] = 1;
                }
                int[][] ret2 = new int[rank][];
                Array.Copy(ret, ret2, rank);
                power--;
                while(power > 0)
                {
                    if (power % 2 == 0)
                    {
                        ret2 = Mul(ret2, ret2);
                        power >>= 1;
                    }
                    else
                    {
                        ret = Mul(ret, ret2);
                        power--;
                    }
                }
                return ret[0];
            }

            static int[][] Mul(int[][] dest, int[][] src)
            {
                int[][] ret = new int[dest.Length][];
                for (int i = 0; i < dest.Length; i++)
                    ret[i] = Enumerable.Range(0, dest.Length).Select(j => 0).ToArray();
                for (int i = 0; i < dest.Length; i++)
                    for (int j = 0; j < dest.Length; j++)
                    { 
                        for (int k = 0; k < dest.Length; k++)
                        {
                            ret[i][j] += dest[i][k] * src[k][j] % C;
                        }
                        ret[i][j] %= C;
                    }
                return ret;
            }
        }
    }
}
