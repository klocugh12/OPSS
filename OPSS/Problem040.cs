namespace OPSS
{
    /* Difficulty: 4/5
     * Wejście:
W pierszym wierszu znajduje się liczba zestawów danych 0 < d ≤ 100. Każdy zestaw składa się z
dwóch liczb: liczby pierwszej P, 2 ≤ P ≤ 1000000 i H, 0 ≤ H ≤ 10000 oddzielonych spacją.
Wyjście:
Dla każdego zestawu danych powinieneś wypisać liczbę pól (liczb) na planszy (w trójkącie Pascala
o wysokości H), które nie dzielą sie przez P.
     */
    public sealed class Lotki : ProblemBase
    {
        protected override string Input => "3\r\n2 2\r\n3 4\r\n7 6";

        protected override string Output => "5\r\n12\r\n28";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int p = int.Parse(splits[0]), h = int.Parse(splits[1]) + 1;
                if(p >= h)
                {
                    output.Add(((h * (h + 1)) >> 1).ToString());
                    continue;
                }
                int total = 1;
                int pp = p;
                int add = 2;
                int pow = 1;
                while (pp * p <= h)
                {
                    total += add;
                    add = (add << 1) + 2;
                    pp *= p;
                    pow++;
                }
                pp = h - pp;
                add = 0;
                for(int k = 0; k < pp / p; k++)
                {
                    add = (add << 1) + 2;
                }
                total += add;
                total *= (p * (p + 1)) >> 1;
                total += (1 << pow) * ((pp % p) * ((pp % p) + 1) >> 1);
                output.Add(total.ToString());
            }
        }
    }
}
