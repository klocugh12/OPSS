namespace OPSS
{
    /* Difficulty: 4/5
     * You're given a safe you need to crack. A safe contains a keypad with digits 0 to 9.
     * Using flour, you've found, which digits are in the code (flour stuck to the grease 
     * from the fingers). You also know, how long the code is. 
     * Assuming each combination takes 1 second to check, calculate, how long at most 
     * it's going to take to crack the code.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 100.
     * Each data set consists of two numbers K and N separated by a whitespace, 1 ≤ K ≤ 10, K ≤ N ≤ 24.
     * K is number of distinct digits in a code, N is length of a code.
     * 
     * Output
     * D lines, each containing maximum time it takes to crack the code in the following format: days:hours:minutes:seconds.
     */
    public sealed class Wlamanie : ProblemBase
    {
        protected override string Input => "2\r\n3 4\r\n4 4";

        protected override string Output => "0:0:0:36\r\n0:0:0:24";

        static int[] factors = [24, 60, 60];

        void mul(int[] time, int number)
        {
            int[] carries = [0, 0, 0];
            for (int i = 0; i < time.Length; i++)
            {
                time[i] *= number;
                if (i > 0 && time[i] > factors[i - 1])
                {
                    carries[i - 1] = time[i] / factors[i - 1];
                    time[i] %= factors[i - 1];
                }
            }
            for (int i = carries.Length - 1; i >= 0; i--)
            {
                time[i] += carries[i];
                if (i > 0 && time[i] >= factors[i - 1])
                {
                    carries[i - 1]++;
                    time[i] %= factors[i - 1];
                }
            }
        }

        void add(int[] time, int[] time2)
        {
            int[] carries = [0, 0, 0];
            for (int i = 0; i < time.Length; i++)
            {
                time[i] += time2[i];
                if (i > 0 && time[i] > factors[i - 1])
                {
                    carries[i - 1] = time[i] / factors[i - 1];
                    time[i] %= factors[i - 1];
                }
            }
            for (int i = carries.Length - 1; i >= 0; i--)
            {
                time[i] += carries[i];
                if (i > 0 && time[i] >= factors[i - 1])
                {
                    carries[i - 1]++;
                    time[i] %= factors[i - 1];
                }
            }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int[] time = [0, 0, 0, 0];
                int[] factors = [24, 60, 60];
                List<(int, int, int[])> options = [(1, b - 1, [a])];
                while (options[0].Item2 > 0)
                {
                    for (int j = 0; j < options.Count; j++)
                    {
                        var opt = options[j];
                        options.RemoveAt(j);
                        options.Insert(j, (Math.Min(opt.Item1 + 1, a), opt.Item2 - 1, opt.Item3.Concat(opt.Item1 < a ? [a - opt.Item1] : [a]).ToArray()));
                        if (opt.Item1 < a && opt.Item2 > a - opt.Item1)
                        {
                            options.Insert(j, (opt.Item1, opt.Item2 - 1, opt.Item3.Concat(opt.Item1 > 1 ? [opt.Item1] : []).ToArray()));
                            j++;
                        }
                    }
                }
                foreach(var opt in options)
                {
                    int[] timeTemp = [0, 0, 0, 1];
                    foreach (var f in opt.Item3)
                        mul(timeTemp, f);
                    add(time, timeTemp);
                }
                output.Add(string.Join(":", time));
            }
        }
    }
}
