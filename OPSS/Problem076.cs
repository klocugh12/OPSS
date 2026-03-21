namespace OPSS
{
    /* Difficulty: 4/5
     * A logistics company provides transport services between N towns connected with K roads.
     * Any two cities have at most a single road connecting them and each road is two-way.
     * From any city you can get to any other city, but not necessarily directly.
     * Each road has daily limit of deliveries. Transporting each box costs 1 dollar, and that's
     * a daily limit for each box, meaning each box can only be transferred between two directly
     * connected cities each day. Find minimum number of days necessary to deliver all boxes 
     * from city 1 to city N given specific budget.
     * 
     * Input.
     * First line contains number of data sets D (1 ≤ D ≤ 20).
     * First line of each data set contains two numbers separated by a whitespace.
     * First number N (1 ≤ N ≤ 100) represents number of cities. Second number K represents number of roads.
     * Following K lines each contain three numbers separated by a whitespace: A, B, C (1 ≤ A, B ≤ N, 1 ≤ C ≤ 100)
     * First two numbers represent numbers of cities a given road connects.
     * Third number is daily limit of boxes that can be transported using this road.
     * Last line of each data set contains two numbers separated by a whitespace.
     * First number T is a number of containers to transport from city 1 to city N.
     * Second number F is a budget dedicated for whole delivery (1 ≤ T ≤ 10^7; 1 ≤ F ≤ 2^31-1).
     * 
     * Output
     * D lines, each containing minimum number of days needed for entire delivery,
     * or -1 if budget is not sufficient.
     */
    public sealed class Transport : ProblemBase
    {
        protected override string Input => "1\r\n4 5\r\n1 2 5\r\n1 3 2\r\n2 3 1\r\n3 4 10\r\n2 4 3\r\n11 23";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int N = splits[0], K = splits[1];
                //start end limit length
                List<int[]> routes = [];
                j++;
                for (int k = 0; k < K; k++)
                {
                    splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    if (splits[0] == 1)
                        routes.Add([splits[0], splits[1], splits[2], 1]);
                    else
                        foreach (var route in routes.Where(r => splits[0] == r[1]).ToArray())
                        {
                            routes.Add([route[0], splits[1], Math.Min(route[2], splits[2]), route[3] + 1]);
                        }
                    j++;
                }
                routes.RemoveAll(r => r[1] != N);
                routes.Sort((a, b) => a[3].CompareTo(b[3]));
                splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                int T = splits[0], F = splits[1];
                var groups = routes.GroupBy(r => r[3]).Select(g => (g.Key, g.Sum(r => r[2]))).ToList();
                if(F < T * groups[0].Key)
                {
                    j++;
                    output.Add("-1");
                    continue;
                }
                int days = groups[0].Key;
                do
                {
                    var allowed = groups.Where(g => g.Key <= days).Select(g => (days - g.Key + 1) * g.Item2).ToArray();
                    int x = 0;
                    int k = 0;
                    while(k < allowed.Length && x + allowed[k] <= F && x < T)
                    {
                        x += allowed[k];
                        k++;
                    }
                    if (x >= T)
                        break;
                    days++;
                }
                while (true);
                output.Add(days.ToString());
            }
        }
    }
}
