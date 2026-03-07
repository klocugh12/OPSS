namespace OPSS
{
    /* 1/5
     * W pewnej firmie informatycznej znajdują się serwery gromadzące duże ilości danych. W związku z
przebudową infrastruktury technicznej podjęto decyzję o przeniesieniu i umieszczeniu danych na
jednym serwerze. Każde dwa serwery połączone są bezpośrednim łączem. Każdy z serwerów w
danym momencie może albo wysyłać dane tylko do jednego serwera, albo odbierać dane tylko od
jednego serwera. Każdy serwer może pomieścić dane znajdujące się na wszystkich serwerach. Cała
operacja przenoszenia danych powinna zostać przeprowadzona tak, aby trwała jak najkrócej. Czas
przenoszenia 1MB danych pomiędzy dwoma serwerami jest stały i wynosi 1s.
Zadanie
Napisz program, który dla zadanej konfiguracji serwerów (rozmiar przechowywanych danych)
wyznaczy minimalną liczbę sekund, potrzebnych do wykonania tej operacji.
Wejście
Pierwsza linia wejścia zawiera liczbę zestawów danych C (1 ≤ C ≤ 100). W kolejnych wierszach
wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa się z dwóch wierszy.
Pierwszy wiersz zestawu zawiera liczbę naturalną n określającą liczbę serwerów (1 ≤ n ≤ 100).
Drugi wiersz zestawu danych zawiera n liczb całkowitych: a1, ..., an, oddzielonych pojedynczą
spacją. Liczba ai (i = 1, ..., n; 0 ≤ ai < 2^31) określa rozmiar danych w MB przechowywanych przez i-
ty serwer.
Wyjście
Dla każdego zestawu danych, w kolejnych liniach wyjścia, należy wypisać minimalną liczbę
sekund potrzebnych do wykonania operacji przenoszenia danych na jeden serwer.
     */
    public sealed class Serwery : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n4 10 8 3\r\n2\r\n1 1";

        protected override string Output => "15\r\n1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= N; i++)
            {
                j++;
                var data = input[j].Split(" ").Select(s => int.Parse(s)).ToList();
                j++;
                data.Remove(data.Max());
                output.Add(data.Sum().ToString());
            }
        }
    }
}
