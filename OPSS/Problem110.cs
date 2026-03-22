namespace OPSS
{
    /* Difficulty: 2/5
     * Consider a blood bank. There are N blood types numbered 1 to N. A group g1 is compatible 
     * with group g2, if a person with blood type g2 can accept blood type g1. Any blood type 
     * is compatible with itself. Compatibility of g1 with g2 does not necessarily mean inverse is true.
     * Your goal is to determine, whether contents of blood bank can service all the patients 
     * according to blood type compatibility rules.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 10).
     * First line of each data set contains number of blood types N (1 ≤ N ≤ 100).
     * Second line of each data set contains N numbers, each separated by a whitespace.
     * i-th number di in this line is equal of number of units of blood of i-th type in the bank (0 ≤ di ≤ 1000).
     * Third line of each data set also contains N numbers, each separated by a whitespace.
     * i-th number bi in this line is equal to number of units of blood of i-th type needed by patients (0 ≤ bi ≤ 1000). Liczba i-ta w tej linii (1 ≤ i ≤ N) określa liczbę.
     * Following N lines describe compatibility chart of blood types.
     * Each contains numbers separated by a whitespace. First number is number of blood types compatible 
     * with i-th type Ci (1 ≤ Ci ≤ N), followed by Ci numbers, each representing a compatible blood type.
     * 
     * Output
     * C lines, each containing an answer: TAK if supplies in blood bank can serve al patients,
     * NIE otherwise.
     */
    public sealed class GrupyKrwi : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n6 2 2 4\r\n5 3 1 5\r\n4 1 2 3 4\r\n3 2 3 4\r\n2 3 4\r\n1 4\r\n2\r\n1 5\r\n2 3\r\n2 1 2\r\n1 2";

        protected override string Output => "TAK\r\nNIE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[j]);
                j++;
                var stored = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                var needed = input[j].Split(' ').Select(s => int.Parse(s));
                stored = stored.Zip(needed, (a, b) => a - b).ToArray();
                for(int k = 0; k < N; k++)
                {
                    j++;
                    if (stored[k] <= 0)
                        continue;
                    var compatible = input[j].Split(' ').Skip(1).Select(s => int.Parse(s) - 1);
                    foreach (var c in compatible.Where(c2 => stored[c2] < 0))
                        stored[c] += stored[k]; 
                }
                output.Add(stored.All(c => c >= 0) ? "TAK" : "NIE");
                j++;
            }
        }
    }
}
