namespace OPSS
{
    /* Difficulty: 3/5
     * A nonnegative integer H is HEX-palindromic, if there exists a natural number k > 1
     * such as multiplying H times k and reversing it, we get H again, if we use hexadecimal
     * representation. No leading zeros are allowed.
     * Example: 17340 dec (43BC hex) is HEX-palindromic (17340 * 3 = 52020 dec,
     * 43BC * 3 = CB34 hex).
     * Find largest HEX-palindromic number lesser than any given N.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 1000.
     * Each data set consists of a single line containing a hexadecimal representation
     * of a number N, N ≥ 0. It has at most 10 digits and no leading zeros.
     * Letters are uppercase.
     * 
     * Output
     * C lines, each containing largest HEX-palindromic number lesser than a given N,
     * or 0 if there is no such number.
     */
    public sealed class LiczbyHEXPalindromiczne : ProblemBase
    {
        protected override string Input => "3\r\n2000\r\nF533\r\n409F0";

        protected override string Output => "10EF\r\n43BC\r\n21FDE";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                var N = input[i];
                if(N.Length < 4 || (N.Length == 4 && N.CompareTo("10EF") <= 0))
                {
                    output.Add("0");
                    continue;
                }
                string first = $"10{new string('0', N.Length - 4)}EF";
                string second = $"21{new string('0', N.Length - 4)}DE";
                string third = $"43{new string('0', N.Length - 4)}BC";
                if (N.CompareTo(first) < 0)
                    output.Add($"43{new string('F', N.Length - 5)}BC");
                else if(N.CompareTo(second) < 0)
                    output.Add($"10{Middle(N, first)}EF");
                else if (N.CompareTo(third) < 0)
                    output.Add($"21{Middle(N, second)}DE");
                else
                    output.Add($"43{Middle(N, third)}BC");
            }
        }

        static string Middle(string toEdit, string pattern)
        {
            if (toEdit[0..1].CompareTo(pattern[0..1]) > 0)
                return new('F', toEdit.Length - 4);
            var result = toEdit[2 .. (toEdit.Length - 3)];
            if (toEdit.Substring(toEdit.Length - 2, 2).CompareTo(pattern.Substring(pattern.Length - 2, 2)) > 0)
                return result;
            var num = result.Select(c => (c >= '0' && c <= '9' ? c - '0' : (c - 'A' + 10))).ToArray();
            int k = num.Length - 1;
            num[k]--;
            while (num[k] < 0)
            {
                num[k] += 16;
                k--;
                num[k]--;
            }
            return string.Join("", num.Select(c => c < 10 ? (char)(c + '0') : (char)(c - 10 + 'A')));
        }
    }
}
