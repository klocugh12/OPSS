namespace OPSS
{
    /* Difficulty: 4/5
     * Towers of Hanoi is a classic game with three rods and n disks.
     * Initially, all disks are on the first rod, sorted from largest to smallest bottom-up.
     * Its goal is to move all disks from first rod to second, in keeping with two rules:
     * * In a single move you can only move a single topmost disk from any rod.
     * * You cannot place larger disk on top of a smaller one.
     * Try to do so in smallest amount of moves possible.
     * 
     * Now, let's modify the game, so that disks are black and white, alternating.
     * In keeping with previously mentioned rules, move all white disks to second rod,
     * and all black disks to third one.
     * Find smallest amount of moves necessary to do so for any given number of disks.
     * 
     * Input
     * A single number n (0 ≤ n ≤ 1000) - number of disks.
     * 
     * Output
     * A single number equal to minimum number of moves necessary to complete the game with two colors.
     */
    public sealed class DwubarwneWiezeHanoi : ProblemBase
    {
        protected override string Input => "6";

        protected override string Output => "45";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            int x = n % 3;
            List<int> result = [x];
            List<int> mul5power = x == 0 ? [5] : [x, 0];
            while (x < n)
            {
                int offset = mul5power.Count - result.Count;
                int carry = 0;
                for (int k = result.Count - 1; k >= 0; k--)
                {
                    result[k] += mul5power[offset + k];
                    result[k] += carry;
                    carry = result[k] / 10;
                    result[k] %= 10;
                }
                for (int k = offset - 1; k >= 0; k--)
                {
                    result.Insert(0, mul5power[k] + carry);
                    carry = result[k] / 10;
                    result[k] %= 10;
                }
                carry = 0;
                for (int k = mul5power.Count - 1; k >= 0; k--)
                {
                    mul5power[k] <<= 3;
                    mul5power[k] += carry;
                    carry = mul5power[k] / 10;
                    mul5power[k] %= 10;
                }
                if (carry > 0)
                    mul5power.Insert(0, carry);
                x += 3;
            }
            output.Add(string.Join("", result));
        }
    }
}
