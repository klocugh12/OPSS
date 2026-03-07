namespace OPSS
{
    /* 1/5
     * 
Liczba pierwsza to liczba naturalna większa od 1, która dzieli się tylko przez 1 i przez siebie samą
(a zatem ma dokładnie dwa dzielniki naturalne).
Zadanie
Napisz program, który wyznacza n-tą w kolejności liczbę pierwszą.
Wejście
Pierwsza linia zawiera dokładnie jedną liczbę C, 1<=C<=200000, będącą liczbą zestawów danych.
W C kolejnych liniach występują poszczególne zestawy danych. Każdy zestaw składa się z jednej
liczby naturalnej 1<=n<=15000.
Wyjście
Program powinien wypisać na standardowe wyjście C linii. I-ta linia powinna zawierać dokładnie
jedną liczbę naturalną, będącą n-tą w kolejności liczbą pierwszą dla zestawu o numerze I
     */
    public sealed class LiczbyPierwsze : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n2\r\n5\r\n7";

        protected override string Output => "5\r\n3\r\n11\r\n17";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> primes = [3];

            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[i]);
                if (n == 1)
                    output.Add("2");
                else
                {
                    int candidate = primes.Last() + 2;
                    while (primes.Count < n - 1)
                    {
                        if (!primes.Any(p => candidate % p == 0))
                            primes.Add(candidate);
                        else
                            candidate += 2;
                    }
                    output.Add(primes[n - 2].ToString());
                }
            } 
        }
    }
}
