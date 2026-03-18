namespace OPSS
{
    /* Difficulty: 3/5
     * Elections are coming. All politicians are decided on whether they support or oppose each other.
     * This relation is mutial: if politician A supports politician B, then politician B also supports
     * politician A. Not all politicians have a party, so they are trying to make a new one.
     * To do so, they organize meetings to debate and in turn consider roster of new party.
     * A problematic situation may occur, when some politician outside of the party supports some
     * politicians from the party, but opposes others. In such case a party could split in two.
     * To remedy this, it is necessary to have all such politicians join a party. There is also a 
     * public vote ranking, in which each politician has a distinct ranking (no ties).
     * Given list of mutual relations between politicians and their public rankings, determine 
     * smallest size of party including two given politicians, which would not be threatened by 
     * possible split. Also find highest and lowest ranks in public vote for that party.
     * 
     * Input
     * First line contains number of politicians N (2 ≤ N ≤ 800).
     * Second line contains a number K (0 ≤ K ≤ N*(N-1)/2).
     * Following K lines each contain two numbers separated by a whitespace A and B (1 ≤ A, B ≤ N).
     * They mean politicians ranked A and B in public rankings support each other. All such pairs are distnct.
     * Folowing line contains number of meetings S (1 ≤ S ≤ N*(N-1)/2).
     * Following S lines each contain two numbers separated by a whitespace X and Y (1 ≤ X, Y ≤ N).
     * They are ranks of politicians who participated in a given meeting.
     * 
     * Output
     * S lines, each containing three numbers separated by a whitespace each.
     * They are, respectively: smallest number of members of a party containing both politicians in a meeting,
     * which would not be threatened by a split, highest, then lowest rank in a public vote for this party.
     */
    public sealed class Partie : ProblemBase
    {
        protected override string Input => "11\r\n13\r\n1 2\r\n2 3\r\n3 4\r\n4 5\r\n5 6\r\n3 5\r\n4 6\r\n7 8\r\n7 9\r\n7 10\r\n8 10\r\n9 10\r\n10 11\r\n4\r\n4 5\r\n3 6\r\n9 11\r\n6 7";

        protected override string Output => "2 4 5\r\n6 1 6\r\n4 7 11\r\n11 1 11";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[1]);
            List<int>[] supports = Enumerable.Range(0, N).Select(i => new List<int>()).ToArray();
            for (int i = 0; i < N; i++)
            {
                var supps = input[i + 2].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                supports[supps[0]].Add(supps[1]);
                supports[supps[1]].Add(supps[0]);
            }
            int k = int.Parse(input[N + 2]);
            for(int i = 0; i < k; i++)
            {
                var meeting = input[N + i + 3].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                List<int> party = new(meeting);
                int[] toAdd;
                do
                {
                    toAdd = party.SelectMany(s => supports[s]).Where(s => !party.Contains(s)).GroupBy(g => g).Where(g => g.Count() < party.Count).Select(g => g.First()).ToArray();
                    party.AddRange(toAdd);
                }
                while (toAdd.Length > 0);
                output.Add($"{party.Count} {party.Min() + 1} {party.Max() + 1}");
            }
        }
    }
}
