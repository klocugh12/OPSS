namespace OPSS
{
    /* Difficulty: 4/5
     * Zadanie
Twoim zadaniem będzie stwierdzenie, czy dla zadanego początkowego układu klocków, możliwe
będzie w skończonej liczbie ruchów doprowadzenie do wygranej. Dodatkowo nie będziemy
ograniczać się do klasycznej wersji gry, ale wprowadzimy dowolne rozmiary planszy, zawsze
jednak będzie tylko 1 puste pole.
Fot. Klasyczna "Piętnastka".
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 10, oznaczająca ilość
zestawów danych. Każdy zestaw danych rozpoczyna się linią zawierającą 2 liczby naturalne W i K,
2 ≤ W, K ≤ 100. Liczby te oznaczają odpowiednio liczbę wierszy oraz kolumn w układance. W
każdym z kolejnych W wierszy zestawu danych znajduje się K liczb naturalnych z zakresu 0..W*K-
1. Są to numery klocków na poszczególnych pozycjach w momencie rozpoczęcia gry. Numer 0
oznacza puste miejsce za pomocą którego dokonujemy ruchów.
Wyjście
Dla każdego zestawu danych w osobnych liniach należy wypisać słowo "tak" jeśli gracz może
doprowadzić do wygranej albo słowo "nie", jeśli jest to niemożliwe.
     */
    public sealed class Pietnastka : ProblemBase
    {
        protected override string Input => "2\r\n4 4\r\n1 6 2 4\r\n3 13 11 7\r\n14 5 10 8\r\n0 9 15 12\r\n4 4\r\n1 2 3 4\r\n5 6 7 8\r\n9 10 11 12\r\n13 15 14 0";

        protected override string Output => "tak\r\nnie";

        static bool FourSolvable(List<int[]> puzzle)
        {
            var last4 = Enumerable.Range(0, 2).SelectMany(i => puzzle[puzzle.Count + i - 2].TakeLast(2)).ToList();
            if (!last4.Contains(0))
                return false;
            while (last4.Last() != 0)
            {
                last4 = [last4[2], last4[0], last4[3], last4[1]];
            }
            return last4[0] < last4[1] && last4[1] < last4[2];
        }
        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int K = int.Parse(splits[0]), W = int.Parse(splits[1]);
                j++;
                List<int[]> rows = [];
                for (int k = 0; k < W; k++)
                {
                    rows.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                    j++;
                }
                int rowsTop = 0;
                while (rowsTop < rows.Count && Enumerable.Range(0, K).All(i2 => rows[rowsTop][i2] == rowsTop * K + i2 + 1))
                {
                    rowsTop++;
                }
                int colsLeft = 0;
                while (colsLeft < K && Enumerable.Range(0, rows.Count).All(
                    i2 => rows[i2][colsLeft] == i2 * K + colsLeft + 1))
                {
                    colsLeft++;
                }
                W -= rowsTop;
                K -= colsLeft;
                if (W == 1 && Enumerable.Range(0, K - 1).All(
                    i2 => rows[rows.Count - 1][i2] == (rows.Count - 1) * K + i2 + 1
                    || rows[rows.Count - 1][i2 + 1] == (rows.Count - 1) * K + i2 + 1))
                    W = 0;
                if (K == 1 && Enumerable.Range(0, K - 1).All(
                    i2 => rows[i2][K - 1] == i2 * K
                    || rows[i2 + 1][K - 1] == i2 * K))
                    K = 0;
                bool solvable = K != 1 && W != 1;
                solvable = solvable && (K != 2 || W != 2 || FourSolvable(rows));
                output.Add(solvable ? "tak" : "nie");
            }
        }
    }
}
