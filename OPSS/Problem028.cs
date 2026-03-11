namespace OPSS
{
    /* Difficulty: 4/5
     * Alice and Bob play a game, where they append pieces of line in alternating manner, 
     * until line collapses under its own weight.
     * First piece is appended to an infinitely durable hook, following pieces are each appended to the last one.
     * Players can use many pieces, with different weights and durabilities.
     * Durability of a piece is directly proportional to its weight, and the ratio is the same for all pieces.
     * Ratio describes how many times its weight a piece can handle. If combined weight of all appended pieces
     * exceed piece's durability, it breaks and player who appended last piece loses the game.
     * All pieces are in infinite supply.
     * Bob starts the game. For any given set of pieces you must determine, if he has a winning strategy.
     * A winning strategy means that Bob's choices can force a win regardless of Alice's choices.
     * 
     * Input
     * First line contains number of data sets C,1 ≤ C ≤ 100.
     * First line of each data set contains a single number N, 1 ≤ N < 100, equal to number of available pieces.
     * Next line contains N positive numbers no greater than 1000 separated by whitespace, equal to weights of available pieces.
     * Last line of each data set contains a single number WW, 1 ≤ WW ≤ 100, equal to durability to weight ratio of all pieces.
     * 
     * Output
     * C lines, each containg an answer: "tak", if Bob has winning strategy for respective data set, "nie" otherwise.
     */
    public sealed class Sznurki : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n1 2 3\r\n4\r\n4\r\n1 2 4 10\r\n12\r\n5\r\n90 91 92 93 999\r\n100\r\n4\r\n450 900 901 902\r\n4";

        protected override string Output => "tak\r\nnie\r\ntak\r\nnie";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                j++;
                int[] vals = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                int WW = int.Parse(input[j]) - 1;
                j++;
                int[] results = new int[WW * vals.Max() + 1];
                foreach (var v in vals)
                    results[v] = 1;
                foreach (var v in vals)
                {
                    int limit = v * WW;
                    if (results[limit] > 0)
                        continue;
                    for (int k = 0; k <= limit; k++)
                    {
                        for (int l = 0; l < vals.Length; l++)
                        {
                            if (results[k] % 2 == 0)
                            {
                                if (k >= vals[l] && results[k] % 2 == 0)
                                    results[k] = results[Math.Min(k - vals[l], vals[l] * WW)] + 1;
                            }
                        }
                    }
                }
                output.Add(vals.Any(v => results[v * WW] % 2 == 0) ? "tak" : "nie");
            }
        }
    }
}
