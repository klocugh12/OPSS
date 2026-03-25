namespace OPSS
{
    /* Difficulty: 1/5
     * John has been invited to a party. Since he owns a bakery, he brought C cookies with him.
     * He gave each guest as many cookies as possible, but every guest got the same number.
     * He ate remaining cookies himself. Determine, how many cookies he was able to eat.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10).
     * First line of each data set contains an integer N (1 ≤ N ≤ 100). 
     * Second line of each data set contains N number separated by a whitespace each:
     * x1, x2, x3, ..., xN (-10 ≤ xi ≤ 10 and xi ≠ 0 dla 1 ≤ i ≤ N).
     * Each of the number represents a change in number of guests. Positive xi means xi guests have 
     * arrived, negative xi means -xi guests have left the party. Initial state was 0 guests.
     * John came after all N described changes.
     * Third line of a data set contains number of cookies John brought to a party C (1 ≤ C ≤ 1000).
     * 
     * Output
     * D lines, each containing a number of cookies John ate in the end.
     */
    public sealed class Ciastka : ProblemBase
    {
        protected override string Input => "3\r\n4\r\n1 3 -4 2\r\n7\r\n3\r\n4 2 -5\r\n8\r\n1\r\n9\r\n14";

        protected override string Output => "1\r\n0\r\n5";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                int N = int.Parse(input[j]);
                j++;
                var guests = input[j].Split(' ').Select(s => int.Parse(s)).Sum();
                j++;
                int C = int.Parse(input[j]);
                j++;
                output.Add((guests == 0 ? C : C % guests).ToString());
            }
        }
    }
}
