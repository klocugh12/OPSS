namespace OPSS
{
    /* 2/5
     * 
Twoim zadaniem jest znaleźć najmniejszą dodatnią liczbę całkowitą Q, tak aby iloczyn jej cyfr był
równy N.
Wejście
Wejście zawiera dokładnie jedną liczbę naturalną N (0 ≤ N ≤ 10^9).
Wyjście
Twój program powinien wypisać dokładnie jedną liczbę Q spełniającą warunki zadania. Jeśli taka
liczba nie istnieje program powinien wypisać liczbę -1.
     */
    public sealed class IloczynCyfr : ProblemBase
    {
        protected override string Input => "10";

        protected override string Output => "25";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            if(N < 10)
            {
                output.Add(input[0]);
                return;
            }
            List<int> primes = [2, 3, 5, 7];
            int i = 0;
            List<int> factors = [];
            while (i < primes.Count)
            {
                while(N % primes[i] == 0)
                {
                    factors.Add(primes[i]);
                    N /= primes[i];
                }
                i++;
            }
            if(N > 1)
            {
                output.Add("-1");
                return;
            }
            output.Add(string.Join("", factors));
        }
    }
}
