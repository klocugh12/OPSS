using System.Text;

namespace OPSS
{
    /* Time limit: 2s, Memory limit: 16MB, Difficulty: 3/5
     * Bees, as we know, build honeycombs out of adjacent regular hexagons.
     * One swarm of bees however did things differently. It built its honeycomb in such a way that:
     * ● regular hexagons don't have common edges, only common vertices,
     * ● empty space between hexagons is filled with regular triangles,
     * ● each triangle has at least one common edge with a hexagon.
     * Since this is not as efficient way to store honey, bees decided to convert to just using hexagons.
     * However, honey stored in triangular sections of honeycomb cannot be reused and is going to waste.
     * Find out, how much honey is going to waste.
     * A honeycomb is a polygon without holes that can be described with a way to walk around it,
     * using numbers 1..6, where 1 describes step at 0 degrees towards x-axis, and values increase
     * every 60 degrees clockwise, as shown below:
     *   5   6
     *    \ /
     * 4 - * - 1
     *    / \
     *   3   2
     * Given such description, calculate number of triangular sections of a honeycomb.
     * 
     * Input
     * First line contains number of data sets  C, 1 ≤ C ≤ 100.
     * Each data set consists of a single line containing a sequence of steps describing a honeycomb.
     * Digits are not separated by any characters. Length of each sequence is no more than 100000 digits.
     * 
     * Output
     * C lines, each containing number of triangular segments within respective honeycomb.
     */
    public sealed class PlasterMiodu : ProblemBase
    {
        protected override string Input => "2\r\n112611322312345345345555561\r\n123423456156";

        protected override string Output => "13\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            Dictionary<string, string> replacementsTri = new()
            {
                ["111"] = "1261",
                ["222"] = "2312",
                ["333"] = "3423",
                ["444"] = "4534",
                ["555"] = "5645",
                ["666"] = "6156",
                ["135"] = "",
                ["246"] = "",
                ["351"] = "",
                ["462"] = "",
                ["513"] = "",
                ["624"] = "",
                ["13"] = "2",
                ["24"] = "3",
                ["35"] = "4",
                ["46"] = "5",
                ["51"] = "6",
                ["62"] = "1"
            };
            Dictionary<string, string> replacementsHexFull = new()
            {
                ["123456"] = "",
                ["234561"] = "",
                ["345612"] = "",
                ["456123"] = "",
                ["561234"] = "",
                ["612345"] = ""
            };
            Dictionary<string, string> replacementsHexPartial = new()
            {
                ["12345"] = "3",
                ["23456"] = "4",
                ["34561"] = "5",
                ["45612"] = "6",
                ["56123"] = "1",
                ["61234"] = "2",
                ["1234"] = "32",
                ["2345"] = "43",
                ["3456"] = "54",
                ["4561"] = "65",
                ["5612"] = "16",
                ["6123"] = "21"
            };
            int C = int.Parse(input[0]);
            for (int i = 1; i <= C; i++)
            {
                StringBuilder sb = new(input[i]);
                int count = 0;
                while (sb.Length > 2)
                {
                    string key, newRep;
                    for (int k = 0; k < sb.Length - 5; k++)
                    {
                        key = sb.ToString().Substring(k, 6);
                        if (replacementsHexFull.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                    }
                    for (int k = 0; k < sb.Length - 4; k++)
                    {
                        key = sb.ToString().Substring(k, 5);
                        if (replacementsHexPartial.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                        else
                        {
                            key = key.Substring(0, 4);
                            if (replacementsHexPartial.TryGetValue(key, out newRep))
                                sb.Replace(key, newRep);
                        }
                    }
                    if (sb.Length >= 4)
                    {
                        key = sb.ToString().Substring(sb.Length - 4, 4);
                        if (replacementsHexPartial.TryGetValue(key, out newRep))
                            sb.Replace(key, newRep);
                    }
                    for (int k = 0; k < sb.Length - 3; k++)
                    {
                        key = sb.ToString().Substring(k, 3);
                        if (replacementsTri.TryGetValue(key, out newRep))
                        {
                            int n = sb.Length;
                            sb.Replace(key, newRep);
                            count += (n - sb.Length) / (key.Length - newRep.Length);
                        }
                        else
                        {
                            key = key.Substring(0, 2);
                            if (replacementsTri.TryGetValue(key, out newRep))
                            {
                                int n = sb.Length;
                                sb.Replace(key, newRep);
                                count += (n - sb.Length) / (key.Length - newRep.Length);
                            }
                        }
                    }
                    if (sb.Length >= 2)
                    {
                        key = sb.ToString().Substring(sb.Length - 2, 2);
                        if (replacementsTri.TryGetValue(key, out newRep))
                        {
                            int n = sb.Length;
                            sb.Replace(key, newRep);
                            count += (n - sb.Length) / (key.Length - newRep.Length);
                        }
                    }
                }
                output.Add(count.ToString());
            }
        }
    }
}
