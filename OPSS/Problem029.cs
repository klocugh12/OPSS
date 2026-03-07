namespace OPSS
{
    /* 2/5
     * Grupa archeologów z Bitlandii zorganizowała wyprawę naukową na Półwysep Bajtocki w celu
przeprowadzenia wykopalisk. Podejrzewali znaleźć tam szczątki pradawnej cywilizacji
Andorxorów, znanej powszechnie z posiadania wysokiego poziomu maszyn liczących. Już drugiego
dnia wykopalisk wydawało się, że uczeni odnieśli sukces: udało się im wydobyć dziwne
urządzenie, które z wyglądu przypominało prehistoryczny komputer! Szczęśliwi badacze byli
przekonani, że teraz będą mogli rozszyfrować wszystkie zagadki Andorxorów i zgłębić wszystkie
tajniki ich przeogromnej wiedzy. Jednak zadanie to okazało się dużo trudniejsze niż mogli
kiedykolwiek przypuszczać. Po uruchomieniu maszyny pojawił się napis:
Witaj w świecie Andorxorów!
f0 = 0,
fn = n^3 * (fn-1+1) + n^2, n > 0
n = 54128
fn mod 3331 = ???
... a po dwóch sekundach maszyna wyłączyła się. Po ponownym uruchomieniu pojawiał się
analogiczny napis, lecz różniący się od poprzedniego zadaną wartością n. Archeolodzy zaczęli
wpisywać losowe liczby w odpowiedzi na pytanie zadane przez maszynę myśląc, że uda im się ją
oszukać. Jednak nic z tego nie wyszło - błędna odpowiedż powodowała ponowne uruchomienie
"komputera", to zaś generowało kolejne zapytanie, za każdym razem dla innego n.
Uczonym nie pozostało nic innego, tylko mozolnie obliczać wartości fn mod 3331 dla kolejnych n
tak, aby móc odpowiednie wyniki wprowadzić do maszyny przed upływem dwóch sekund... Jest to
jednak dla nich ogromnie trudne zadanie...
Zadanie
Pomóż archeologom uruchomić prehistoryczną maszynę.
Wejście
W pierwszej linijce wejścia znajduje się liczba C, 1 ≤ C ≤ 5, oznaczająca liczbę zestawów danych.
W kolejnych C wierszach wejścia znajdują się zestawy danych. Każdy zestaw składa się z liczby
naturalnej n, 0 ≤ n ≤ 100000000.
Wyjście
Dla każdego zestawu danych, dla liczby n, na wyjściu powinna znaleźć się wartość fn mod 3331,
będąca hasłem do uruchomienia urządzenia.
     */
    public sealed class PrehistorycznyKomputer : ProblemBase
    {
        protected override string Input => "3\r\n2\r\n10\r\n225";

        protected override string Output => "28\r\n254\r\n959";

        const int K = 3331;

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> tab = [0];
            for(int i = 1; i < K; i++)
            {
                tab.Add((i * i % K) * (i * (tab[tab.Count - 1] + 1) + 1) % K);
            }
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int c = int.Parse(input[i]);
                output.Add(tab[c % K].ToString());
            }
        }
    }
}
