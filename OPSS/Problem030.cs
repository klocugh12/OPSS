namespace OPSS
{
    /* Difficulty: 4/5
     * You're given a word consisting of letters from English alphabet.
     * Find number of ways you can remove letters from that word, such as resulting word reads the same
     * left to right and right to left. Each way is considered unique, if it removes letters from different
     * positions. E.g., for word AAA you can obtain word AA in three unique ways (*AA, A*A, AA*).
     * Resulting word has to have at least one letter. Removing zero letters is also a valid option.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * Each data set consists of a single line containing a single word of length L, 1 ≤ L ≤ 50.
     * 
     * Output
     * C lines, each containing number of ways to remove letters from original words to get a word
     * reading the same right to left as left to right.
     */
    public sealed class Mikolaj : ProblemBase
    {
        protected override string Input => "4\r\nX\r\nXX\r\nALA\r\nAAA";

        protected override string Output => "1\r\n3\r\n5\r\n7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j;
            for(int i = 1; i <= N; i++)
            {
                Dictionary<char, List<int>> occurences = [];
                for(j = 0; j < input[i].Length; j++)
                {
                    if (!occurences.ContainsKey(input[i][j]))
                        occurences.Add(input[i][j], []);
                    occurences[input[i][j]].Add(j);
                }
                if (input[i].Length == 1)
                {
                    output.Add("1");
                    continue;
                }
                else if (input[i].Length == 2)
                {
                    output.Add(input[i][0] == input[i][1] ? "3" : "2");
                    continue;
                }
                int count = input[i].Length + (input[i].Length > 1 ? 1 : 0);
                List<(int, int, int, int)> pal2 = [];
                foreach(var oc in occurences.Keys.Where(k => occurences[k].Count > 1))
                {
                    for(j = 0; j < occurences[oc].Count; j++)
                    {
                        for (int k = j + 1; k < occurences[oc].Count; k++)
                        {
                            pal2.Add((occurences[oc][j], occurences[oc][j], occurences[oc][k], occurences[oc][k]));
                            count++;
                        }
                    }
                }
                j = 3;
                while(j < input[i].Length && pal2.Count > 0)
                {
                    if(j % 2 == 1)
                        foreach(var pal in pal2)
                        {
                            count += (pal.Item3 - pal.Item2 - 1);
                        }
                    else
                    {
                        List<(int, int, int, int)> newPal2 = [];
                        for (int k = 0; k < pal2.Count; k++)
                        {
                            for(int l = k + 1; l < pal2.Count; l++)
                            {
                                if (pal2[k].Item2 < pal2[l].Item1 && pal2[k].Item3 > pal2[l].Item4)
                                {
                                    if(pal2[l].Item3 - pal2[l].Item2 > 1)
                                        newPal2.Add((pal2[k].Item1, pal2[l].Item2, pal2[l].Item3, pal2[k].Item4));
                                    count++;
                                }
                                else if(pal2[l].Item2 < pal2[k].Item1 && pal2[l].Item3 > pal2[k].Item4)
                                {
                                    if(pal2[k].Item3 - pal2[k].Item2 > 1)
                                        newPal2.Add((pal2[l].Item1, pal2[k].Item2, pal2[k].Item3, pal2[l].Item4));
                                    count++;
                                }
                            }
                        }
                        pal2 = newPal2;
                    }
                    j++;
                }
                output.Add(count.ToString());
            }
        }
    }
}
