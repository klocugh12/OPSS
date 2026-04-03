namespace OPSS
{
    /* Time limit: 2s, Memory limit: 4MB, Difficulty: 3/5
     * An IP address version P consists of P numbers from 0 to 255 separated by a dot each.
     * In this case addresses can only consist of some specific, predetermined numbers.
     * Knowing those numbers, print possible IP addresses in ascending order.
     * 
     * Input
     * First line contains IP version P, 3 ≤ P ≤ 10.
     * Second line contains P numbers separated by a whitespace.
     * They represent predetermined numbers allowed in an address. 
     * Each number is an integer from 0 to 255. Repetitions are allowed.
     * 
     * Output
     * Maximum 10000 lines, each containing unique IP address made from given numbers.
     * Addresses must be printed in ascending order.
     * If there are no more than 10000 addresses, print all of them.
     */
    public sealed class AdresyIP : ProblemBase
    {
        protected override string Input => "4\r\n127 0 0 1";

        protected override string Output => "0.0.1.127\r\n0.0.127.1\r\n0.1.0.127\r\n0.1.127.0\r\n0.127.0.1\r\n0.127.1.0\r\n1.0.0.127\r\n1.0.127.0\r\n1.127.0.0\r\n127.0.0.1\r\n127.0.1.0\r\n127.1.0.0";

        static int total = 0;
        static void IPs(string added, IEnumerable<int> free, List<string> output)
        {
            if (total >= 10000)
                return;
            if (free.Count() == 1)
            {
                output.Add($"{added}.{free.First()}");
                total++;
                return;
            }
            else
                foreach (int c in free.Distinct())
                {
                    var i = free.ToList().IndexOf(c);
                    IPs(added == "" ? $"{c}" : $"{added}.{c}", free.Take(i).Concat(free.Skip(i + 1).Take(free.Count() - i - 1)), output);
                }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var list = input[1].Split(' ').Select(s => int.Parse(s)).ToList();
            list.Sort((a, b) => a.CompareTo(b));
            IPs("", list, output);
        }
    }
}
