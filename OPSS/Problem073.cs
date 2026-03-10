namespace OPSS
{
    /* Difficulty: 3/5
     * Janek, starszy kolega Edka (znanego z hodowli chomików) z Wydziału Biologii, pracuje nad
doktoratem. W tym celu musi hodować pewien rodzaj alg i oszacować tempo ich wzrostu. Początek
hodowli wydawał się prosty. "Kolonia" alg umieszczona w akwarium po jednej dobie zwiększyła
swoją objętość czterokrotnie, to znaczy, że młodych alg było 3 razy więcej niż ich "matek", ale po 5
dniach młodych było "prawie" 600 razy więcej niż na początku, zatem reguła czterokrotnego
wzrostu okazała się fałszywa. Dokładniejsze badania wykazały, że komórki alg "matek" w ciągu
doby pączkując dają życie czterem nowym komórkom, poprzednie pokolenie alg ("babki") ginąc
wydzielają toksynę, która hamuje wzrost młodych w stosunku 1:1, to znaczy ginie tyle samo
starych co nowych komórek. Na początku hodowli nie było co prawda starych alg, ale widocznie
hodowla dopiero przystosowywała się do nowego środowiska stąd wolniejszy wzrost. Szczegóły
rozwoju alg najlepiej prześledzić na rysunku. Ponieważ Janek, jak to biolog, nie jest zbyt sprawny
w rachunkach, więc oszacowanie wielkości hodowli (np. po kilku miesiącach) przerasta jego
możliwości. Pomóż ambitnemu badaczowi!
Zadanie
Należy oszacować "mnożnik", czyli liczbę komórek młodych alg wywodzących się z jednej
komórki macierzystej, po zadanej liczbie dni.
Wejście
W pierwszym wierszu znajduje się liczba N, 0 < N ≤ 10000 oznaczająca liczbę zestawów danych.
W każdym z kolejnych N wierszy znajduje się liczba dni hodowli Di, 0 ≤ Di ≤ 200000.
Wyjście
Na wyjściu, w oddzielnych wierszach, należy wypisać dwie liczby całkowite N, C, oddzielone
jedną spacją. N oznacza liczbę cyfr, z których składa się poszukiwany przez Janka mnożnik, a C 10
pierwszych cyfr mnożnika. Gdy N ≤ 10 to należy wypisać dokładnie N cyfr.
     */
    public sealed class HodowlaAlg : ProblemBase
    {
        protected override string Input => "3\r\n1\r\n70\r\n16";

        protected override string Output => "1 3\r\n40 8574848899\r\n10 1117014753";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                List<int> prev = [1], curr = [3];

                int a = int.Parse(input[i]);
                for (int j = 1; j < a; j++)
                {
                    List<int> next = new(curr);
                    int toCarry = 0;
                    int k = curr.Count - 1;
                    while(k >= 0)
                    {
                        next[k] <<= 2;
                        next[k] += toCarry;
                        toCarry = next[k] / 10;
                        next[k] %= 10;
                        k--;
                    }
                    if (toCarry > 0)
                        next.Insert(0, toCarry);
                    int d = next.Count - prev.Count;
                    for(k = prev.Count - 1; k >= 0; k--)
                    {
                        next[k + d] -= prev[k];
                    }
                    for (k = next.Count - 1; k > 0; k--)
                    {
                        if (next[k] < 0)
                        {
                            next[k] += 10;
                            next[k - 1]--;
                        }
                    }
                    if (next[0] == 0)
                        next.RemoveAt(0);
                    prev = curr;
                    curr = next;
                }
                output.Add($"{curr.Count} {string.Join("", curr.Take(10))}");
            }
        }
    }
}
