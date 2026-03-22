namespace OPSS
{
    /* Difficulty: 3/5
     * A crime has been committed. Multiple suspects have been rounded up, of which there is one 
     * perpetrator. Each suspect may testify several times. In a single testimony a suspect can 
     * either name a perpetrator, corroborate or contradict any other testimony.
     * Most likely perpetrator is a suspect, for whom number of false testimonies, assuming that
     * suspect was in fact guilty, would be the lowest. Note that each suspect can make mutually 
     * exclusive testimonies - in such case most certainly some of them are lies.
     * Determine, who is the most likely perpetrator, given list of suspects and their testimonies.
     * If there is more than one equally likely perpetrator, indicate all of them.
     * 
     * Input
     * First line contains number of data sets D (1 ≤ D ≤ 10). 
     * First line of each data set contains two numbers separated by a whitespace.
     * They are, respectively, number of suspects N and number of testimonies Z (1 ≤ N ≤ 10000, 
     * 1 ≤ Z ≤ 20000). Suspects are numbered 1 to N. Following Z lines each contain a single testimony.
     * Each testimony is described by a number P, then a letter C, then a number K, each separated by a 
     * whitespace (1 ≤ P ≤ N). Interpret them as follows:
     * If C is "W", then suspect P testified that suspect K is guilty.
     * If C is "P", then suspect P corroborated K-th testimony.
     * If C is "F", then suspect P contradicted K-th testimony.
     * 
     * Output
     * D lines, each containing list of most likely perpetrators, listed in ascending order. 
     */
    public sealed class Detektyw : ProblemBase
    {
        protected override string Input => "2\r\n3 4\r\n1 W 3\r\n2 P 1\r\n3 W 1\r\n2 F 3\r\n3 3\r\n1 W 2\r\n2 W 3\r\n3 W 1";

        protected override string Output => "3\r\n1 2 3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                List<(int, string, int)> Ws = [];
                var pars = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int[] supps = new int[pars[0]]; 
                j++;
                for(int k = 0; k < pars[1]; k++)
                {
                    var s = input[j].Split(' ');
                    var p = (int.Parse(s[0]), s[1], int.Parse(s[2]));
                    Ws.Add(p);
                    if (s[1] == "W")
                    {
                        supps[p.Item3 - 1]++;
                    }
                    else 
                    {
                        int x = p.Item3 - 1;
                        bool supp = p.Item2 == "P";
                        while (Ws[x].Item2 != "W")
                        {
                            if (Ws[x].Item2 == "F")
                                supp = !supp;
                            x = Ws[x].Item3 - 1;
                        }
                        if(supp)
                        {
                            supps[Ws[x].Item3 - 1]++;
                        }
                        else
                        {
                            for(int k2 = 0; k2 < supps.Length; k2++)
                            {
                                if(k2 != Ws[x].Item3 - 1)
                                    supps[k2]++;
                            }
                        }
                    }
                    j++;
                }
                List<int> minLiars = [];
                int min = 0;
                for(int k = 0; k < pars[0]; k++)
                {
                    if (supps[k] > min)
                    {
                        min = supps[k];
                        minLiars.Clear();
                    }
                    if (supps[k] >= min)
                        minLiars.Add(k + 1);
                }
                output.Add(string.Join(' ', minLiars));
            }
        }
    }
}
