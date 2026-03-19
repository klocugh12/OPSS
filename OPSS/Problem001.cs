namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * Compute value of the greatest common divisor of two numbers a and b.
     * 
     *  Input
     *  First line contains number of data sets n, 1 ≤ n ≤ 50000. 
     *  Following n lines contain numbers a, b, 1 ≤ a,b ≤ 100000000, separated with a single whitespace.
     *  
     *  Output
     *  n lines, where i-th line contains value of GCD(a, b), where a and b are values from i-th data set.
     */
    public sealed class ProblemEuklidesa : ProblemBase
    {
        protected override string Input => "3\r\n2 6\r\n4 14\r\n5 7";

        protected override string Output => "2\r\n2\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            for(int i = 1; i <= n; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                if(a > b)
                {
                    int temp = a;
                    a = b; 
                    b = temp;
                }
                while(a > 1)
                {
                    int temp = a;
                    a = b % a;
                    b = temp;
                }
                output.Add($"{(a == 0 ? b : a)}");
            }
        }
    }
}
