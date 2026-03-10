namespace OPSS
{
    /* Difficulty: 4/5
     * Tomek, podejrzewając swojego szefa o malwersacje finansowe, postanowił włamać się do
firmowego sejfu i udowodnić mu przestępstwo. Jednak taki plan posiada kilka słabych punktów.
Jednym z nich jest włamanie się do sejfu, który posiada cyfrowy zamek. Będąc małym chłopcem
Tomek często oglądał MacGyver'a, który w jednym z odcinków miał podobny problem. Na filmie
udało mu się rozwiązać go w następujący sposób: za pomocą mąki stwierdził, które klawisze na
zamku są tłuste (a więc używane) - dmuchnął mąką w klawiaturę i do tych tłustych klawiszy
przykleiła się mąka ;) - a następnie, wiedząc jak długi jest kod, wystukał po kolei wszystkie
możliwe kombinacje. Tomek postanowił skorzystać z podobnej metody. Jednak po kupieniu mąki i
stwierdzeniu jakie klawisze są używane, zaczął się zastanawiać ile czasu zajmie mu dostanie się do
środka. Twoim zadaniem jest oszacowanie tego czasu (przy założeniu że na jedną kombinację
potrzebuje 1 sekundy).
Wejście
W pierwszej linii znajduje się liczba naturalna D, 1 ≤ D ≤ 100, oznaczająca liczbę zestawów
danych. Każdy zestaw składa się z jednej linii zawierającej dwie liczby całkowite: K, 1 ≤ K ≤ 10,
oraz N, K ≤ N ≤ 24, gdzie K oznacza liczbę różnych cyfr, zaś N długość kodu.
Wyjście
Dla każdego zestawu danych wypisz w osobnej linii czas jaki zajmie Tomkowi włamanie się do
sejfu w formacie: dni:godziny:minuty:sekundy
     */
    public sealed class Wlamanie : ProblemBase
    {
        protected override string Input => "2\r\n3 4\r\n4 4";

        protected override string Output => "0:0:0:36\r\n0:0:0:24";

        static int[] factors = [24, 60, 60];

        void mul(int[] time, int number)
        {
            int[] carries = [0, 0, 0];
            for (int i = 0; i < time.Length; i++)
            {
                time[i] *= number;
                if (i > 0 && time[i] > factors[i - 1])
                {
                    carries[i - 1] = time[i] / factors[i - 1];
                    time[i] %= factors[i - 1];
                }
            }
            for (int i = carries.Length - 1; i >= 0; i--)
            {
                time[i] += carries[i];
                if (i > 0 && time[i] >= factors[i - 1])
                {
                    carries[i - 1]++;
                    time[i] %= factors[i - 1];
                }
            }
        }

        void add(int[] time, int[] time2)
        {
            int[] carries = [0, 0, 0];
            for (int i = 0; i < time.Length; i++)
            {
                time[i] += time2[i];
                if (i > 0 && time[i] > factors[i - 1])
                {
                    carries[i - 1] = time[i] / factors[i - 1];
                    time[i] %= factors[i - 1];
                }
            }
            for (int i = carries.Length - 1; i >= 0; i--)
            {
                time[i] += carries[i];
                if (i > 0 && time[i] >= factors[i - 1])
                {
                    carries[i - 1]++;
                    time[i] %= factors[i - 1];
                }
            }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                int[] time = [0, 0, 0, 0];
                int[] factors = [24, 60, 60];
                List<(int, int, int[])> options = [(1, b - 1, [a])];
                while (options[0].Item2 > 0)
                {
                    for (int j = 0; j < options.Count; j++)
                    {
                        var opt = options[j];
                        options.RemoveAt(j);
                        options.Insert(j, (Math.Min(opt.Item1 + 1, a), opt.Item2 - 1, opt.Item3.Concat(opt.Item1 < a ? [a - opt.Item1] : [a]).ToArray()));
                        if (opt.Item1 < a && opt.Item2 > a - opt.Item1)
                        {
                            options.Insert(j, (opt.Item1, opt.Item2 - 1, opt.Item3.Concat(opt.Item1 > 1 ? [opt.Item1] : []).ToArray()));
                            j++;
                        }
                    }
                }
                foreach(var opt in options)
                {
                    int[] timeTemp = [0, 0, 0, 1];
                    foreach (var f in opt.Item3)
                        mul(timeTemp, f);
                    add(time, timeTemp);
                }
                output.Add(string.Join(":", time));
            }
        }
    }
}
