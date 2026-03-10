using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * You're given an encrypted text and an alphabet.
     * To decipher it, for each character in encrypted text, 
     * find its position in an alphabet counting from the beginning and replace it with character
     * at same position, except counting from the end.

Input
First line contains an alphabet (ASCII characters with codes [33..126] or ['!'..'~']).
    Second line contains number of encrypted texts, 0 < N < 1000000.
Following N lines contain a single encrypted text each.

    Output:
    N lines each containing a single deciphered text.
     */
    public sealed class StaryTestament : ProblemBase
    {
        protected override string Input => "ZaBoW9#At\r\n3\r\nZtA\r\nBo99###aa\r\nttAA##99WWooBBaZ";

        protected override string Output => "tZa\r\n#9ooBBBAA\r\nZZaaBBooWW99##At";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string alphabet = input[0];
            Dictionary<char, char> cipher = new();
            for (int i = 0; i < alphabet.Length; i++) 
            {
                cipher.Add(alphabet[i], alphabet[alphabet.Length - i - 1]);
            }
            int N = int.Parse(input[1]);
            for(int i = 2; i <= N + 1; i++)
            {
                StringBuilder msg = new();
                foreach (char c in input[i])
                    msg.Append(cipher[c]);
                output.Add(msg.ToString());
            }
        }
    }
}
