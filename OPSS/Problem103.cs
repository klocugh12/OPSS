namespace OPSS
{
    /* Difficulty: 3/5
     * An animal graph G can be described as follows:
     * ● G is simple, connected and contains exactly two cycles (head and body), each no shorter than 3.
     * ● Cycles do not have common vertices and are connected by a single path (neck).
     * ● Head cycle is shorter than body cycle.
     * ● Two vertices on body cycle have degree 4. Each of them has two additional paths of equal length (two pairs of legs).
     * ● One or two vertices on body cycle have degree 3. First of them connects body and neck,
     *   second (optional) one has additional path representing a tail.
     * ● All remaining body cycle vertices have degree 2.
     * ● All head cycle vertices have degree 2, except for one connecting head with neck, which has degree 3.
     * Smallest animal graph has 11 nodes. Given graph's description, answer whether it is an animal graph.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * First line of each data set contains number of nodes n, 1 ≤ n ≤ 100000.
     * Following n - 1 lines each contain multiple numbers separated by a whitespace.
     * First number is a number of neighbors of current node with index greater than current one.
     * Following numbers are indexes of neighbors of current node, sorted in ascending order.
     * n-th node does not have a corresponding line, because it has greatest index.
     * 
     * Output
     * C lines, each containing an answer for a respective data set: "tak" if graph is an animal, "nie" otherwise.
     */
    public sealed class GrafyAnimalne : ProblemBase
    {
        protected override string Input => "2\r\n19\r\n2 2 3\r\n1 3\r\n1 4\r\n1 5\r\n1 6\r\n2 10 7\r\n3 8 16 18\r\n1 9\r\n2 10 15\r\n2 13 14\r\n1 13\r\n1 14\r\n0\r\n0\r\n0\r\n1 17\r\n0\r\n1 19\r\n11\r\n2 10 11\r\n2 8 9\r\n1 7\r\n1 7\r\n1 8\r\n1 8\r\n2 8 9\r\n0\r\n1 10\r\n1 11";

        protected override string Output => "tak\r\ntak";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {    
                int n = int.Parse(input[j]);
                j++;
                Dictionary<int, List<int>> nodes = [];
                for (int k = 1; k <= n; k++)
                    nodes.Add(k, []);
                for (int k = 1; k < n; k++)
                {
                    foreach(var node in input[j].Split(' ').Skip(1).Select(s => int.Parse(s)))
                    {
                        nodes[node].Add(k);
                        nodes[k].Add(node);
                    }
                    j++;
                }
                bool isAnimal = TrimLegsAndTail(nodes);
                isAnimal &= HasTwoCyclesAndNeck(nodes);
                isAnimal &= AreCyclesDifferent(nodes);
                output.Add(isAnimal ? "tak" : "nie");
            }
        }

        static bool AreCyclesDifferent(Dictionary<int, List<int>> nodes)
        {
            int[] counts = [0, 0];
            foreach (var n in nodes.Where(node => node.Value.Count == 3))
            {
                HashSet<int>[] cycles = [[], [], []];
                int i = 0;
                while (i < 3)
                {
                    cycles[i].Add(n.Value[i]);
                    i++;
                }
                i = 0;
                int next = cycles[i].First();
                while (i < 3)
                {
                    next = nodes[next].FirstOrDefault(node => 
                        !cycles[i].Contains(node));
                    if (next == 0 || nodes[next].Count == 3)
                    {
                        i++;
                        if (i < 3)
                            next = cycles[i].First();
                    }
                    else
                        cycles[i].Add(next);
                }
                int value = (cycles[0].Count == cycles[1].Count) ? cycles[0].Count :
                    (cycles[1].Count == cycles[2].Count ? cycles[1].Count : cycles[0].Count) ;
                if (counts[0] == 0)
                    counts[0] = value;
                else
                    counts[1] = value;
            }
            return counts[0] != counts[1];
        }

        static bool HasTwoCyclesAndNeck(Dictionary<int, List<int>> nodes)
        {
            if (nodes.Count < 6)
                return false;
            int c2 = 0, c3 = 0;
            var toRemove = nodes.Keys.Where(k => nodes[k].Count == 0).ToList();
            foreach (var tr in toRemove)
                nodes.Remove(tr);
            foreach(var n in nodes)
            {
                if (n.Value.Count == 2)
                    c2++;
                else if (n.Value.Count == 3)
                    c3++;
                else
                    return false;
            }
            return c3 == 2;
        }

        static bool TrimLegsAndTail(Dictionary<int, List<int>> nodes)
        {
            if (nodes.Count < 11)
                return false;
            int[] ones = Enumerable.Range(1, nodes.Count).Where(j => nodes[j].Count == 1).ToArray();
            if (ones.Length < 4 || ones.Length > 5)
                return false;
            List<List<int>> links = [];
            for(int i = 0; i < ones.Length; i++)
            {
                links.Add([ones[i]]);
                int newOne = nodes[ones[i]][0];
                while (nodes[ones[i]].Count == 1)
                {
                    nodes[newOne].Remove(ones[i]);
                    nodes[ones[i]].Clear();
                    ones[i] = newOne;
                    links[i].Add(newOne);
                    newOne = nodes[ones[i]][0];
                }
            }
            var groups = links.GroupBy(n => n.Last());
            bool hasTail = false;
            foreach(var g in groups)
            {
                if (g.Count() > 2)
                    return false;
                if(g.Count() == 1)
                {
                    if (hasTail)
                        return false;
                    else
                        hasTail = true;
                }
                if (g.First().Count != g.Last().Count)
                    return false;
            }
            return true;
        }
    }
}
