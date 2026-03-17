namespace OPSS
{
    /* Difficulty: 4/5
     * Consider an alley with N statues alongside it. Every year, a different statue is 
     * being moved to the spot next to King's Palace. Cost of moving a statue depends on both 
     * weight and distance needed to move it, exactly C * |A - B|, where C is weight of statue,
     * and A and B are respectively, statue's initial and final position. Since there is already 
     * a statue next to King's Palace, it needs to change position as well. However, it is not 
     * always optimal to simply swap old and new statue next to King's Palace. It may be better
     * to move heavier old statue into a position of some other, lighter statue and then move lighter 
     * statue further back, possibly to the spot of even lighter statue, and so on, until previously freed position
     * is reached. Your goal is to find minimum possible cost to move a given statue to a given
     * position next to King's Palace and to have all positions filled.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10).
     * First line of each data set contains three numbers separated by a whitespace each.
     * They are, respectively: number of statues N, position of statue to move next to King's Palace P,
     * and position next to King's Palace K (1 ≤ N ≤ 50000; 1 ≤ P, K ≤ N).
     * Second line of each data set contains N numbers separated by a whitespace.
     * An i-th number in line represents weight of statue in i-th position. All weights are 
     * positive and lesser than 1000000.
     * 
     * Output
     * D lines, each containing minimum cost to move statue from position P to K and have all positions filled.
     */
    public sealed class CzterystaDwadziesciaDwa : ProblemBase
    {
        protected override string Input => "2\r\n10 2 7\r\n8 3 6 12 9 4 8 7 1 5\r\n10 9 5\r\n8 3 6 12 9 4 8 7 1 5";

        protected override string Output => "37\r\n25";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[1]) - 1, b = int.Parse(splits[2]) - 1;
                j++;
                var tab = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                List<int> solution = [a, b];
                bool cont;
                do
                {
                    cont = false;
                    for (int k = 0; k < solution.Count; k++)
                    {
                        var curr = solution[k];
                        var next = solution[(k + 1) % solution.Count];
                        var minL = 0;
                        for (int l = 1; l < tab.Length; l++)
                        {
                            if (solution.Contains(l))
                                continue;
                            if (tab[curr] * Math.Abs(l - curr) + tab[l] * Math.Abs(l - next)
                                < tab[curr] * Math.Abs(minL - curr) + tab[minL] * Math.Abs(minL - next))
                                minL = l;
                        }
                        if (tab[curr] * Math.Abs(minL - curr) + tab[minL] * Math.Abs(minL - next) <
                            tab[curr] * Math.Abs(next - curr))
                        {
                            solution.Insert(k + 1, minL);
                            cont = true;
                        }
                    }
                }
                while (cont);
                int sum = 0;
                for (int k = 0; k < solution.Count; k++)
                {
                    sum += tab[solution[k]] * Math.Abs(solution[k] - solution[(k + 1) % solution.Count]);
                }
                output.Add(sum.ToString());
            }
        }
    }
}
