namespace OPSS
{
    /* Difficulty: 3/5
     * Consider a software with K components and multiple patches.
     * Each patch can update some (not necessarily all) components to its specified version.
     * Patch v+1 can only be used if all components updated by that patch have version v.
     * Find out, whether is it possible to update all components of a given program to a specified
     * version. If so, write numbers of patches to use.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 10. 
     * First line of each data set contains 3 integers separated by a whitespace each.
     * They are, respectively: number of components K, 1 ≤ K ≤ 10, number of patches L, 1 ≤ L ≤ 10000,
     * and target software version V, 2 ≤ V ≤ 1000.
     * Following are L pairs of lines describing each patch.
     * First line of each patch contains two numbers separated by a whitespace.
     * They are, respectively: patch version VL, and number of updated components KL,
     * 2 ≤ VL ≤ V, 1 ≤ KL ≤ K. Second line of each patch contains indexes of updated components
     * separated by a whitespace. Updated components are sorted in ascending order.
     * Patches are indexed 1 to L in order of appearing in input.
     * 
     * Output
     * D lines, each containing numbers of patches to apply written in order of application.
     * All numbers must be separated by a whitespace each. If there is more than one answer,
     * write smallest one lexicographically. If no answer exists, write -1 instead.
     */
    public sealed class Laty : ProblemBase
    {
        protected override string Input => "2\r\n7 10 4\r\n4 2\r\n1 2\r\n3 4\r\n1 2 3 4\r\n2 2\r\n4 5\r\n2 3\r\n1 2 3\r\n3 2\r\n1 7\r\n3 3\r\n5 6 7\r\n2 2\r\n6 7\r\n2 3\r\n3 4 5\r\n2 7\r\n1 2 3 4 5 6 7\r\n4 5\r\n3 4 5 6 7\r\n3 4 3\r\n3 2\r\n1 2\r\n3 2\r\n2 3\r\n2 3\r\n1 2 3\r\n3 2\r\n1 3";

        protected override string Output => "3 4 2 1 7 6 10\r\n-1";

        class Patch
        {
            public int Number;
            public int Version;
            public required int[] Modules;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int D = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= D; i++)
            {
                List<Patch> patches = [];
                var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                int[] versions = new int[splits[0]];
                for (int k = 0; k < versions.Length; k++)
                    versions[k] = 1;
                int package = 1;
                List<int> order = [];
                for (int k = 0; k < splits[1]; k++)
                {
                    int version = int.Parse(input[j].Split(' ')[0]);
                    j++;
                    var modules = input[j].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                    if (modules.All(m => versions[m] == version - 1))
                    {
                        foreach (var m in modules)
                            versions[m]++;
                        order.Add(package);
                        var toApply = patches.FirstOrDefault(p => p.Modules.All(m => versions[m] == p.Version - 1));
                        while (toApply != null)
                        {
                            foreach (var m in toApply.Modules)
                                versions[m]++;
                            order.Add(toApply.Number);
                            patches.Remove(toApply);
                            toApply = patches.FirstOrDefault(p => p.Modules.All(m => versions[m] == p.Version - 1));
                        }
                    }
                    else
                        patches.Add(new Patch()
                        {
                            Number = package,
                            Version = version,
                            Modules = modules
                        });
                    package++;
                    j++;
                }
                while (patches.Count > 0)
                {
                    if (patches[0].Modules.All(m => versions[m] == patches[0].Version - 1))
                    {
                        foreach (var m in patches[0].Modules)
                            versions[m]++;
                        order.Add(patches[0].Number);
                    }
                    patches.RemoveAt(0);
                }
                if (versions.All(v => v == splits[2]))
                    output.Add(string.Join(" ", order));
                else
                    output.Add("-1");
            }
        }
    }
}
