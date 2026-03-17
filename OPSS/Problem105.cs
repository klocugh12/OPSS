namespace OPSS
{
    /* Difficulty: 1/5
     * There are n servers in a network. A request has been made to merge all contained data 
     * onto a single server. Any server has enough capacity to do so, but it is necessary to pick 
     * one, for which that operation will take the least time. Any unit of data can be transferred 
     * in a single unit of time between any two servers. A single server can only either transmit
     * or receive data at any given time.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 100).
     * Each data set consists of two lines. First line contains number of servers n (1 ≤ n ≤ 100)
     * Second line contains n integers a1, ..., an, each separated by a whitespace.
     * Each ai value (i = 1, ..., n; 0 ≤ ai < 2^31) represents amount of data on i-th server.
     * 
     * Output
     * C lines, each containing minimum number of units of time needed to merge all data onto a single server.
     */
    public sealed class Serwery : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n4 10 8 3\r\n2\r\n1 1";

        protected override string Output => "15\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                j++;
                var data = input[j].Split(" ").Select(s => int.Parse(s)).ToList();
                j++;
                data.Remove(data.Max());
                output.Add(data.Sum().ToString());
            }
        }
    }
}
