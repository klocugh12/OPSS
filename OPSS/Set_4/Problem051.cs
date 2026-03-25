namespace OPSS
{
    /* Difficulty: 3/5
     * 
     * A knight is a chess piece which moves in L-shaped pattern. First, it moves 2 squares in 
     * one of four direction, then it moves one square in direction perpendicular to previous one.
     * Initially, a knight is in position (Sx, Sy). Find minimum number of moves it needs to reach
     * a given position (Kx, Ky)
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 5000. 
     * Each data set consists of a single line containing four numbers seaparated by a whitespace each.
     * They are, respectively, Sx, Sy, Kx, Ky, -1000000 ≤ Sx, Sy, Kx, Ky ≤ 1000000. 
     * Sx and Sy describe starting position, Kx and Ky describe final position to reach.
     * 
     * Output
     * D lines, each containin minimum number of moves knight needs to get from (Sx,Sy) to (Kx,Ky).
     */
    public sealed class Skoczek : ProblemBase
    {
        protected override string Input => "3\r\n2 2 5 7\r\n3 2 4 6\r\n0 0 2 2";

        protected override string Output => "4\r\n3\r\n4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            for (int i = 1; i <= D; i++)
            {
                var tab = input[i].Split(' ').Select(s => int.Parse(s)).ToArray();
                int x = Math.Abs(tab[0] - tab[2]);
                int y = Math.Abs(tab[1] - tab[3]);
                int moves = (Math.Min(x, y) / 3) * 3;
                if (moves > 0)
                {
                    x -= moves;
                    y -= moves;
                    moves = (moves / 3) << 1;
                }
                int dist = (Math.Max(x, y) >> 2) << 2;
                if (x > y)
                    x -= dist;
                else
                    y -= dist;
                moves += (dist >> 1);
                dist = x * y;
                switch (dist)
                {
                    case 0:
                        switch (x + y)
                        {
                            case 0:
                                break;

                            case 1:
                                moves = (moves == 0) ? 3 : moves + 1;
                                break;

                            case 2:
                                moves += 2;
                                break;

                            case 3:
                                moves += 3;
                                break;
                        }
                        break;

                    case 1:
                    case 3:
                        moves += 2;
                        break;

                    case 2:
                        moves++;
                        break;

                    case 4:
                        if (x + y == 5)
                            moves += 3;
                        else
                            moves += 4;
                        break;

                    default:
                        break;
                }
                output.Add(moves.ToString());
            }
        }
    }
}
