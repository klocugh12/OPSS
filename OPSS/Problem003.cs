namespace OPSS
{
    /* Difficulty: 1/5
     * 
Compute value of sum of n numbers.

     Input
First line contains number of data sets C, 1<=C<=200000.
Following C lines contain one data set each. Each data set consists of one number n 1=<n<=100000,
    ollowed by n numbers ai 0<=ai<=1000, 1<=i<=n.

    Output
    C lines, where i-th line contains sum of values described in i-th data set.
     */
    public sealed class LaboratoryjneRozwazania : ProblemBase
    {
        protected override string Input => "2\r\n3 1 2 3\r\n4 0 2 0 0";

        protected override string Output => "6\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int N2 = int.Parse(splits[0]);
                int sum = 0;
                for (int j = 1; j <= N2; j++)
                    sum += int.Parse(splits[j]);
                output.Add(sum.ToString());
            }
        }
    }
}
