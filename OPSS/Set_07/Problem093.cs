namespace OPSS
{
    /* Time limit: 1.5s, Memory limit: 32MB, Difficulty: 3/5
     * Alice and Bob play a game in which they take turns herding sheeps divided into several groups,
     * starting with Alice. A player during they turn can herd any number of sheeps provided they are all
     * from the same group. Whoever has to herd the last sheep, loses the game.
     * After a few turns Alice declared herself a winner, even though the game was far from over.
     * How is that possible? Find out, whether you can determine a winner in advance, based on
     * initial grouping of sheeps, assuming Alice starts and both Alice and Bob play optimally.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 10.
     * Each data set contains a single line with multiple numbers each separated by a whitespace.
     * First number is equal to number of groups of sheeps N, 1 ≤ N ≤ 100000, and following N
     * numbers ai 1 ≤ ai ≤ 2^31-1, 1 ≤ i ≤ N are equal to number of sheeps in i-th group.
     * 
     * Output
     * C lines, each containing an answer: J, if Alice will win, or B, if Bob will win.
     */
    public sealed class Owce : ProblemBase
    {
        protected override string Input => "2\r\n5 7 9 23 11 17\r\n3 1 2 3";

        protected override string Output => "J\r\nB";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                var groups = input[i].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                output.Add(groups.Select(g => g > 1 ? 2 : 1).Sum() % 2 == 0 ? "J" : "B");
            }
        }
    }
}
