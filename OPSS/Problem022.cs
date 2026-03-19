namespace OPSS
{
    /* Difficulty: 2/5
     * 
     * Alice and Bob spend time arranging pebbles in specific patterns, such as
     * each column of the arrangement has no more pebbles than previous ones.
     * Both Bob and Alice describe such arrangements with sequences.
     * First element of Bob's sequence is total number of pebbles, followed by numbers of pebbles
     * in each column, starting from the column with most pebbles. 
     * Alice's sequence consists of numbers of pebbles in each row instead,
     * also starting from row with most pebbles.
     * 
     *      1 ●
     *      1 ●
     *      2 ● ●
     *      2 ● ●
     *      4 ● ● ● ●
     *      4 ● ● ● ●
     *      5 ● ● ● ● ●
     *      5 ● ● ● ● ●
     *        8 6 4 4 2
     * Sample arrangement (Bob: 24 8 6 4 4 2, Alice: 5 5 4 4 2 2 1 1).
     * Write a program translating Bob's descriptions to Alice's.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * Following C lines each contain Bob's description of some arrangement.
     * Each line starts with total number of pebbles N, 1 ≤ N ≤ 1000000,
     * followed by i numbers a1, a2, ..., ai (1 ≤ i ≤ 1000; 1 ≤ ai ≤ 1000)
     * sorted in descending order, each meaning number of pebbles in i-th column.
     * Numbers are separated by a single whitespace.
     * 
     * Output
     * C lines each containing Alice's descriptions of respective arrangement.
     */
    public sealed class Kamyki : ProblemBase
    {
        protected override string Input => "2\r\n24 8 6 4 4 2\r\n19 5 3 3 3 2 1 1 1";

        protected override string Output => "5 5 4 4 2 2 1 1\r\n8 5 4 1 1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                int[] a = input[i].Split(' ').Select(s => int.Parse(s)).Skip(1).ToArray();
                List<int> result = [];
                int index = a.Length - 1;
                for (int j = 1; j <= a[0]; j++)
                {
                    while (a[index] < j)
                        index--;
                    result.Add(index + 1);
                }
                output.Add(string.Join(" ", result));
            }
        }
    }
}
