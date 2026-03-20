namespace OPSS
{
    /* Difficulty: 4/5
     * You're given a network of N computers. Not all computers are directly connected, 
     * but for any two computers in the network there exists some connection between them.
     * Network has a tree structure, i.e. there exists a parent node for any computer in the network
     * other than computer 1, which is main router of the network.
     * 
     * Task is organized in following manner:
     * ● First, pick a main computer of the network and run master program on it.
     * ● Master program splits the task and sends execution units to all other computers in the network.
     * ● When other computer is done with its calculations, it sends results back to main computer.
     * ● Main computer composes final solution from partial results it received from other computers.
     * 
     * If there is no direct connection from main computer to some other machines, data packet travels 
     * through all intermediate computers on the way via shortest possible way.
     * It is necessary to pick a main computer in such way to minimize transfers across the network.
     * You cannot pick computer 1, as it is not physically available to you.
     * Also, compute work needed to transfer all data across the network, assuming each direct transfer 
     * takes 1 unit of work.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 30.
     * First line of each data set contains number of computers in the network N, 2 ≤ N ≤ 100000.
     * Following N-1 lines each contain a single number equal to number of parent of computers 2 to N.
     * 
     * Output
     * C lines each containing two numbers separated by a whitespace. 
     * First number is a number of main computer to minimize work done transferring data. 
     * Second number is a minimum number of work units spent to transfer data.
     */
    public sealed class ObliczeniaRownolegle : ProblemBase
    {
        protected override string Input => "2\r\n7\r\n1\r\n1\r\n2\r\n2\r\n3\r\n3\r\n9\r\n9\r\n2\r\n9\r\n4\r\n4\r\n4\r\n4\r\n1";

        protected override string Output => "2 11\r\n4 12";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= C; i++)
            {
                int N = int.Parse(input[j]);
                j++;
                List<int>[] nodes = new List<int>[N];
                for (int k = 0; k < N; k++)
                    nodes[k] = [];
                for (int k = 1; k < N; k++)
                {
                    nodes[int.Parse(input[j]) - 1].Add(k);
                    j++;
                }
                int head = 0;
                int combined = 1;
                List<int> trav = [];
                while (true)
                {
                    List<(int, int)> counts = [];
                    foreach (var c in nodes[head])
                    {
                        int cTotal = nodes[c].Count;
                        List<int> children = new(nodes[c]);
                        while (children.Count > 0)
                        {
                            cTotal += nodes[children[0]].Count;
                            children.AddRange(nodes[children[0]]);
                            children.RemoveAt(0);
                        }
                        counts.Add((c, cTotal));
                    }
                    counts.Sort((a, b) => (Math.Abs(a.Item2 + combined - (N >> 1)).CompareTo(Math.Abs(b.Item2 + combined - (N >> 1)))));
                    if (head == 0 || Math.Abs(counts[0].Item2 + combined - (N >> 1)) < Math.Abs(combined - (N >> 1)))
                    {
                        trav.Add(head);
                        head = counts[0].Item1;
                        combined += counts.Skip(1).Sum(s=> s.Item2 + 1);
                    }
                    else
                        break;
                }
                int p = head;
                while(trav.Any())
                {
                    var last = trav[trav.Count - 1];
                    nodes[last].Remove(p);
                    nodes[p].Add(last);
                    p = last;
                    trav.Remove(last);
                }
                int sum = 0, level = 1;
                List<int> untraversed = [head];
                while(untraversed.Any())
                {
                    untraversed = untraversed.SelectMany(u => nodes[u]).ToList();
                    sum += untraversed.Count * level;
                    level++;
                }
                output.Add($"{head + 1} {sum}");
            }
        }
    }
}
