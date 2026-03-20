namespace OPSS
{
    /* Difficulty: 4/5
     * Thieves have robbed the bank and are trying to escape to their hideout.
     * To avoid being spotted as much as possible, they will be going through numerous city parks.
     * Each city park is bounded by three or more streets and contains no streets itself.
     * Thieves can only get from one park to another, if those parks are across a street
     * or a junction of two streets. Thieves need not pick the shortest possible way to their
     * hideout. Parks are numbered 1 to p. Thieves are currently in park 1, park p contains their hideout.
     * City police is trying to catch the thieves. They cannot access parks 1 or p, but they can set up
     * patrols in any other park. Assume, that if thieves go through patrolled park, they will get caught.
     * Parks 1 and p are not adjacent to each other, hence it is possible to catch thieves.
     * You must find minimum number of parks to patrol to ensure thieves are caught.
     * 
     * Input
     * First line contains number of data sets C, 1 ≤ C ≤ 100.
     * Each data set is described as follows:
     * First line contains two numbers separated by a whitespace, n and m, 1 ≤ n < m ≤ 100000.
     * They are, respectively, number of junctions and streets in the city.
     * Following m lines contain two numbers separated by a whitespace, 
     * meaning indexes of junctions connecting consecutive streets.
     * Each two junctions can only be connected by a single street, and streets do not intersect with one another.
     * Following line contains a single number p, 3 ≤ p ≤ 300, meaning number of parks.
     * Following p lines start with number of junctions (and hence also streets) surrounding given park,
     * then contain that many indexes of junctions surrounding that park.
     * Both parks and junctions in the data set are described in ascending order (1 to p and 1 to m respectively).
     * 
     * Output
     * C lines, each containing minimum number of patrols needed to catch the thieves.
     */
    public sealed class Oblawa : ProblemBase
    {
        protected override string Input => "1\r\n12 22\r\n1 2\r\n1 3\r\n2 4\r\n3 4\r\n3 5\r\n3 6\r\n4 6\r\n4 7\r\n4 8\r\n5 6\r\n6 7\r\n7 8\r\n5 9\r\n6 10\r\n7 10\r\n8 10\r\n9 10\r\n9 12\r\n10 12\r\n12 11\r\n10 11\r\n11 8\r\n9\r\n3 4 7 6\r\n4 1 2 3 4\r\n3 3 5 6\r\n3 4 7 8\r\n4 5 9 10 6\r\n3 7 8 10\r\n3 8 10 11\r\n3 9 12 10\r\n3 12 11 10";

        protected override string Output => "3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= C; i++)
            {
                var splits = input[i].Split(' ');
                int p = int.Parse(splits[0]), m = int.Parse(splits[1]);
                j++;
                List<HashSet<int>> parksStreets = [];
                int k;
                for(k = 0; k < m; k++)
                {
                    j++;
                }
                p = int.Parse(input[j]);
                j++;
                for (k = 0; k < p; k++)
                {
                    var parkJunctions = input[j].Split(' ').Skip(1).Select(i => int.Parse(i) - 1).ToHashSet();
                    parksStreets.Add(parkJunctions);
                    j++;
                }
                List<List<int>> parkParks = [];
                for (k = 0; k < parksStreets.Count; k++)
                {
                    parkParks.Add(Enumerable.Range(0, parksStreets.Count)
                        .Where(l => k != l && parksStreets[l].Any(p2 => parksStreets[k].Contains(p2))).Distinct().ToList());
                }
                k = 1;
                while(k < parkParks.Count - 1)
                {
                    if (parkParks[k].Skip(1).All(p => parkParks[parkParks[k][0]].Contains(p)))
                    {
                        foreach(var park in parkParks[k])
                            parkParks[park].Remove(k);
                        parkParks[k].Clear();
                    }
                    k++;
                }
                output.Add(parkParks.Where(p => p.Count > 0).Min(p => p.Count).ToString());
            }
        }
    }
}
