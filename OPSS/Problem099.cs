namespace OPSS
{
    /* 1/5
     * 
Numery telefonów w Opsslandii są zapisywane w postaci wyrazów, aby książki telefoniczne były
bardziej przystępne dla przeciętnego czytelnika. Każdej literze wyrazu reprezentującego numer
telefonu odpowiada jedna cyfra.
ABC: 2
DEF: 3
GHI: 4
JKL: 5
MNO: 6
PQRS: 7
TUV: 8
WXYZ: 9
Poza Opsslandią numery telefoniczne zapisywane są w postaci ciągu cyfr i osoby spoza Opsslandii
mają trudności z rozkodowaniem numerów wyrazowych.
Napisz program tłumaczący wyrazowe numery telefonów na ich odpowiedniki cyfrowe.
Wejście
Pierwszy wiersz zawiera dodatnią liczbę całkowitą C (1 ≤ C ≤ 100000) określającą ilość
wyrazowych numerów telefonicznych.
W każdym z kolejnych C wierszy znajduje się wyraz do przetłumaczenia. Każdy wyraz zawiera co
najmniej 1 i co najwyżej 100 liter.
Wyjście
Dla każdego wyrazowego numeru telefonu należy, w osobnej lini, wypisać odpowiadający mu
numer telefonu w postaci ciągu cyfr
     */
    public sealed class Komorka : ProblemBase
    {
        protected override string Input => "1\r\nOPSS";

        protected override string Output => "6777";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            Dictionary<char, int> code = new(){ { 'A', 2 }, { 'B', 2 }, { 'C', 2 }, { 'D', 3 }, { 'E', 3 }, { 'F', 3 }, { 'G', 4 },
                { 'H', 4 },{ 'I', 4 },{ 'J', 5 },{ 'K', 5 },{ 'L', 5 },{ 'M', 6 },{ 'N', 6 },{ 'O', 6 },{ 'P', 7 },
            { 'Q', 7 },{ 'R', 7 },{ 'S', 7 },{ 'T', 8 },{ 'U', 8 },{ 'V', 8 },{ 'W', 9 },{ 'X', 9 },{ 'Y', 9 },{ 'Z', 9 },};
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                output.Add(string.Join("", input[i].Select(s => code[s])));
            }
        }
    }
}
