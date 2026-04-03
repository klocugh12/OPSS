namespace OPSS
{
    /* Time limit: 0.5s, Memory limit: 4MB, Difficulty: 2/5
     * You're given a fish tank to populate with several species of fish.
     * Each speies can live in a different temperature range.
     * Your goal is to find largest possible temperature range, in which all given species can live.
     * 
     * Input
     * First line contains number of species N (1 ≤ N ≤ 50).
     * Each of the following N ines contains two numbers tmin and tmax, separated by a whitespace,
     * representing minimum and maximum temperature each species can live in (inclusive, 3 ≤ tmin ≤ tmax ≤ 38). 
     * Following line contains number of queries K (1 ≤ K ≤ 1000).
     * Following K lines each contain a single query. A query consists of numbers, each separated by a whitespace.
     * First number of a query is number of species m and following m numbers are indexes of species 
     * a1, a2, ... am (1 ≤ m ≤ N; 1 ≤ ai ≤ N, dla i = 1, 2, ..., m).
     * 
     * Output
     * K lines, each containing two numbers t1 and t2 separated by a whitespace,
     * representing largest temperature range, which can accomodate all species in a query.
     * If not all species can be accomodated, write NIE instead.
     */
    public sealed class Rybki : ProblemBase
    {
        protected override string Input => "4\r\n22 26\r\n18 28\r\n20 28\r\n8 20\r\n2\r\n3 1 2 3\r\n2 1 4";

        protected override string Output => "22 26\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int j = 1;
            List<int[]> ranges = [];
            int N = int.Parse(input[0]);
            for (int i = 0; i < N; i++)
            {
                ranges.Add(input[j].Split(' ').Select(s => int.Parse(s)).ToArray());
                j++;
            }
            N = int.Parse(input[j]);
            j++;
            for (int i = 0; i < N; i++)
            {
                var fishes = input[j].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                int min = 3, max = 38;
                foreach (var f in fishes)
                {
                    min = Math.Max(min, ranges[f - 1][0]);
                    max = Math.Min(max, ranges[f - 1][1]);
                }
                output.Add(min <= max ? $"{min} {max}" : "NIE");
                j++;
            }
        }
    }
}
