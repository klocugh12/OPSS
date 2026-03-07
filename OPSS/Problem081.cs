namespace OPSS
{
    /* 2/5
     * Mieszkańcy Opsslandii planują budowę Instytutu Badań nad Figurami Foremnymi (w skrócie
IBFF). Ma to być przestronny budynek o podstawie kwadratowej, zbudowany na specjalnych
kwadratowych płytach o rozmiarach a x a metrów, sprowadzonych z odległych zakątków kraju.
Płyty mają przylegać do siebie bocznymi krawędziami i formować kwadrat - podobnie jak na
szachownicy. Niestety część płyt uległa zniszczeniu podczas transportu i dowieziono tylko k płyt.
Mieszkańcy Opsslandii bardzo się tym faktem zmartwili, bo płyt nie wystarczy na zbudowanie
Instytutu zakładanej wielkości. Dowiezienie płyt też nie wchodzi w grę, ponieważ transport płyt
trwa bardzo długo i jest niezmiernie kosztowny. Postanowili zatem, że zbudują największy budynek
z dowiezionych płyt zachowując kwadratową podstawę Instytutu.
Pomóż mieszkańcom Opsslandii ustalić maksymalną możliwą długość ściany budynku, wiedząc że
płytek nie można ciąć.
Wejście
W pierwszej linii podana jest jedna liczba całkowita C, oznaczająca liczbę zestawów danych
wejściowych (1 ≤ C ≤ 5000). W linii i+1 (i = 1, 2, ..., C) podane są dwie liczby całkowite: k,
oznaczająca ilość płyt (1 ≤ k ≤ 2^31-1) oraz a, oznaczająca długość boku płyty (1 ≤ a ≤ 2^31-1).
Wyjście
Dla każdego zestawu, w osobnych liniach wyjścia, powinna pojawić się jedna nieujemna liczba
całkowita, oznaczająca maksymalną możliwą długość ściany budynku, wyrażoną w metrach.
     */
    public sealed class Plyty : ProblemBase
    {
        protected override string Input => "2\r\n10 3\r\n4 5";

        protected override string Output => "9\r\n10";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                output.Add((b * (int)Math.Sqrt(a)).ToString());
            }
        }
    }
}
