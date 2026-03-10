namespace OPSS
{
    /* Difficulty: 1/5
     * 
Wejście
W pierwszej linii wejścia znajduje się liczba C, określająca liczbę zestawów danych, 1 ≤ C ≤ 100.
W kolejnych wierszach wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa
się z wiersza zawierającego niepusty ciąg nawiasów, który wydrukowała zepsuta maszyna
drukarska. Długość ciągu nie przekracza 1000 znaków.
Wyjście
Dla każdego zestawu danych, w osobnej linii wyjścia, należy wypisać liczbę, określającą liczbę
nawiasów które należy wstawić do zadanego ciągu, aby otrzymać poprawne nawiasowanie.
     */
    public sealed class MaszynaDrukarska : ProblemBase
    {
        protected override string Input => "2\r\n)\r\n(()))()(()))";

        protected override string Output => "1\r\n2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int l = 0, p = 0;
                for (int j = 0; j < input[i].Length; j++) 
                {
                    if (input[i][j] == '(')
                        l++;
                    else
                        p++;
                }
                output.Add(Math.Abs(l - p).ToString());
            }
        }
    }
}
