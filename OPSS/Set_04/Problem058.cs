namespace OPSS
{
    /* Difficulty: 4/5
     * A company needs to segregate its waste to become environmentally friendly.
     * There are N bins, each dedicated for one type of waste. To segregate waste, a robot is employed
     * to move waste item by item between containers. Each item can only be moved once, and moving any item
     * takes a single unit of time. Work is done when all items are segregated in distinct containers,
     * i.e., there is only one type of waste in each container.
     * Find out best and worst case for sorting waste.
     * 
     * Input
     * First line contains number of containers (and also waste types) N, 1 ≤ N ≤ 200.
     * Each of the following N lines contains N numbers separated by a whitespace.
     * Numbers x1,x2,..., xN represent amount of waste of each type 1 to N, 0 ≤ xi≤ 1000, 1 ≤ i ≤ N.
     * 
     * Output
     * Two numbers separated by a whitespace, equal to respectively shortest and longest time needed
     * to segregate waste.
     */
    public sealed class Segregacja : ProblemBase
    {
        protected override string Input => "2\r\n2 3\r\n4 3";

        protected override string Output => "5 7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<List<int>> infos = [];

            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ').Select(s => int.Parse(s)).ToList();
                infos.Add(splits);
            }
            List<int> mins = [0], maxs = [0];
            for (int i = 1; i < N; i++)
            {
                List<int> stayMin = [], stayMax = [], switchMin = [], switchMax = [];
                int sum = 0;
                for (int j = 0; j <= i; j++)
                    sum += infos[j][i];
                for (int j = 0; j < i; j++)
                {
                    switchMin.Add(infos[j][mins[j]] + sum - infos[j][i]);
                    switchMax.Add(infos[j][maxs[j]] + sum - infos[j][i]);
                    stayMin.Add(infos[j][i] + infos[i][mins[j]]);
                    stayMax.Add(infos[j][i] + infos[i][mins[j]]);
                }
                int min = 0, max = 0;
                for (int j = 1; j < i; j++)
                {
                    int d = switchMin[j] - stayMin[j];
                    if (d < switchMin[min] - stayMin[min])
                        min = j;
                    if (d > switchMin[max] - stayMin[max])
                        min = j;
                }
                if (switchMin[min] - stayMin[min] < 0)
                {
                    int m2 = mins.IndexOf(min);
                    mins[m2] = i;
                    mins.Add(min);
                    for (int j = 0; j <= i; j++)
                        infos[j][mins[j]] = switchMin[min];
                }
                else
                {
                    mins.Add(i);
                    for (int j = 0; j < i; j++)
                        infos[j][mins[j]] = stayMin[min];
                }
                if (switchMax[max] - stayMax[max] > 0)
                {
                    int m2 = maxs.IndexOf(max);
                    maxs[m2] = i;
                    maxs.Add(max);
                    for (int j = 0; j <= i; j++)
                        infos[j][maxs[j]] = switchMax[max];
                }
                else
                {
                    maxs.Add(i);
                    for (int j = 0; j <= i; j++)
                        infos[j][maxs[j]] = stayMax[max];
                }
            }
            output.Add($"{infos[0][mins[0]]} {infos[0][maxs[0]]}");
        }
    }
}
