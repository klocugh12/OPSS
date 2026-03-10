namespace OPSS
{
    /* Difficulty: 2/5
     * 

Opis

Pewien bardzo znany poszukiwacz skarbów, pan Skardfinder, właśnie odkrył kolejną w swoim życiu tajemniczą mapę określającą położenie skarbu. Mapa jest bardzo specyficzna, zawiera jedynie szereg liczb, po dwie w wierszu, których jest bardzo dużo. Po wielu miesiącach uważnego studiowania mapy i okoliczności jej zdobycia, pan Skardfinder wreszcie odkrył, co liczby te oznaczają. Otóż każda para jest pojedynczą wskazówką, gdzie pierwsza liczba w wierszu oznacza kierunek, w którym należy iść, a druga ilość kroków, które trzeba wykonać w tym kierunku. Kierunki te są zakodowane następująco:

        0 - północ
        1 - południe
        2 - zachód
        3 - wschód
      

Kolejnym odkryciem było to, że liczenie kroków trzeba rozpocząć od pewnej studni. Teraz nie pozostało nic innego, jak tylko pojechać tam i zacząć chodzić, jak mapa wskazuje, a potem odkopać skarb. Ale... tych wskazówek może być nawet 100000, a kroków w każdej z nich nawet do 10000. Nikt rozsądny oczywiście nie będzie chodził tyle pieszo, a na pewno nie tak znany poszukiwacz skarbów, jak pan Skardfinder. Dał Ci więc zadanie napisania dla niego programu, który wskaże, jak osiągnąć skarb robiąc jak najmniej kroków (oczywiście poruszając się tylko zgodnie z etykietą poszukiwaczy skarbów tzn. tylko w kierunkach północ-południe, lub wschód-zachód), lub powie, że skarb znajduje się we wspomnianej studni (jeśli kroki zaprowadzą nas do punktu, z którego zaczynaliśmy liczyć). Teren, na którym szukamy skarbu jest pustym polem i w zasięgu mapy nie ma na nim żadnych przeszkód, które uniemożliwiałyby robienie kroków w którymkolwiek z kierunków.
Specyfikacja wejścia

Pierwsza linia wejścia zawiera liczbę całkowitą D (1 ≤ D ≤ 50), oznaczającą liczbę zestawów danych. Pierwsza linia każdego zestawu zawiera jedną liczbę całkowitą N (0 ≤ N ≤ 100000), oznaczającą liczbę wskazówek - par określających kierunek i ilość kroków do wykonania. Kolejnych N linii składa się z liczb a i b (0 ≤ a ≤ 3; 1 ≤ b ≤ 10000), które oznaczają pojedynczą wskazówkę.
Specyfikacja wyjścia

Dla każdego zestawu danych należy wypisać najkrótszą drogę zgodną z zasadami etykiety poszukiwaczy skarbów, lub słowo studnia, jeśli skarb znajduje się w studni. Drogę oznaczamy tak samo jak na mapie, przez pary kierunek i ilość kroków w tym kierunku wypisanych w jednej linii. Jeśli do skarbu prowadzi prosta droga, należy wypisać tylko jedną linię z kierunkiem i ilością kroków. Jeśli droga musi skręcać, to wtedy pan Skardfinder po przeprowadzeniu na miejscu wizji lokalnej prosił, aby najpierw kroki wykonywać w orientacji północ-południe, a dopiero gdy znajdziemy się na odpowiedniej wysokości w tej orientacji, zacząć robić kroki w orientacji wschód-zachód.

     */
    public sealed class Skarby : ProblemBase
    {
        protected override string Input => "3\r\n3\r\n1 1\r\n0 2\r\n3 1\r\n4\r\n0 1\r\n2 1\r\n1 1\r\n3 1\r\n2\r\n0 1\r\n0 2";

        protected override string Output => "0 1\r\n3 1\r\nstudnia\r\n0 3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 0; i < N; i++)
            {
                int c = int.Parse(input[j]);
                j++;
                int[] tally = new int[2];
                for(int k = 0; k < c; k++)
                {
                    var splits = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                    j++;
                    tally[splits[0] >> 1] += ((splits[0] % 2 == 0) ? 1 : -1) * splits[1]; 
                }
                if (tally.All(t => t == 0))
                    output.Add("studnia");
                else
                {
                    foreach (var t in Enumerable.Range(0, 2))
                        if(tally[t] != 0)
                            output.Add($"{(t << 1) + (tally[t] > 0 ? 0 : 1)} {Math.Abs(tally[t])}");
                }
            }
        }
    }
}
