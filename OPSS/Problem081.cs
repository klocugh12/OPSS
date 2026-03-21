namespace OPSS
{
    /* Problemset: 7, Difficulty: 2/5
     * A building is planned to be built over square tiles with dimensions a x a meters.
     * Tiles must be arranged in a shape of a square. Find largest possible side of a building,
     * which can be built over such arrangement, whose base is a square as well. Tiles cannot be cut.
     * 
     * Input
     * First line contains number of data sets (1 ≤ C ≤ 5000).
     * C following lines each contain two numbers separated by a whitespace.
     * First number, k (1 ≤ k ≤ 2^31-1), represents number of tiles, and second, a 
     * (1 ≤ a ≤ 2^31-1) represents length of each tile's sides.
     * 
     * Output
     * C lines, each containing maximum possible side of a building expressed in meters.
     */
    public sealed class Plyty : ProblemBase
    {
        protected override string Input => "2\r\n10 3\r\n4 5";

        protected override string Output => "9\r\n10";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                var splits = input[i].Split(' ');
                int k = int.Parse(splits[0]), a = int.Parse(splits[1]);
                output.Add((a * (int)Math.Sqrt(k)).ToString());
            }
        }
    }
}
