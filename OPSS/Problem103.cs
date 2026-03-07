namespace OPSS
{
    /* 3/5
     * 
Zdefiniujmy pojęcie grafu animalnego. Obrazowo można opisać graf animalny, jako graf który
odpowiednio narysowany na płaszczyźnie może przedstawiać schemat czworonoga.
A bardziej formalnie:
● Graf G nazywamy grafem animalnym, jeśli spełnia następujące warunki:
● G jest prosty, spójny, zawiera dokładnie 2 cykle, każdy o długości co najmniej 3.
● Oba cykle są rozłączne, połączone są pojedynczą ścieżką, zawierającą co najmniej 1
krawędź (ścieżka ta będzie szyją czworonoga).
● Jeden z cykli jest krótszy od drugiego. Krótszy cykl nazywać będziemy głową, dłuższy
tułowiem.
● Dokładnie 2 wierzchołki tułowia mają stopień 4. Do każdego z tych wierzchołków
dołączone są 2 ścieżki równej długości (są to 2 pary nóg naszego czworonoga).
● 1 lub 2 wierzchołki tułowia mają stopień 3. Jeden z nich to wierzchołek łączący tułów z
głową, a ewentualny drugi wierzchołek stopnia 3 to miejsce w którym dołączona do tułowia
jest jeszcze jedna ścieżka (to ogon naszego czworonoga - nie każdy czworonóg ma ogon).
● Pozostałe wierzchołki tułowia mają stopnie równe 2.
● Wszystkie wierzchołki głowy mają stopień 2, za wyjątkiem jednego, łączącego głowę z
tułowiem. Ma on stopień 3.
Rys. Przykłady grafów animalnych.
Najmniejszy graf animalny ma 11 wierzchołków. Twoim zadaniem będzie badanie animalności
grafów.
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
                List<List<int>> nodes = [];
                for (int k = 0; k < n; k++)
                    nodes.Add([]);
                for (int k = 0; k < n - 1; k++)
                {
                    foreach(var node in input[j].Split(' ').Skip(1).Select(s => int.Parse(s) - 1))
                    {
                        nodes[node].Add(k);
                        nodes[k].Add(node);
                    }
                    j++;
                }
                bool isAnimal = true;
                isAnimal = isAnimal && TrimLegsAndTail(nodes);
                isAnimal = isAnimal && HasTwoCyclesAndNeck(nodes);
                output.Add(isAnimal ? "tak" : "nie");
            }
        }

        static bool HasTwoCyclesAndNeck(List<List<int>> nodes)
        {
            if (nodes.Count < 6)
                return false;
            nodes.RemoveAll(n => n.Count == 0);
            nodes.Sort((a, b) => -a.Count.CompareTo(b.Count));
            return nodes[0].Count == 3 && nodes[1].Count == 3 && nodes.Skip(2).All(n => n.Count == 2);
        }

        static bool TrimLegsAndTail(List<List<int>> nodes)
        {
            if (nodes.Count < 11)
                return false;
            int[] ones = Enumerable.Range(0, nodes.Count).Where(j => nodes[j].Count == 1).ToArray();
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
