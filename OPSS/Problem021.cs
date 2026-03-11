namespace OPSS
{
    /* Difficulty: 2/5
     * 
     * A ship travels one hour downstream and two hours upstream. How long would a raft travel downstream?
     * 
     * Explanation:
     * A ship has its own engine and can travel both downstream and upstream, downstream naturally being faster.
     * A raft has no engine and can only travel downstream with same speed as stream does.
     * 
     * Input:
     * A single line containing two integers: N, M, 1 ≤ N < M ≤ 100000, where N is time ship spends travelling downstream,
     * while M is time ship spents travelling upstream.
     * 
     * Output:
     * Single integer equal to time raft spends travelling downstream.
     */
    public sealed class Tratwa : ProblemBase
    {
        protected override string Input => "1 2";

        protected override string Output => "4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int tp = int.Parse(splits[0]), tm = int.Parse(splits[1]);
            //tp(vs + vp) = tm(vs - vp)
            //vs(tm - tp) = vp(tm + tp)
            //vs = vp(tm + tp)/(tm - tp)
            output.Add((tp * ((tm + tp) / (tm - tp) + 1)).ToString());
        }
    }
}
