namespace OPSS
{
    /* Difficulty: 2/5
     * Paramecium iustus is a singular genus. It feeds on bacteria, but a colony attempts to 
     * equally distribute all local bateria between members to consume, then search for food 
     * somewhere else. Periodically colony multiplies by factor X. Local bacteria multiply the same way.
     * Determine, whether available bacteria can be divided equally among the colony.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10).
     * Each data set is defined as follows:
     * First line of a data set contains number of multiplications N (1 ≤ N ≤ 100000).
     * Following N lines each contain a letter and a number separated by a whitespace.
     * A letter is either b, if bacteria have multiplied, or p, if paramecium have multiplied.
     * A number is multiplication factor X to apply to respective colony, (1 ≤ X ≤ 100000).
     * 
     * Output
     * D lines, each containing number of bacteries for each paramecium colony member, or -1,
     * if equal distribution is not possible.
     */
    public sealed class Pantofelek : ProblemBase
    {
        protected override string Input => "3\r\n4\r\nb 12\r\nb 5\r\np 20\r\nb 7\r\n5\r\nb 8\r\np 13\r\nb 5\r\nb 39\r\np 2\r\n5\r\nb 6\r\nb 48\r\np 9\r\nb 11\r\np 3";

        protected override string Output => "21\r\n60\r\n-1";

        int gcd(int m, int n)
        {
            int temp;
            if (m > n)
            {
                temp = m;
                m = n;
                n = temp;
            }
            while (m > 0)
            {
                temp = m;
                m = n % m;
                n = temp;
            }
            return n;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                int N = int.Parse(input[j]);
                j++;
                int b = 1, p = 1;
                for (int k = 0; k < N; k++)
                {
                    var s = input[j].Split(' ');
                    int X = int.Parse(s[1]);
                    if (s[0] == "p")
                        p *= X;
                    else
                        b *= X;
                    X = gcd(b, p);
                    b /= X;
                    p /= X;
                    j++;
                }
                output.Add(b % p == 0 ? (b / p).ToString() : "-1");
            }
        }
    }
}
