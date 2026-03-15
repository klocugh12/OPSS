using System.Text;

namespace OPSS
{
    /* Difficulty: 4/5
     * A bit pattern is a binary number with n-bits, of which k are equal to 1, 1 ≤ k ≤ n.
     * Consider all bit patterns sorted lexicographically in descending order.
     * Find d-th bit pattern. Assume that d is not larger than number of such bit patterns.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 500.
     * Each data set consists of two lines. First line contains two natural numbers n and k, 1 ≤ k ≤ n ≤ 100,
     * separated by a whitespace. They mean, respectively, length of pattern and number of 1s.
     * econd line contains index of pattern to find d, 1 ≤ d ≤ 2^31-1.
     * 
     * Output
     * C lines, each containing a bit pattern with requested index d.
     */
    public sealed class WzorceBitowe : ProblemBase
    {
        protected override string Input => "2\r\n3 3\r\n1\r\n2 1\r\n2";

        protected override string Output => "111\r\n01";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<List<int>> binoms = [[1], [1, 1]];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[(i << 1) - 1].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = int.Parse(input[i << 1]);
                StringBuilder sb = new();
                while(c > 0 && b > 0)
                {
                    while(a - 1 >= binoms.Count)
                    {
                        binoms.Add([1]);
                        for(int k = 1; k < binoms.Count - 1; k++)
                        {
                            binoms[binoms.Count - 1].Add(binoms[binoms.Count - 2][k] + binoms[binoms.Count - 2][k - 1]);
                        }
                        binoms[binoms.Count - 1].Add(1);
                    }    
                    int skip = binoms[a - 1][b - 1];
                    if (skip < c)
                    {
                        sb.Append('0');
                        c -= skip;
                    }
                    else
                    {
                        sb.Append('1');
                        b--;
                    }
                }
                sb.Append(b== 0 ? '0' : '1', a - sb.Length);
                output.Add(sb.ToString());
            }
        }
    }
}
