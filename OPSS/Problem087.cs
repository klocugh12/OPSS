namespace OPSS
{
    /* Difficulty: 3/5
     * Consider a sequence generated as follows:
     * First, set up two parameters as positive integers.
     * First term of a sequence is equal to sum of two parameters.
     * Second term is equal to sum of first term and second parameter.
     * Each following term is a sum of two previous terms.
     * With right parameter setup, it is possible to get Fibonacci sequence.
     * Your goal is to find a pair of parameters that will generate a given number in most possible steps.
     * 
     * Input
     * First line contains number of data sets C (1 ≤ C ≤ 5000).
     * Each data set consists of a single line containing a number N (1 ≤ N ≤ 2*10^9),
     * equal to a number to generate.
     * 
     * Output
     * C lines, each containing two numbers separated by a whitespace. They are equal to initial
     * parameters needed to obtain N in most possible steps, or BRAK if it is not possible.
     * Note that order of parameters does matter.
     */
    public sealed class Generis : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n10";

        protected override string Output => "1 1\r\n2 2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> fib = [1, 1];
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[i]);
                while (N > fib[^1])
                    fib.Add(fib[^1] + fib[^2]);
                int n = fib.Count - 1;
                while (n > 0 && N > (fib[n] + fib[n - 1]))
                    n--;
                bool found = false;
                while (n > 0 && !found)
                {
                    for (int j = 1; j <= (N / fib[n]); j++)
                        for (int k = 1; k <= (N / fib[n]); k++)
                            if (j * fib[n - 1] + k * fib[n] == N)
                            {
                                output.Add($"{j} {k}");
                                found = true;
                                j = N;
                                break;
                            }
                    if (!found)
                        n--;
                }
                if (n == 0)
                    output.Add("BRAK");
            }
        }
    }
}
