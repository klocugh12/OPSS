namespace OPSS
{
    /* Difficulty: 3/5
     * You have a vending machine, which gives out a change after each transaction.
     * Given list of coin values and a change to dispense, find minimum number of coins to use.
     * 
     * Input
     * First line contains number of data sets C, 0 < C ≤ 100.
     * First line of each data set contains two numbers N and K separated by a whitespace,  0 ≤ N ≤ 1000, 0 < K ≤ 100.
     * They are equal to change to dispense and number of coins respectively.
     * Second line of each data set contains K values of coins used to dispense a change.
     * Values are unique, positive and no greater than 1000. Each value is separated by a whitespace.
     * 
     * Output
     * C lines, each containing minimum number of coins needed to dispense a change, or 0 if it is not possible for a given value.
     */
    public sealed class Automat : ProblemBase
    {
        protected override string Input => "3\r\n11 3\r\n5 3 1\r\n1 1\r\n2\r\n1000 10\r\n1 3 5 7 10 20 50 100 200 300";

        protected override string Output => "3\r\n0\r\n4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int c = int.Parse(splits[0]);
                j++;
                var coins = input[j].Split(' ').Select(s => int.Parse(s)).Where(s => s <= c).OrderBy(s => s).ToArray();
                j++;
                if(coins.Length == 0)
                {
                    output.Add("0");
                    continue;
                }
                int[] counts = new int[c + 1];
                foreach(int coin in coins)
                {
                    counts[coin] = 1;
                }
                for(int k = coins[0] + 1; k <= c; k++)
                {
                    foreach (int coin in coins.Where(c2 => c2 < k))
                        counts[k] = counts[k] == 0 ? counts[k - coin] + 1 : Math.Min(counts[k], counts[k - coin] + 1);
                }
                output.Add(counts[c].ToString());
            }
        }
    }
}
