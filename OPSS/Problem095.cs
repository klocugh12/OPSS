namespace OPSS
{
    /* Difficulty: 4/5
     * A natural number L is self-descriptive, if, for all k from 1 to L's length,
     * k-th digit in order from most to least significant, is equal to number of occurrences 
     * of digit k + 1. In other words, most significant digit is number of zeroes in L,
     * second most significant digit is number of ones in L, etc.
     * For a given n, find largest self-descriptive number no lesser than n.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 100.
     * Each data set contains a single number n, 0 ≤ n < 10^10.
     * 
     * Output
     * D lines, each containing largest self-descriptive number no lesser than n, or -1 
     * if no such number exists.
     */
    public sealed class SamoopisujaceSieLiczby : ProblemBase
    {
        protected override string Input => "1\r\n2000";

        protected override string Output => "1210";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            List<long> results = [];
            int len = 4;
            while (len <= 10)
            {
                foreach (int zeros in Enumerable.Range((len - (len > 5 ? 4 : 3)), 2))
                {
                    int[] test = new int[len];
                    test[0] = zeros;
                    int toReplace = test.Length - zeros - 1;
                    int remaining = len - zeros;
                    List<int> digits = [zeros];
                    while (remaining > 0)
                    {
                        if (remaining == toReplace)
                        {
                            digits.Add(1);
                            remaining--;
                        }
                        else
                        {
                            digits.Add(2);
                            remaining -= 2;
                        }
                        toReplace--;
                    }
                    int index = 1;
                    while (index < test.Length - 1)
                    {
                        test[index] = digits.Count(i2 => i2 == index);
                        index++;
                    }
                    if (Enumerable.Range(0, test.Length).All(t => test[t] == test.Count(t2 => t2 == t)))
                    {
                        long val = 0;
                        foreach (var x in test)
                            val = val * 10 + x;
                        results.Add(val);
                    }
                }
                len++;
                
            }
            for (int k = 0; k < D; k++)
            {
                int n = int.Parse(input[k + 1]);
                int j = 0;
                while (j < results.Count && n >= results[j])
                    j++;
                output.Add(j == 0 ? "-1" : results[j - 1].ToString());
            }
        }
    }
}
