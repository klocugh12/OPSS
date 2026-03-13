namespace OPSS
{
    /* Difficulty: 3/5
     * Consider following cipher:
    
    C/C++:
    int foo ( int n )
    {
        return n ^ (n >> 1);
    }
    
    Pascal:
    function foo ( n : longint ) : longint;
    begin
        foo := n xor (n shr 1);
    end;

    Write a function that works other way round.
    If it's named oof, then oof(foo(n)) = n.

    Input
    Each line of input contains a single number n, 0 ≤ n ≤ 2^31 - 1.
    Last line is always equal to zero.

    Output
    For all non-zero lines write two values foo(n) and oof(n) separated by a whitespace,
    where foo(n) is result of above function and oof(n) is its inverse.
     */
    public sealed class Szyfr : ProblemBase
    {
        protected override string Input => "1\r\n123\r\n1001\r\n689\r\n0";

        protected override string Output => "1 1\r\n70 82\r\n541 689\r\n1001 801";

        void setb(ref int x, int c) => x |= (1 << c);

        bool checkb(int x, int c) => (x & (1 << c)) > 0;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int i = 0;
            while (input[i] != "0")
            {
                int x = int.Parse(input[i]);
                int foo = x ^ (x >> 1);
                int oof = 0;
                int c = -1, y = x;
                while(y > 0)
                {
                    c++;
                    y >>= 1;
                }
                setb(ref oof, c);
                while(c > 0)
                {
                    if (checkb(oof, c) != checkb(x, c - 1))
                        setb(ref oof, c - 1);
                    c--;
                }
                output.Add($"{foo} {oof}");
                i++;
            }
        }
    }
}
