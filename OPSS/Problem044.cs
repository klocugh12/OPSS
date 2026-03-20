using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * You're given a string of English uppercase letters.
     * Compress them into strings in following way:
     * If there are more than two occurrences of same letter in a row, only write first occurence
     * followed by number of occurrences of that letter in a row.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 50.
     * Each data set contains a single word containing only English uppercase letter.
     * Each word is no more than 200 characters long.
     * 
     * Output
     * C lines, each containing compressed version of respective word.
     */
    public sealed class Flamaster : ProblemBase
    {
        protected override string Input => "4\r\nOPSS\r\nABCDEF\r\nABBCCCDDDDEEEEEFGGHIIJKKKL\r\nAAAAAAAAAABBBBBBBBBBBBBBBB";

        protected override string Output => "OPSS\r\nABCDEF\r\nABBC3D4E5FGGHIIJK3L\r\nA10B16";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                StringBuilder result = new();
                int count = 1;
                string s = input[i];
                for (int j = 1; j <= s.Length; j++)
                {
                    if (j < s.Length && s[j] == s[j - 1])
                        count++;
                    else
                    {
                        if (count == 1)
                        {
                            result.Append(s[j - 1]);
                        }
                        else if (count == 2)
                        {
                            result.Append(s[j - 1]);
                            result.Append(s[j - 1]);
                        }
                        else
                        {
                            result.Append(s[j - 1]);
                            result.Append(count);
                        }
                        count = 1;
                    }
                }
                output.Add(result.ToString());
            }
        }
    }
}
