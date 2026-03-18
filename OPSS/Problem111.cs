namespace OPSS
{
    /* Difficulty: 3/5
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
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int c = a % 2 == 0 ? 1 : -1;
                a++;
                a >>= 1;
                a *= ((a << 1) + c);
                List<int> numbers = [];
                do
                {
                    numbers.Clear();
                    while (a > 0)
                    {
                        numbers.Add(a % b);
                        a /= b;
                    }
                    a = numbers.Sum();
                }
                while (numbers.Count != 1);
                output.Add(numbers[0] < 10 ? numbers[0].ToString() : ((char)('A' + (numbers[0] - 10))).ToString());
            }
        }
    }
}
