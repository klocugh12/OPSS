namespace OPSS
{
    /* Time limit: 1s, Memory limit: 4MB, Difficulty: 2/5
     * Alice and Bob have plenty of chocolate bars left after a birthday party.
     * They decided they will be eating it piece by piece in alternating manner,
     * with Alice taking the first piece out of first chocolate bar.
     * They are however wondering, who will eat last remaining piece given all those chocolate bars?
     * Help them find out a solution.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 20. 
     * First line of each data set contains number of chocolate bars C, 1 ≤ C ≤ 100.
     * Following C lines each contain two numbers a and b,  0 < a, b < 2^31,
     * describing dimensions of each chocolate bar. a and b are separated by a single whitespace.
     * 
     * Output
     * D lines, each containing an answer, who will eat last remaining piece. 0 if Alice, 1 if Bob.
     */
    public sealed class Czekoladka : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n10 20\r\n3 3\r\n36 3\r\n11 99\r\n1\r\n5 7";

        protected override string Output => "1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int i = 1;
            while (i < input.Length)
            {
                int C = int.Parse(input[i]);
                i++;
                bool odd = false;
                for (int j = 0; j < C; j++)
                {
                    var splits = input[i].Split(' ').Select(s => int.Parse(s));
                    if (!splits.Any(s => s % 2 == 0))
                        odd = !odd;
                    i++;
                }
                output.Add(odd ? "0" : "1");
            }
        }
    }
}
