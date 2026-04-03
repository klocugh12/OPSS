namespace OPSS
{
    /* Time limit: 0.5s, Memory limit: 16MB, Difficulty: 3/5
     * For integers N, B (0 ≤ N; 2 ≤ B ≤ 36), RB(N) is defined as follows:
     * N, if 0 ≤ N ≤ B-1
     * RB(N)= RB(sum of digits of N in base-B representation), dla N ≥ B
     * Given N, B (1 ≤ N ≤ 2^31-1; 2 ≤ B ≤ 36) find RB(1+2+3+...+N).
     * 
     * Input
     * First line contains number of data sets L (1 ≤ L ≤ 1000).
     * Each data set consists of a single line containing two numbers N and B, separated by 
     * a whitespace (1 ≤ N ≤ 2^31-1; 2 ≤ B ≤ 36).
     * 
     * Output
     * L lines, each containing RB(1+2+3+...+N) in base-B representation
     * For digits greater than 9 use uppercase English letters from A to Z (10 to 35 respectively).
     */
    public sealed class SumaCyfr2 : ProblemBase
    {
        protected override string Input => "2\r\n7 36\r\n8 36";

        protected override string Output => "S\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int L = int.Parse(input[0]);
            for (int i = 1; i <= L; i++)
            {
                var splits = input[i].Split(' ');
                int N = int.Parse(splits[0]), B = int.Parse(splits[1]);
                int c = N % 2 == 0 ? 1 : -1;
                N++;
                N >>= 1;
                N *= ((N << 1) + c);
                List<int> numbers = [];
                do
                {
                    numbers.Clear();
                    while (N > 0)
                    {
                        numbers.Add(N % B);
                        N /= B;
                    }
                    N = numbers.Sum();
                }
                while (numbers.Count != 1);
                output.Add(numbers[0] < 10 ? numbers[0].ToString() : ((char)('A' + (numbers[0] - 10))).ToString());
            }
        }
    }
}
