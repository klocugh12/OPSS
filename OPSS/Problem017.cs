namespace OPSS
{
    /* 1/5
     * 
Frma MIRACLE, znany potentat na rynku baz danych, opracowała nowy sposób sortowania, który
będzie zastosowany w najnowszym produkcie o nazwie kodowej Miracle 13k. W obecnej wersji
metoda pozwala sortować niemalejąco tylko liczby naturalne z przedziału 1..1000.
Algorytm polega na zamianie miejscami dwóch sąiednich elementów. Na przykład - jeśli chcemy
posortować ciąg 3 1 2 to musimy zamienić miejscami 1 i 3 - otrzymamy ciąg 1 3 2 - nastepnie 3 - 2.
Wykonaliśmy zatem 2 zamiany. W celu wytestowania metody potrzebny jest program, który
policzy minimalną liczbę zamian potrzebną do posortowania ciągu liczb z przedziału 1..1000.
Niestety, programista pracujący nad algorytm miał wypadek i zadanie to powierzono Tobie.
Wejście
W pierwszej linii znajduje się liczba 1≤N≤1000 określająca ilość liczb w ciągu. W drugiej lini jest
N liczb z przedziału 1..1000 rozdzielonych spacją.
Wyjście
Na wyjściu powinna się znaleźć jedna liczba określająca minimalną liczbę zamian w ciągu
wejściowym.
     */
    public sealed class Sortowanie : ProblemBase
    {
        protected override string Input => "3\r\n3 1 2";

        protected override string Output => "2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int[] tab = input[1].Split(' ').Select(s => int.Parse(s)).ToArray();
            int swaps = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (tab[j] < tab[j - 1])
                    {
                        int temp = tab[j];
                        tab[j] = tab[j - 1];
                        tab[j - 1] = temp;
                        swaps++;
                    }
                }
            }
            output.Add(swaps.ToString());
        }
    }
}
