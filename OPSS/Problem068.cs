namespace OPSS
{
    /* 3/5
     * 
Któż nie zna twierdzenia Pitagorasa?! Albo kto nie słyszał o liczbach pierwszych? Z pewnością nie
uczestnicy OPSS-esji!, którzy kochają matematykę! Oto zadanie, które zawiera oba te pojęcia...
Trójkąty, których boki (a, b, c) spełniają równanie Pitagorasa: a^2 + b^2 = c^2 i jednocześnie a, b, c są
liczbami całkowitymi, nazywane są trójkątami pitagorejskimi.
Jest ich nieskończenie wiele, a najmniejszym z nich jest tzw. "trójkąt egipski" o bokach: 3, 4, 5.
Jeżeli zwiększymy wymagania i zechcemy, aby dwa z boków były liczbami pierwszymi,
otrzymamy trójkąty, które można nazwać "doskonałymi trójkątami pitagorejskimi" (nie jest to
nazwa oficjalna), których również jest nieskończenie wiele, ale znacznie mniej niż "zwykłych"
trójkątów pitagorejskich. Posortowane według długości najkrótszego boku dają się łatwo
zidentyfikować. Wspomniany trójkąt egipski jest pierwszym w ciągu "doskonałych trójkątów
pitagorejskich".
Twoim zadaniem jest znalezienie wskazanego "doskonałego trójkąta pitagorejskiego".
Zadanie
Należy podać dwie liczby pierwsze, które są bokami n-tego doskonałego trójkąta pitagorejskiego.
Wejście
W pierwszym wierszu wejścia znajduje się liczba naturalna C, 1 ≤ C ≤ 1000, oznaczająca liczbę
zestawów danych. W następnych C wierszach podane są liczby całkowite oznaczające numery
doskonałych trójkątów. Numery trójkątów są liczbami naturalnymi z przedziału 1..4000.
Wyjście
Na wyjściu, w każdym z C wierszy należy wypisać, oddzielone pojedynczą spacją, dwie liczby
pierwsze m, n, m ≤ n, równe bokom trójkąta pitagorejskiego o podanym numerze.
     */
    public sealed class DoskonaleTrojkatyPitagorejskie : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n7";

        protected override string Output => "3 5\r\n61 1861";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> sieve = [3];
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int n = int.Parse(input[i]);
                int a = 0, c = 0;
                int j = 0, k = 0;
                while(j < n)
                {
                    if (k == sieve.Count)
                    {
                        int next = sieve[k - 1] + 2;
                        if (sieve.All(p => next % p != 0))
                            sieve.Add(next);
                    }
                    a = sieve[k]; 
                    c = ((a * a) >> 1) + 1;
                    int l = sieve[sieve.Count - 1];
                    while (sieve[sieve.Count - 1] < c)
                    {
                        if (sieve.All(p => l % p != 0))
                            sieve.Add(l);
                        l += 2;
                    }
                    if (sieve.Contains(c))
                        j++;
                    k++;
                }
                output.Add($"{a} {c}");
            }
        }
    }
}
