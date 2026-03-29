namespace OPSS
{
    /* Difficulty: 3/5
     * A queen is a chess piece which attacks its whole rank (row), file (column) and both diagonals.
     * Given n x n chessboard, find number of ways you can place k queens, so that no two queens attack each other.
     * 
     * Input
     * First line contains number of data sets d, 1 ≤ d ≤ 10.
     * Next d lines each contain a single data set. Each data set consists of two numbers separated by a whitespace
     * n and k, 1 ≤ n ≤ 12, 1 ≤ k ≤ n^2, corresponding to size of chessboard and number of queens.
     * 
     * Output
     * d lines, each containing number of ways to place k queens on nxn chessboard without any two queens attacking each other.
     */
    public sealed class Hetmani : ProblemBase
    {
        protected override string Input => "2\r\n3 2\r\n8 8";

        protected override string Output => "8\r\n92";

        struct Position
        {
            public int Column;
            public int Diag1;
            public int Diag2;
        }

        static Position Deconstruct(int j, int a)
        {
            int b = j / a, c = j % a;
            return new Position()
            {
                Column = c,
                Diag1 = c + b,
                Diag2 = b + (a - c - 1)
            };
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int d = int.Parse(input[0]);
            for (int i = 1; i <= d; i++)
            {
                var splits = input[i].Split(' ');
                int n = int.Parse(splits[0]), k = int.Parse(splits[1]);
                if (n < k)
                {
                    output.Add("0");
                    continue;
                }
                int j = 0;
                bool[] cols = new bool[n], diag1 = new bool[(n << 1) - 1], diag2 = new bool[(n << 1) - 1];
                List<int> queens = new(k);
                int counter = 0;
                int limit = n * n - Math.Max(n, k + 1);
                while (true)
                {
                    var coords = Deconstruct(j, n);
                    if (queens.Count == 0)
                    {
                        if (j >= limit)
                            break;
                        queens.Add(j);
                        cols[coords.Column] = true;
                        diag1[coords.Diag1] = true;
                        diag2[coords.Diag2] = true;
                        j += n - (j % n);
                        continue;
                    }
                    if (!(cols[coords.Column] || diag1[coords.Diag1] || diag2[coords.Diag2]))
                    {
                        if (queens.Count < k - 1)
                        {
                            queens.Add(j);
                            cols[coords.Column] = true;
                            diag1[coords.Diag1] = true;
                            diag2[coords.Diag2] = true;
                            j += n - (j % n);
                        }
                        else
                        {
                            counter++;
                            j++;
                        }
                    }
                    else
                        j++;
                    while (j >= n * n)
                    {
                        j = queens[^1];
                        coords = Deconstruct(j, n);
                        cols[coords.Column] = false;
                        diag1[coords.Diag1] = false;
                        diag2[coords.Diag2] = false;
                        queens.RemoveAt(queens.Count - 1);
                        j++;
                    }
                }
                output.Add(counter.ToString());
            }
        }
    }
}
