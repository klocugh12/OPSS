namespace OPSS
{
    /* Difficulty: 3/5
     * 
W dawnych czasach, starożytna cywilizacja Bajteków do przewidywania zjawisk astronomicznych,
używała maszyny o nazwie Generis, która wyznaczała mozolnie kolejne wyrazy pewnego
rosnącego ciągu liczb. Maszyna była konfigurowalna, a na jej konfigurację składały się dwie liczby
całkowite dodatnie, tzw. pierwsza i druga liczba konfiguracji. Pierwszym wyrazem ciągu
generowanego przez maszynę była suma liczb z jej konfiguracji, drugim wyrazem, suma
pierwszego wyrazu oraz drugiej liczby konfiguracji, natomiast od trzeciego wyrazu ciągu, każdy
wyraz był sumą dwóch poprzednio wygenerowanych wyrazów.
Przy odpowiedniej konfiguracji maszyna potrafiła generować ciąg Fibonacciego (co prawda nie od
pierwszego elementu) i był to w zasadzie jedyny ciąg, który znalazł u Bajteków praktyczne
zastosowanie. Konfigurowalność maszyny nie była zbyt przydatna Bajtekom, a przynajmniej nie
mieli pojęcia jak ją dobrze wykorzystać. Potrafili dostrzec wszechobecność ciągu Fibonacciego w
przyrodzie i za pomocą maszyny dokonywali m. in. przewidywania pogody. Konfigurowaniem
maszyny zajmowali się jedynie naukowcy, próbujący co jakiś czas odnaleźć praktyczne
zastosowanie innych ciągów generowanych przez maszynę.
Aby odkryć zastosowania innych ciągów generowanych przez maszynę naukowcy potrzebowali
grantów na badania nad nią. Postanowili zorganizować prezentację dla możnych Kraju Bajteków,
aby pokazać im działanie maszyny i wyjaśnić jakie można by uzyskać z niej korzyści, gdyby
badania nad ciągami generowanymi przez maszynę przyniosły efekty.
Kierujący badaniami profesor Hexos postanowił, że maszyna wygeneruje takie ciągi, w których
będą występowały pewne bardzo szczególne dla matematycznej kultury Bajteków liczby. Każdy
pokaz będzie polegał na odpowiednim skonfigurowaniu maszyny i wykonywaniu obliczeń, aż do
momentu uzyskania pewnej zadanej i podanej publiczności przed konfiguracją liczby. Profesor
chce, aby obliczenia były efektowne, oraz aby wyglądały na jak najbardziej skomplikowane
(wywarcie takiego wrażenia na publiczności ma ułatwić pozyskanie grantu).
Profesor nie ma czasu aby dla każdej liczby, jaką chce uzyskać w kolejnych pokazach prezentacji,
wyznaczyć taką konfigurację maszyny, aby dokonywała ona obliczeń możliwie długo. To zadanie
zlecił swojemu asystentowi, czyli Tobie!
Wejście
W pierwszym wierszu występuje jedna liczba całkowita C (1 ≤ C ≤ 5000) określająca ilość
pokazów zaplanowanych przez profesora. W i+1 (i = 1, 2, ..., C) wierszu znajduje się liczba N (1 ≤
N ≤ 2*10^9), której wygenerowanie założył sobie profesor.
Wyjście
Dla każdej liczby N wypisz taką konfigurację maszyny, w której liczba N będzie wyznaczona przez
maszynę możliwie najpóźniej. Dla każdego zestawu wypisz w osobnej linii pierwszą i drugą liczbę
konfiguracji oddzielone pojedyncza spacją. Jeśli istnieje więcej niż jedna konfiguracja, która
gwarantuje najdłuższy czas obliczeń, podaj tę, dla której pierwsza liczba konfiguracji jest
najmniejsza. W przypadku, gdy konfiguracja maszyny nie istnieje wypisz jedno słowo: BRAK.
Pamiętaj, że kolejność liczb w konfiguracji ma znaczenie.
     */
    public sealed class Generis : ProblemBase
    {
        protected override string Input => "2\r\n3\r\n10";

        protected override string Output => "1 1\r\n2 2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> fib = [1, 1];
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]);
                while (a > fib[fib.Count - 1])
                    fib.Add(fib[fib.Count - 1] + fib[fib.Count - 2]);
                int n = fib.Count - 1;
                while (n > 0 && a > (fib[n] + fib[n - 1]))
                    n--;
                bool found = false;
                while (n > 0 && !found)
                {
                    for (int j = 1; j <= (a / fib[n]); j++)
                        for (int k = 1; k <= (a / fib[n]); k++)
                            if (j * fib[n - 1] + k * fib[n] == a)
                            {
                                output.Add($"{j} {k}");
                                found = true;
                                j = a;
                                break;
                            }
                    if (!found)
                        n--;
                }
                if (n == 0)
                    output.Add("BRAK");
            }
        }
    }
}
