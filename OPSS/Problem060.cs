using System.Text;

namespace OPSS
{
    /* Problemset: 5, Difficulty: 1/5
     * A certain species of bacteria has a genome, which can be described by chromosomes B, C and S.
     * B and C are simple chromosomes. S is a complex chromosome, which countains for other chromosomes,
     * whether simple or complex ones.
     * During its life cycle all complex chromosomes that consist of same four simple chromosomes
     * merge into a simple chromosome with the same letter.
     * When transformations are no longer possible, a bacteria dies.
     * Find its final form. See below for sample bacteria:
     * 
     * +----+----+----+----+
     * |    |    |    |    |
     * | C  | C  | C  | B  |
     * +----+----+----+----+
     * |    |    |    |    |
     * | C  | C  | C  | B  |
     * +----+----+----+----+
     * |         |    |    |
     * |         | B  | B  |
     * |    B    +----+----+
     * |         |    |    |
     * |         | B  | B  |
     * +---------+----+----+
     * Cycle 1 is represented by a sequence SSCCCCSCBCBBSBBBB. 
     * 
     * After transforming it looks like this:
     * +---------+----+----+
     * |         |    |    |
     * |         | C  | B  |
     * |    C    +----+----+
     * |         |    |    |
     * |         | C  | B  |
     * +---------+----+----+
     * |         |         |
     * |         |         |
     * |    B    |    B    |
     * |         |         |
     * |         |         |
     * +---------+---------+
     * Cycle 2 is represented by a sequence: SCSCBCBBB w czasie 2 cykli.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 30.
     * Following C line each contain a single string describing chromosome sequence.
     * Each string is no longer than 100000 characters.
     * 
     * Output
     * C lines, each containing a string and a number separated by a whitespace.
     * A string represents final genome sequence and a number equals to final cycle of bacteria's life.
     */
    public sealed class RekursywnaBakteriaCzworkowa : ProblemBase
    {
        protected override string Input => "3\r\nSSCCCCSCBCBBSBBBB\r\nSSCCCCSCCCCSCCCCSCCCC\r\nSBBBB";

        protected override string Output => "SCSCBCBBB 2\r\nC 3\r\nB 2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                StringBuilder newGenome = new(input[i]);
                int cycle = 0;
                int len;
                do
                {
                    len = newGenome.Length;
                    newGenome.Replace("SCCCC", "C").Replace("SBBBB", "B");
                    cycle++;
                }
                while (len > newGenome.Length);

                output.Add($"{newGenome} {cycle}");
            }
        }
    }
}
