namespace OPSS
{
    /* Difficulty: 3/5
     * 
Doszedłeś do etapu na którym czeka na Ciebie Cwany Lutek. Aby przejść dalej musisz poprawnie
odpowiedzieć na pytanie przez niego postawione. Test Lutka jest krótki i zawsze taki sam. Cwaniak
rzuca dwie liczby N i K, a Ty musisz odpowiedzieć, czy liczba sposobów wskazania K
przedmiotów ze zbioru wszystkich N przedmiotów (kolejność wskazywania nie ma znaczenia) jest
liczbą parzystą czy nieparzystą.
Wejście
W pierwszym wierszu znajduje się liczba d, określająca ilość zestawów danych, 1 ≤ d ≤ 1000.
Każdy zestaw znajduje się w osobnej linii i zawiera dwie liczby całkowite N i K, 0 ≤ N, K ≤
1000000000, oddzielone pojedynczą spacją.
Wyjście
Dla każdego zestawu danych w oddzielnej linii wyjścia powinieneś wypisać jedną literę 'P' jeśli
liczba sposobów jest liczbą parzystą lub 'N' jeśli jest liczbą nieparzystą.
     */
    public sealed class CwanyLutek : ProblemBase
    {
        protected override string Input => "3\r\n100 2\r\n7 7\r\n19 9";

        protected override string Output => "P\r\nN\r\nP";

        static string Lutek(int a, int b)
        {
            while (true)
            {
                b = Math.Min(b, a - b);
                if (b == 0)
                    return "N";
                if (b == 1)
                    return a % 2 == 0 ? "P" : "N";
                int x = 1;
                while (x < a)
                    x <<= 1;
                if (a - b <= a - (x >> 1))
                    a -= (x >> 1);
                else
                    return "P";
            } 

        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                output.Add(Lutek(a, b));
            }
        }
    }
}
