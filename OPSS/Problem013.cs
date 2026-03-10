namespace OPSS
{
    /* Difficulty: 3/5
     * Mamy do dyspozycji szachownicę nieskończonej wielkości oraz znajdujące się na niej pionki
ułożone w prostokąt (wypełniony) o wymiarach m x n (1 ≤ m, n ≤ 1000). Pionki leżą na polach
planszy. Jeden pionek leży dokładnie na jednym polu planszy.
Rozpoczynamy rozgrywkę dla jednego gracza, zgodnie z następującymi zasadami gry. Każdy
pionek może przeskoczyć nad innym pionkiem ("zbić go") wzdłuż linii poziomej lub pionowej na
planszy. Pionka, który został zbity, usuwamy z planszy i wykluczamy go z dalszej gry. Celem gry
jest uzyskanie jak najmniejszej liczby pionków na szachownicy.
Zadanie
Mając zadaną parę liczb m oraz n, oddzielonych od siebie w pliku wejściowym spacją, należy
napisać program, który określi najmniejszą możliwą liczbę pionków pozostałych na szachownicy.
Wejście
Liczby m i n oddzielone spacją.
Wyjście
Najmniejsza liczba pionków pozostałych na szachownicy.
     */
    public sealed class ProstaGra : ProblemBase
    {
        protected override string Input => "3 4";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
            //rządek 3xn się redukuje do 3x(n - 1), ale dla każdej nieparzystej 2xn zostaną dwie.
            //Dla parzystej n przy 2xn zostanie tylko jedna.
            if (a == 1 || b == 1)
                output.Add(((Math.Max(a, b) + 1) >> 1).ToString());
            else
                output.Add((a % 3 == 0 || b % 3 == 0 || ((a == 2 && b % 2 == 1) || (b == 2 && a % 2 == 1))) ? "2" : "1");
        }
    }
}
