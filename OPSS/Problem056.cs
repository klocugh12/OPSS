namespace OPSS
{
    /* 2/5
     * 
Wejście
W pierwszej linii znajduje się liczba osób B, 1≤ B≤100000 , które zakupiły bilet na danej trasie. W
kolejnych B liniach znajdują się liczby całkowite nieujemne: t0 i t1, 0≤ t0< t1≤ 2^31 - 1; oddzielone
pojedynczą spacją. Są to odpowiednio: czas wejścia do pociągu oraz czas wyjścia z pociągu
kolejnej osoby.
Wyjście
Na standardowym wyjściu powinna być wypisana jedna liczba, będącą maksymalną ilością osób
znajdujących się równocześnie w pociągu.
     */
    public sealed class LublinKrakow : ProblemBase
    {
        protected override string Input => "3\r\n1 2\r\n2 3\r\n2 4";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<int> ins = [], outs = [];
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                ins.Add(int.Parse(splits[0]));
                outs.Add(int.Parse(splits[1]));
            }
            ins.Sort();
            outs.Sort();
            int inIndex = 0, outIndex = 0;
            int count = 0;
            while (inIndex < ins.Count)
            {
                while (outs[outIndex] <= ins[inIndex])
                {
                    count--;
                    outIndex++;
                }
                count++;
                inIndex++;
            }
            output.Add(count.ToString());
        }
    }
}
