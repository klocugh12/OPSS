namespace OPSS
{
    /* Problemset: 6, Difficulty: 1/5
     * A resonant word is a word containing more vowels (upper- or lowercase letters: 
     * 'a', 'e', 'i', 'o', 'u', 'y') than consonants and has at least one consonant.
     * Given a list of words determine number of resonant words on that list.
     * 
     * Input
     * First line contains number of words on the list N, 1 ≤ N ≤ 1000.
     * Following N lines each contains a single word consisting of upper- and lowercase letters
     * of English alphabet.
     * 
     * Output
     * A single line containing number of resonant words on the list.
     */
    public sealed class DzwieczneSlowa : ProblemBase
    {
        protected override string Input => "3\r\naBecadLo\r\nOPSS\r\naaba";

        protected override string Output => "1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int total = 0;
            for(int i = 1; i <= N; i++)
            {
                char[] vowels = ['a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y'];
                string s = input[i];
                int count = s.Count(c => vowels.Contains(c));
                if (count > (s.Length >> 1) && count < s.Length)
                    total++;
            }
            output.Add(total.ToString());
        }
    }
}
