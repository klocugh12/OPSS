namespace OPSS
{
    /* 3/5
     * 
Żabka porusza się po prostej ścieżce złożonej z n pól. Startowe położenie to pole numer 1, a
końcowe - n. Żabka porusza się tylko w kierunku pola końcowego. Żabka może przesunąć się
jednym skokiem minimalnie o kmin pól, a maksymalnie o kmax pól.
Zadanie
Napisz program, który obliczy na ile sposobów żabka może osiągnąć swoje położenie końcowe,
jeżeli porusza się tak że każdy następny skok jest co najmniej takiej samej długości jak poprzedni.
Wejście
Pierwsza linia zawiera dokładnie jedną liczbę m, 1 ≤ m ≤ 100, będąca liczbą zestawów danych. W
m kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z liczb
naturalnych n, kmin, kmax, oddzielonych pojedynczą spacją. (2 ≤ n ≤ 1000, 1 ≤ kmin ≤ kmax ≤
1000).
Wyjście
Program powinien wypisać na standardowe wyjście m linii. I-ta linia powinna zawierać dokładnie
jedną liczbę naturalną, będąca liczbą możliwych ruchów dających żabce osiągniecie pola
końcowego. Liczba ta nie będzie większa od 2^31.
     */
    public sealed class Zabka : ProblemBase
    {
        protected override string Input => "4\r\n10 2 5\r\n15 5 8\r\n20 1 20\r\n22 2 7";

        protected override string Output => "5\r\n2\r\n490\r\n72";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int n = int.Parse(splits[0]), kmin = int.Parse(splits[1]), kmax = Math.Min(n - 1, int.Parse(splits[2]));
                int[] tab = new int[n];
                for (int k = kmin; k <= kmax; k++)
                {
                    tab[k]++;
                    for (int j = k + 1; j < n; j++)
                        tab[j] += tab[j - k];
                }
                output.Add(tab[n - 1].ToString());
            }
        }
    }
}
