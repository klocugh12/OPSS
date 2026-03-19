namespace OPSS
{
    /* Difficulty: 2/5
     * Find square of a number consisting of 1s only (1, 11, 111, 1111, ...).
     * 
     * Input
     * First line contains number of data sets k, 1 ≤ k ≤ 500.
     * Following k lines contain one data set each. Each data set consists of single number n
     * (0 < n ≤ 200) corresponding to number of 1s in number to be squared.
     * 
     * Output
     * k lines, where i-th line is a square of number consisting of number of 1s from i-th data set.
     */
    public sealed class KwadratJedynek : ProblemBase
    {
        protected override string Input => "1\r\n2";

        protected override string Output => "121";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int k = int.Parse(input[0]);
            for(int i = 1; i <= k; i++)
            {
                int n = int.Parse(input[i]);
                byte[] lm1 = new byte[(n << 1) - 1];
                for(int j = 0; j < n; j++)
                {
                    for(int j2 = 0; j2 < n; j2++)
                    {
                        lm1[lm1.Length - (j + j2 + 1)]++;
                        int l = lm1.Length - (j + j2 + 1);
                        while(lm1[l] == 10)
                        {
                            lm1[l - 1]++;
                            lm1[l] = 0;
                            l--;
                        }
                    }
                }
                output.Add(string.Join("", lm1));
            }
        }
    }
}
