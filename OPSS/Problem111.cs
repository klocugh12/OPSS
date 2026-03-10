namespace OPSS
{
    /* Difficulty: 3/5
     * Dla liczb całkowitych N, B (0 ≤ N; 2 ≤ B ≤ 36), przez RB(N) oznaczmy liczbę zdefiniowaną
następująco:
N, dla 0 ≤ N ≤ B-1
RB(N)=
RB(suma cyfr liczby N w systemie o podstawie B), dla N ≥ B
Zadanie
Dla zadanych liczb całkowitych N, B (1 ≤ N ≤ 2^31-1; 2 ≤ B ≤ 36) obliczyć wartość
RB(1+2+3+...+N).
Wejście
Pierwszy wiersz wejścia zawiera jedną liczbę całkowitą L (1 ≤ L ≤ 1000) - jest to liczba zestawów
danych. W kolejnych L wierszach znajdują się zestawy danych. Każdy zestaw danych składa się z
dwóch liczb całkowitych N, B oddzielonych pojedynczą spacją (1 ≤ N ≤ 2^31-1; 2 ≤ B ≤ 36).
Wyjście
Dla każdego zestawu danych na wyjściu należy wypisać wartość RB(1+2+3+...+N) (w systemie o
podstawie B). Cyfry większe niż 9 powinny być wypisywane dużymi literami alfabetu angielskiego
('A' zamiast 10, 'B' zamiast 11, ..., 'Z' zamiast 35).
     */
    public sealed class SumaCyfr2 : ProblemBase
    {
        protected override string Input => "2\r\n7 36\r\n8 36";

        protected override string Output => "S\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = a % 2 == 0 ? 1 : -1;
                a++;
                a >>= 1;
                a *= ((a << 1) + c);
                List<int> numbers = [];
                do
                {
                    numbers.Clear();
                    while (a > 0)
                    {
                        numbers.Add(a % b);
                        a /= b;
                    }
                    a = numbers.Sum();
                }
                while (numbers.Count != 1);
                output.Add(numbers[0] < 10 ? numbers[0].ToString() : ((char)('A' + (numbers[0] - 10))).ToString());
            }
        }
    }
}
