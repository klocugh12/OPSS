namespace OPSS
{
    /* Time limit: 1s, Memory limit: 1.5MB, Difficulty: 1/5
     * A factorial (n!) is defined as 1 for n ≤ 1 and product of all integers from 1 to n otherwise.
     * Find last digit of n!.
     * 
     * Input
     * One line containing a single integer n, 0 ≤ n ≤ 30000.
     * 
     * Output
     * One line containing last digit of n!.
     */
    public sealed class Silnia : ProblemBase
    {
        protected override string Input => "4";

        protected override string Output => "4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int n = int.Parse(input[0]);
            string[] digits = ["1", "1", "2", "6", "4"];
            output.Add(n < 5 ? digits[n] : "0");

        }
    }
}
