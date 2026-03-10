namespace OPSS
{
    /* Difficulty: 1/5
     * 
     * Consider sorting algorithm that swaps adjacent numbers to achieve ascending sorting order.
     * Sample input: 3 1 2. First swap 1 and 3 to get 1 3 2, then 3 and 2 to get 1 2 3.
     * We performed two swaps. Find minimum number of swaps to sort a given input.
     * 
     * Input
     * First line contains number of values to be sorted N, 1≤N≤1000.
     * Second line contains values to be sorted. All value are from range 1..1000 and are separated
     * by whitespace.
     * 
     * Output
     * Single number equal to minimum number of swaps needed to sort the input.
     */
    public sealed class Sortowanie : ProblemBase
    {
        protected override string Input => "3\r\n3 1 2";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int[] tab = input[1].Split(' ').Select(s => int.Parse(s)).ToArray();
            int swaps = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (tab[j] < tab[j - 1])
                    {
                        int temp = tab[j];
                        tab[j] = tab[j - 1];
                        tab[j - 1] = temp;
                        swaps++;
                    }
                }
            }
            output.Add(swaps.ToString());
        }
    }
}
