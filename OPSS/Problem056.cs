namespace OPSS
{
    /* Difficulty: 2/5
     * 
     * A train travels from Lublin to Kraków. It stops at several midway stations, where passengers 
     * board the train or leave it. Find maximum number of passengers on a train during a trip.
     * 
     * Input
     * First line contains total number of passengersm on a trip B, 1≤ B≤100000.
     * Following B lines contain two numbers separated by a whitespace, t0 and t1, 0 ≤ t0 < t1 ≤ 2^31 - 1
     * They are, respectively: boarding time and leaving time for each passenger.
     * 
     * Output
     * A single number equal to maximum number of passengers on a train during the trip.
     */
    public sealed class LublinKrakow : ProblemBase
    {
        protected override string Input => "3\r\n1 2\r\n2 3\r\n2 4";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int> ins = [], outs = [];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                ins.Add(int.Parse(splits[0]));
                outs.Add(int.Parse(splits[1]));
            }
            ins.Sort();
            outs.Sort();
            int inIndex = 0, outIndex = 0;
            int count = 0;
            while (inIndex < ins.Count)
            {
                while (outs[outIndex] <= ins[inIndex])
                {
                    count--;
                    outIndex++;
                }
                count++;
                inIndex++;
            }
            output.Add(count.ToString());
        }
    }
}
