namespace OPSS
{
    /* Difficulty: 2/5
     * 
     * Consider the scale to measure weight from the interval (0, 1) with specified precision.
     * Precision can be set as integer from 1 to 10. If precision is equal to m, 
     * then measurement error is less than 1/2^m. Measurements are written down as pairs (l, m).
     * l is reading of the scale when precision is set to m, hence actual weight of an item
     * is estimated to be l/2^m (l is a natural number, 0 < l < 2^m).
     * 
     * Write a program to sort measurements in ascending order.
     * 
     * Input
     * First line contains number of measurments n (1 ≤ n ≤ 20000).
     * Following n lines each contain pairs li, mi, separated by whitespace, 
     * such as 1 ≤ mi ≤ 10 oraz 0 < l < 2^mi
     * 
     * Output
     * n lines each containing a measurement from input in its original form, but sorted in ascending order.
     * If two measurements correspond to same weight, sort them ascending by the reading (first value).
     */
    public sealed class WagaBinarna : ProblemBase
    {
        protected override string Input => "4\r\n1000 10\r\n3 10\r\n5 3\r\n250 8";

        protected override string Output => "3 10\r\n5 3\r\n250 8\r\n1000 10";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            List<int[]> result = [];
            for (int i = 1; i <= n; i++)
            {
                var splits = input[i].Split(' ');
                int[] newSplit = [int.Parse(splits[0]), int.Parse(splits[1]), 0];
                newSplit[2] = newSplit[0] << (10 - newSplit[1]);
                result.Add(newSplit);
            }
            result.Sort((a, b) => a[2] == b[2] ? a[0].CompareTo(b[0]) : a[2].CompareTo(b[2]));
            output.AddRange(result.Select(r => $"{r[0]} {r[1]}"));
        }
    }
}
