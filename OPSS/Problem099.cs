namespace OPSS
{
    /* Difficulty: 1/5
     * Phone numbers can be encoded by words, where letters are mapped to digits as described below:
     * ABC: 2
     * DEF: 3
     * GHI: 4
     * JKL: 5
     * MNO: 6
     * PQRS: 7
     * TUV: 8
     * WXYZ: 9
     * Given a word, find corresponding phone number.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 100000).
     * Each data set consists of a single line containing a single word.
     * Each word consists of 1 to 100 uppercase English letters only.
     * 
     * Output
     * C lines, each containing a phone number corresponding to a given word.
     */
    public sealed class Komorka : ProblemBase
    {
        protected override string Input => "1\r\nOPSS";

        protected override string Output => "6777";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            Dictionary<char, int> code = new(){ { 'A', 2 }, { 'B', 2 }, { 'C', 2 }, { 'D', 3 }, { 'E', 3 }, { 'F', 3 }, { 'G', 4 },
                { 'H', 4 },{ 'I', 4 },{ 'J', 5 },{ 'K', 5 },{ 'L', 5 },{ 'M', 6 },{ 'N', 6 },{ 'O', 6 },{ 'P', 7 },
            { 'Q', 7 },{ 'R', 7 },{ 'S', 7 },{ 'T', 8 },{ 'U', 8 },{ 'V', 8 },{ 'W', 9 },{ 'X', 9 },{ 'Y', 9 },{ 'Z', 9 },};
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                output.Add(string.Join("", input[i].Select(s => code[s])));
            }
        }
    }
}
