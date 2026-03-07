namespace OPSS
{
    /* 1/5
     * 
Zadanie
Napisz program, który:
● wczyta ze standardowego wejścia nieujemną liczbę całkowitą n,
● policzy cyfrę jedności w zapisie dziesiętnym liczby n!,
● wypisze wynik na standardowe wyjście.
Wejście
Pierwszy i jedyny wiersz standardowego wejścia zawiera dokładnie jedną nieujemną liczbę
całkowitą n, 0 ≤ n ≤ 30000.
Wyjście
W pierwszym i jedynym wierszu standardowego wyjścia Twój program powinien zapisać
dokładnie jedną cyfrę równą cyfrze jedności w zapisie dziesiętnym liczby n!.
     */
    public sealed class Silnia : ProblemBase
    {
        protected override string Input => "4";

        protected override string Output => "4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            string[] digits = ["1", "1", "2", "6", "4"];
            output.Add(N < 5 ? digits[N] : "0");
            
        }
    }
}
